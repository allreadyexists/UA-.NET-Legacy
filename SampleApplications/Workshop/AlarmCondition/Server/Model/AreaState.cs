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
using Opc.Ua;

namespace Quickstarts.AlarmConditionServer
{
    /// <summary>
    /// Maps an alarm area to a UA object node.
    /// </summary>
    public partial class AreaState : FolderState
    {
        #region Constructors
        /// <summary>
        /// Initializes the area.
        /// </summary>
        public AreaState(
            ISystemContext context,
            AreaState parent,
            NodeId nodeId,
            AreaConfiguration configuration) 
        : 
            base(parent)
        {
            Initialize(context);

            // initialize the area with the fixed metadata.
            this.SymbolicName = configuration.Name;
            this.NodeId = nodeId;
            this.BrowseName = new QualifiedName(Utils.Format("{0}", configuration.Name), nodeId.NamespaceIndex);
            this.DisplayName = BrowseName.Name;
            this.Description = null;
            this.ReferenceTypeId = ReferenceTypeIds.HasNotifier;
            this.TypeDefinitionId = ObjectTypeIds.FolderType;
            this.EventNotifier = EventNotifiers.SubscribeToEvents;
        }
        #endregion
    }
}
