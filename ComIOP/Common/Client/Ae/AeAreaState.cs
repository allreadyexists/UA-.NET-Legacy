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
using System.Reflection;
using System.Threading;
using Opc.Ua;
using Opc.Ua.Server;

namespace Opc.Ua.Com.Client
{    
    /// <summary>
    /// A object which maps a segment to a UA object.
    /// </summary>
    public partial class AeAreaState : FolderState
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AeAreaState"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="qualifiedName">The qualified name for the area.</param>
        /// <param name="name">The name of the area.</param>
        /// <param name="namespaceIndex">Index of the namespace.</param>
        public AeAreaState(
            ISystemContext context,
            string qualifiedName,
            string name,
            ushort namespaceIndex)
        : 
            base(null)
        {
            m_qualifiedName = qualifiedName;

            this.SymbolicName = name;
            this.TypeDefinitionId = Opc.Ua.ObjectTypeIds.FolderType;
            this.NodeId = AeModelUtils.ConstructIdForArea(qualifiedName, namespaceIndex);
            this.BrowseName = new QualifiedName(name, namespaceIndex);
            this.DisplayName = this.BrowseName.Name;
            this.Description = null;
            this.WriteMask = 0;
            this.UserWriteMask = 0;
            this.EventNotifier = EventNotifiers.SubscribeToEvents;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the qualified name for the area.
        /// </summary>
        /// <value>The qualified name for the area.</value>
        public string QualifiedName
        {
            get { return m_qualifiedName; }
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Creates a browser that finds the references to the branch.
        /// </summary>
        /// <param name="context">The system context to use.</param>
        /// <param name="view">The view which may restrict the set of references/nodes found.</param>
        /// <param name="referenceType">The type of references being followed.</param>
        /// <param name="includeSubtypes">Whether subtypes of the reference type are followed.</param>
        /// <param name="browseDirection">Which way the references are being followed.</param>
        /// <param name="browseName">The browse name of a specific target (used when translating browse paths).</param>
        /// <param name="additionalReferences">Any additional references that should be included.</param>
        /// <param name="internalOnly">If true the browser should not making blocking calls to external systems.</param>
        /// <returns>The browse object (must be disposed).</returns>
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
            NodeBrowser browser = new AeAreaBrower(
                context,
                view,
                referenceType,
                includeSubtypes,
                browseDirection,
                browseName,
                additionalReferences,
                internalOnly,
                m_qualifiedName,
                this.NodeId.NamespaceIndex);

            PopulateBrowser(context, browser);

            return browser;
        }
        #endregion

        #region Private Fields
        private string m_qualifiedName;
        #endregion
    }
}
