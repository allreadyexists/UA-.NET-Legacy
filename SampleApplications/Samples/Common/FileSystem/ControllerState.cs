/*
* Copyright (c) 2005-2026 - OPC Foundation
* 
* All Rights Reserved.
* 
* NOTICE:  All information contained herein is, and remains the property of 
* OPC Foundation. The intellectual and technical concepts contained 
* herein are proprietary to OPC Foundation and may be covered by 
* U.S. and Foreign Patents, patents in process, and are protected by trade secret 
* or copyright law. Dissemination of this information or reproduction of this 
* material is strictly forbidden unless prior written permission is obtained 
* from OPC Foundation.
*/

using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Text;
using System.Reflection;
using System.Threading;
using System.Globalization;
using Opc.Ua;
using Opc.Ua.Server;

namespace FileSystem
{
    public partial class ControllerState
    {
        #region Constructors
        /// <summary>
        /// Initializes a controller from a file.
        /// </summary>
        public ControllerState(ISystemContext context, FileInfo fileInfo) : base(null)
        {
            fileInfo.Refresh();

            // extract the display name from the original file path.
            string name = fileInfo.Name;

            // need to read the correct casing from the file system.
            if (fileInfo.Exists)
            {
                FileInfo[] files = fileInfo.Directory.GetFiles(name);

                if (files != null && files.Length > 0)
                {
                    name = files[0].Name;
                }
            }

            int index = name.LastIndexOf('.');

            if (index >= 0)
            {
                name = name.Substring(0, index);
            }

            // get the system to use.
            FileSystemMonitor system = context.SystemHandle as FileSystemMonitor;

            if (system != null)
            {
                this.NodeId = system.CreateNodeIdFromFilePath(ObjectTypes.ControllerType, fileInfo.FullName);
                this.BrowseName = new QualifiedName(name, system.NamespaceIndex);
                this.OnValidate = system.ValidateController;
            }
            
            this.DisplayName = new LocalizedText(name);
            this.EventNotifier = EventNotifiers.None;
            this.TypeDefinitionId = GetDefaultTypeDefinitionId(context.NamespaceUris);
        }
        #endregion

        #region Public Members
        /// <summary>
        /// Upates the tag values from the CSV file.
        /// </summary>
        public void UpdateValues(ISystemContext context)
        {
            if (m_source == null)
            {
                m_source = new CsvFile();
            }

            if (m_source.Timestamp.AddSeconds(1) > DateTime.UtcNow)
            {
                return;
            }

            FileInfo file = GetFile(context, this.NodeId);

            if (file == null || !file.Exists)
            {
                return;
            }

            m_source.Load(file);     
        }

        /// <summary>
        /// Checks if the file has changed since the last check.
        /// </summary>
        public void CheckForChanges(ISystemContext context)
        {
            FileInfo file = GetFile(context, this.NodeId);

            if (file == null)
            {
                return;
            }

            file.Refresh();

            DateTime lastWriteTime = DateTime.MinValue;

            if (!file.Exists)
            {
                if (m_lastWriteTime == DateTime.MinValue)
                {
                    return;
                }
            }
            else
            {
                lastWriteTime = file.LastWriteTime.ToUniversalTime();

                if (lastWriteTime == m_lastWriteTime)
                {
                    return;
                }
            }
            
            this.Temperature.UpdateChangeMasks(NodeStateChangeMasks.Value);
            this.TemperatureSetPoint.UpdateChangeMasks(NodeStateChangeMasks.Value);

            m_lastWriteTime = lastWriteTime;
        }

        /// <summary>
        /// Returns the file for the controller.
        /// </summary>
        public static FileInfo GetFile(ISystemContext context, NodeId controllerId)
        {
            FileSystemMonitor system = context.SystemHandle as FileSystemMonitor;

            if (system == null)
            {
                return null;
            }

            string filePath = system.ExtractPathFromNodeId(controllerId);
            filePath += ".csv";
            return new FileInfo(filePath);
        }
        #endregion
        
        #region Overridden Methods
        /// <summary>
        /// Sets up callbacks for dynamic variables.
        /// </summary>
        protected override void OnAfterCreate(ISystemContext context, NodeState node)
        {
            base.OnAfterCreate(context, node);
            
            this.Temperature.OnSimpleReadValue = ReadTemperature;
            this.TemperatureSetPoint.OnSimpleReadValue = ReadTemperatureSetPoint;
            this.TemperatureSetPoint.OnSimpleWriteValue = WriteTemperatureSetPoint;
        }
        
        /// <summary>
        /// Reads the temperature for the controller.
        /// </summary>
        private ServiceResult ReadTemperature(ISystemContext context, NodeState node, ref object value)
        {            
            UpdateValues(context);

            double? temperature = GetValueByName(BrowseNames.Temperature) as double?;

            if (temperature == null)
            {
                return StatusCodes.BadOutOfService;
            }

            value = temperature.Value;

            return ServiceResult.Good;
        }
        
        /// <summary>
        /// Reads the temperature set point for the controller.
        /// </summary>
        private ServiceResult ReadTemperatureSetPoint(ISystemContext context, NodeState node, ref object value)
        {            
            UpdateValues(context);

            double? temperatureSetPoint = GetValueByName(BrowseNames.TemperatureSetPoint) as double?;

            if (temperatureSetPoint == null)
            {
                return StatusCodes.BadOutOfService;
            }

            value = temperatureSetPoint.Value;

            return ServiceResult.Good;
        }

        /// <summary>
        /// Write the temperature set point for the controller.
        /// </summary>
        private ServiceResult WriteTemperatureSetPoint(ISystemContext context, NodeState node, ref object value)
        {            
            FileInfo file = GetFile(context, this.NodeId);

            if (file == null || !file.Exists)
            {
                return StatusCodes.BadOutOfService;
            }

            UpdateValues(context);

            SetValueByName(BrowseNames.TemperatureSetPoint, value);

            m_source.Save(file);

            return ServiceResult.Good;
        }
        
        /// <summary>
        /// Sets the value of the tag with the specified name.
        /// </summary>
        private void SetValueByName(string name, object value)
        {
            if (m_source != null)
            {
                m_source.SetValue(name, value);
            }
        }        

        /// <summary>
        /// Returns the value of the tag with the specified name.
        /// </summary>
        private object GetValueByName(string name)
        {
            if (m_source != null)
            {
                return m_source.GetValue(name);
            }

            return null;
        }
        #endregion

        #region Private Fields
        private CsvFile m_source;
        private DateTime m_lastWriteTime;
        #endregion
    }
}
