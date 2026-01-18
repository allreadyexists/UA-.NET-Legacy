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
    /// Stores the metadata for a node representing a folder on a file system.
    /// </summary>
    public class ArchiveFolderState : Opc.Ua.FolderState
    {
        /// <summary>
        /// Creates a new instance of a folder.
        /// </summary>
        public ArchiveFolderState(ISystemContext context, ArchiveFolder folder, ushort namespaceIndex)
        : 
            base(null)
        {
            m_archiveFolder = folder;

            this.TypeDefinitionId = ObjectTypeIds.FolderType;
            this.SymbolicName = folder.Name;
            this.NodeId = ConstructId(folder.UniquePath, namespaceIndex);
            this.BrowseName = new QualifiedName(folder.Name, namespaceIndex);
            this.DisplayName = new LocalizedText(this.BrowseName.Name);
            this.Description = null;
            this.WriteMask = 0;
            this.UserWriteMask = 0;
            this.EventNotifier = EventNotifiers.None;
        }

        /// <summary>
        /// Constructs a node identifier for a folder object.
        /// </summary>
        public static NodeId ConstructId(string filePath, ushort namespaceIndex)
        {
            ParsedNodeId parsedNodeId = new ParsedNodeId();

            parsedNodeId.RootId = filePath;
            parsedNodeId.NamespaceIndex = namespaceIndex;
            parsedNodeId.RootType = NodeTypes.Folder;

            return parsedNodeId.Construct();
        }

        /// <summary>
        /// The physical folder referenced by the node.
        /// </summary>
        public ArchiveFolder ArchiveFolder
        {
            get { return m_archiveFolder; }
        }

        /// <summary>
        /// Creates a browser that explores the structure of the block.
        /// </summary>
        public override INodeBrowser CreateBrowser(
            ISystemContext context,
            ViewDescription view,
            NodeId referenceType,
            bool includeSubtypes,
            BrowseDirection browseDirection,
            QualifiedName browseName,
            IEnumerable<IReference> additionalReferences,
            bool internalOnly)
        {
            NodeBrowser browser = new ArchiveFolderBrowser(
                context,
                view,
                referenceType,
                includeSubtypes,
                browseDirection,
                browseName,
                additionalReferences,
                internalOnly,
                this);

            PopulateBrowser(context, browser);

            return browser;
        }

        private ArchiveFolder m_archiveFolder;
    }
}
