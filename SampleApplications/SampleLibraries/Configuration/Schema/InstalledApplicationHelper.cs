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
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;
using System.Reflection;

namespace Opc.Ua.Configuration
{    
    /// <summary>
    /// Specifies how to configure an application during installation.
    /// </summary>
    public partial class InstalledApplication
    {
        #region Public Methods
        /// <summary>
        /// Loads the application configuration from a configuration section.
        /// </summary>
        public static InstalledApplicationCollection Load(string filePath)
        {
            FileInfo file = new FileInfo(filePath);

            // look in current directory.
            if (!file.Exists)
            {
                file = new FileInfo(Utils.Format("{0}\\{1}", Environment.CurrentDirectory, filePath));
            }

            // look in executable directory.
            if (!file.Exists)
            {
                file = new FileInfo(Utils.GetAbsoluteFilePath(filePath));
            }

            // file not found.
            if (!file.Exists)
            {
                throw ServiceResultException.Create(
                    StatusCodes.BadConfigurationError,
                    "File does not exist: {0}\r\nCurrent directory is: {1}",
                    filePath,
                    Environment.CurrentDirectory);
            }

            return Load(file);
        }

        /// <summary>
        /// Loads a collection of security applications.
        /// </summary>
        public static InstalledApplicationCollection Load(FileInfo file)
        {
            XmlTextReader reader = new XmlTextReader(file.Open(FileMode.Open, FileAccess.Read));

            try
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(InstalledApplicationCollection));
                return serializer.ReadObject(reader, false) as InstalledApplicationCollection;
            }
            finally
            {
                reader.Close();
            }
        }
        #endregion
    }
}
