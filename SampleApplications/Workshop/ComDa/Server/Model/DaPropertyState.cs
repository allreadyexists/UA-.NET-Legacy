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
using Opc.Ua.Com;

namespace Quickstarts.ComDataAccessServer
{    
    /// <summary>
    /// A object which maps a COM DA item to a UA variable.
    /// </summary>
    public partial class DaPropertyState : PropertyState
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DaPropertyState"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="itemId">The item id.</param>
        /// <param name="property">The property.</param>
        /// <param name="namespaceIndex">Index of the namespace.</param>
        public DaPropertyState(
            ISystemContext context, 
            string itemId, 
            DaProperty property, 
            ushort namespaceIndex) 
        : 
            base(null)
        {
            this.TypeDefinitionId = Opc.Ua.VariableTypeIds.DataItemType;
            this.Description = null;
            this.WriteMask = 0;
            this.UserWriteMask = 0;

            if (property != null)
            {
                Initialize(context, itemId, property, namespaceIndex);               
            }
        }
        #endregion

        #region Public Interface
        /// <summary>
        /// Gets the item id.
        /// </summary>
        /// <value>The item id.</value>
        public string ItemId
        {
            get
            {
                return m_itemId;
            }
        }

        /// <summary>
        /// Gets the property id.
        /// </summary>
        /// <value>The property id.</value>
        public int PropertyId
        {
            get
            {
                if (m_property != null)
                {
                    return m_property.PropertyId;
                }

                return -1;
            }
        }

        /// <summary>
        /// Gets the property description.
        /// </summary>
        /// <value>The property description.</value>
        public DaProperty Property
        {
            get { return m_property; }
        }

        /// <summary>
        /// Initializes the node from the element.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="itemId">The item id.</param>
        /// <param name="property">The property.</param>
        /// <param name="namespaceIndex">Index of the namespace.</param>
        public void Initialize(ISystemContext context, string itemId, DaProperty property, ushort namespaceIndex)
        {
            m_itemId = itemId;
            m_property = property;

            if (property == null)
            {
                return;
            }

            this.NodeId = ModelUtils.ConstructIdForDaElement(m_itemId, property.PropertyId, namespaceIndex);
            this.BrowseName = new QualifiedName(property.Name, namespaceIndex);
            this.DisplayName = new LocalizedText(property.Name);
            this.TypeDefinitionId = Opc.Ua.VariableTypeIds.PropertyType;
            this.Value = null;
            this.StatusCode = StatusCodes.BadWaitingForInitialData;
            this.Timestamp = DateTime.UtcNow;

            bool isArray = false;
            this.DataType = ComUtils.GetDataTypeId(property.DataType, out isArray);
            this.ValueRank = (isArray)?ValueRanks.OneOrMoreDimensions:ValueRanks.Scalar;
            this.ArrayDimensions = null;

            // assume that properties with item ids are writeable. the server may still reject the write.
            if (String.IsNullOrEmpty(property.ItemId))
            {
                this.AccessLevel = AccessLevels.CurrentRead;
            }
            else
            {
                this.AccessLevel = AccessLevels.CurrentReadOrWrite;
            }

            this.UserAccessLevel = this.AccessLevel;
            this.MinimumSamplingInterval = MinimumSamplingIntervals.Indeterminate;
            this.Historizing = false;

            // add a reference to the parent node.
            NodeId parentNodeId = ModelUtils.ConstructIdForDaElement(itemId, -1, namespaceIndex);
            this.AddReference(ReferenceTypeIds.HasProperty, true, parentNodeId);
        }
        #endregion

        #region Private Fields
        private string m_itemId;
        private DaProperty m_property;
        #endregion
    }
}
