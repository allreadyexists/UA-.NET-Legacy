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
using System.IO;
using Opc.Ua;

namespace Quickstarts.HistoricalAccessServer
{
    /// <summary>
    /// Stores the metadata for a node representing a folder on a file system.
    /// </summary>
    public class ArchiveFolder
    {
        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public ArchiveFolder(string uniquePath, DirectoryInfo folder)
        {
            m_uniquePath = uniquePath;
            m_directoryInfo = folder;
        }

        /// <summary>
        /// Returns the child folders.
        /// </summary>
        public ArchiveFolder[] GetChildFolders()
        {
            List<ArchiveFolder> folders = new List<ArchiveFolder>();

            if (!m_directoryInfo.Exists)
            {
                return folders.ToArray();
            }

            foreach (DirectoryInfo directory in m_directoryInfo.GetDirectories())
            {
                StringBuilder buffer = new StringBuilder(m_uniquePath);
                buffer.Append('/');
                buffer.Append(directory.Name);
                folders.Add(new ArchiveFolder(buffer.ToString(), directory));
            }

            return folders.ToArray();
        }

        /// <summary>
        /// Returns the child folders.
        /// </summary>
        public ArchiveItem[] GetItems()
        {
            List<ArchiveItem> items = new List<ArchiveItem>();

            if (!m_directoryInfo.Exists)
            {
                return items.ToArray();
            }

            foreach (FileInfo file in m_directoryInfo.GetFiles("*.csv"))
            {
                StringBuilder buffer = new StringBuilder(m_uniquePath);
                buffer.Append('/');
                buffer.Append(file.Name);
                items.Add(new ArchiveItem(buffer.ToString(), file));
            }

            return items.ToArray();
        }

        /// <summary>
        /// Returns the parent folder.
        /// </summary>
        public ArchiveFolder GetParentFolder()
        {
            string parentPath = String.Empty;

            if (!m_directoryInfo.Exists)
            {
                return null;
            }

            int index = m_uniquePath.LastIndexOf('/');

            if (index > 0)
            {
                parentPath = m_uniquePath.Substring(0, index);
            }

            return new ArchiveFolder(parentPath, m_directoryInfo.Parent);
        }

        /// <summary>
        /// The unique path to the folder in the archive.
        /// </summary>
        public string UniquePath
        {
            get { return m_uniquePath; }
        }

        /// <summary>
        /// A name for the folder.
        /// </summary>
        public string Name
        {
            get { return m_directoryInfo.Name; }
        }

        /// <summary>
        /// The physical folder in the archive.
        /// </summary>
        public DirectoryInfo DirectoryInfo
        {
            get { return m_directoryInfo; }
        }

        private string m_uniquePath;
        private DirectoryInfo m_directoryInfo;
    }
}
