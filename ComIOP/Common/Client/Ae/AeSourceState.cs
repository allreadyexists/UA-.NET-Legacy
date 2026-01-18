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
    public partial class AeSourceState : BaseObjectState
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AeSourceState"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="areaId">The area id.</param>
        /// <param name="qualifiedName">The qualified name for the source.</param>
        /// <param name="name">The name of the source.</param>
        /// <param name="namespaceIndex">Index of the namespace.</param>
        public AeSourceState(
            ISystemContext context,
            string areaId,
            string qualifiedName,
            string name,
            ushort namespaceIndex)
            :
                base(null)
        {
            m_areaId = areaId;
            m_qualifiedName = qualifiedName;

            this.TypeDefinitionId = Opc.Ua.ObjectTypeIds.BaseObjectType;
            this.NodeId = AeModelUtils.ConstructIdForSource(m_areaId, name, namespaceIndex);
            this.BrowseName = new QualifiedName(name, namespaceIndex);
            this.DisplayName = this.BrowseName.Name;
            this.Description = null;
            this.WriteMask = 0;
            this.UserWriteMask = 0;
            this.EventNotifier = EventNotifiers.None;

            this.AddReference(ReferenceTypeIds.HasNotifier, true, AeModelUtils.ConstructIdForArea(m_areaId, namespaceIndex));
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the qualified name for the source.
        /// </summary>
        /// <value>The qualified name for the source.</value>
        public string QualifiedName
        {
            get { return m_qualifiedName; }
        }
        #endregion

        #region Private Fields
        private string m_areaId;
        private string m_qualifiedName;
        #endregion
    }
}
