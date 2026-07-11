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
using Opc.Ua.Server;

namespace Quickstarts.HistoricalAccessServer
{
    /// <summary>
    /// Provides access to the system which stores the data.
    /// </summary>
    public class UnderlyingSystem
    {
        /// <summary>
        /// Constructs a new system.
        /// </summary>
        public UnderlyingSystem(HistoricalAccessServerConfiguration configuration, ushort namespaceIndex)
        {
            m_configuration = configuration;
            m_namespaceIndex = namespaceIndex;
        }

        /// <summary>
        /// Returns a folder object for the specified node.
        /// </summary>
        public ArchiveFolderState GetFolderState(ISystemContext context, string rootId)
        {
            StringBuilder path = new StringBuilder();
            path.Append(m_configuration.ArchiveRoot);
            path.Append('/');
            path.Append(rootId);

            ArchiveFolder folder = new ArchiveFolder(rootId, new DirectoryInfo(path.ToString()));
            return new ArchiveFolderState(context, folder, m_namespaceIndex);
        }

        /// <summary>
        /// Returns a item object for the specified node.
        /// </summary>
        public ArchiveItemState GetItemState(ISystemContext context, ParsedNodeId parsedNodeId)
        {
            if (parsedNodeId.RootType != NodeTypes.Item)
            {
                return null;
            }

            StringBuilder path = new StringBuilder();
            path.Append(m_configuration.ArchiveRoot);
            path.Append('/');
            path.Append(parsedNodeId.RootId);

            ArchiveItem item = new ArchiveItem(parsedNodeId.RootId, new FileInfo(path.ToString()));

            return new ArchiveItemState(context, item, m_namespaceIndex);
        }

        private ushort m_namespaceIndex;
        private HistoricalAccessServerConfiguration m_configuration;
    }
}
