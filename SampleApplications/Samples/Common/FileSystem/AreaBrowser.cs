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
    /// <summary>
    /// A class to browse the references for a memory buffer. 
    /// </summary>
    public class AreaBrowser : NodeBrowser
    {
        #region Constructors
        /// <summary>
        /// Creates a new browser object with a set of filters.
        /// </summary>
        public AreaBrowser(
            ISystemContext context,
            ViewDescription view,
            NodeId referenceType,
            bool includeSubtypes,
            BrowseDirection browseDirection,
            QualifiedName browseName,
            IEnumerable<IReference> additionalReferences,
            bool internalOnly,
            AreaState area)
        :
            base(
                context,
                view,
                referenceType,
                includeSubtypes,
                browseDirection,
                browseName,
                additionalReferences,
                internalOnly)
        {
            m_stage = Stage.Begin;

            if (area != null)
            {
                m_area = AreaState.GetDirectory(context, area.NodeId);
                m_isRoot = area.IsRoot;
            }
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Returns the next reference.
        /// </summary>
        /// <returns></returns>
        public override IReference Next()
        {
            lock (DataLock)
            {
                IReference reference = null;

                // enumerate pre-defined references.
                // always call first to ensure any pushed-back references are returned first.
                reference = base.Next();

                if (reference != null)
                {
                    return reference;
                }
                
                // check that the root exists.
                if (m_area == null || !m_area.Exists)
                {
                    return null;
                }

                // don't start browsing huge number of references when only internal references are requested.
                if (InternalOnly)
                {
                    return null;
                }
                
                // start with directories.
                if (m_stage == Stage.Begin)
                {
                    m_stage = Stage.Directories;
                    m_directories = m_area.GetDirectories();
                    m_position = 0;
                }

                // enumerate directories.
                if (m_stage == Stage.Directories)
                {
                    if (IsRequired(ReferenceTypeIds.Organizes, false))
                    {
                        reference = NextChild();

                        if (reference != null)
                        {
                            return reference;
                        }
                    }

                    m_stage = Stage.Files;
                    m_files = m_area.GetFiles("*.csv");
                    m_position = 0;
                }
                
                // enumerate files.
                if (m_stage == Stage.Files)
                {
                    if (IsRequired(ReferenceTypeIds.Organizes, false))
                    {
                        reference = NextChild();

                        if (reference != null)
                        {
                            return reference;
                        }
                    }

                    m_stage = Stage.Parents;
                    m_position = 0;
                }
                
                // enumerate parents.
                if (m_stage == Stage.Parents)
                {
                    if (IsRequired(ReferenceTypeIds.Organizes, true))
                    {
                        if (m_isRoot)
                        {
                            reference = new NodeStateReference(ReferenceTypeIds.Organizes, true, ObjectIds.ObjectsFolder);
                        }
                        else
                        {
                            // create the parent area.
                            AreaState parent = new AreaState(SystemContext, m_area.Parent);

                            // construct the reference.
                            reference = new NodeStateReference(ReferenceTypeIds.Organizes, true, parent);
                        }
                        
                        m_stage = Stage.Done;
                        m_position = 0;
                        
                        return reference;
                    }
                }

                // all done.
                return null;
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Returns the next child with the requested browse name.
        /// </summary>
        private IReference FindByBrowseName()
        {
            NodeState target = null;
            
            // check if match found previously.
            if (m_position == UInt32.MaxValue)
            {
                return null;
            }

            // get the system to use.
            FileSystemMonitor system = SystemContext.SystemHandle as FileSystemMonitor;

            if (system == null)
            {
                return null;
            }

            // browse name must be qualified by the correct namespace.
            if (system.NamespaceIndex != base.BrowseName.NamespaceIndex)
            {
                return null;
            }
            
            // look for file.
            FileInfo[] files = m_area.GetFiles(Utils.Format("{0}.csv", base.BrowseName.Name));

            if (files != null && files.Length > 0)
            {
                target = new ControllerState(SystemContext, files[0]);
            }

            // look for directory
            if (target == null)
            {
                DirectoryInfo[] directories = m_area.GetDirectories(base.BrowseName.Name, SearchOption.TopDirectoryOnly);

                if (directories == null || directories.Length <= 0)
                {
                    return null;
                }

                target = new AreaState(SystemContext, directories[0]);
            }

            // match found.
            m_position = UInt32.MaxValue;

            // return the requested reference type.
            return new NodeStateReference(ReferenceTypeIds.Organizes, false, target);
        }

        /// <summary>
        /// Returns the next child.
        /// </summary>
        private IReference NextChild()
        {
            // check if a specific browse name is requested.
            if (!QualifiedName.IsNull(base.BrowseName))
            {
                return FindByBrowseName();
            }
            
            NodeState target = null;

            // process directories.
            if (m_stage == Stage.Directories)
            {
                if (m_position < 0 || m_directories == null || m_position >= m_directories.Length)
                {
                    return null;
                }

                target = new AreaState(SystemContext, m_directories[m_position]);
                m_position++;
            }

            // process files.
            if (m_stage == Stage.Files)
            {
                if (m_position < 0 || m_files == null || m_position >= m_files.Length)
                {
                    return null;
                }

                target = new ControllerState(SystemContext, m_files[m_position]);
                m_position++;
            }
                        
            return new NodeStateReference(ReferenceTypeIds.Organizes, false, target);
        }
        #endregion

        #region Stage Enumeration
        /// <summary>
        /// The stages available in a browse operation.
        /// </summary>
        private enum Stage
        {
            Begin,
            Directories,
            Files,
            Parents,
            Done
        }
        #endregion

        #region Private Fields
        private Stage m_stage;
        private uint m_position;
        private DirectoryInfo m_area;
        private bool m_isRoot;
        private FileInfo[] m_files;
        private DirectoryInfo[] m_directories;
        #endregion
    }
}
