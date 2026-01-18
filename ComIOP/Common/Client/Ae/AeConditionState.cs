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
using OpcRcw.Ae;

namespace Opc.Ua.Com.Client
{        
    /// <summary>
    /// A object which represents a COM AE condition.
    /// </summary>
    public partial class AeConditionState : AlarmConditionState
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AeConditionState"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="handle">The handle.</param>
        /// <param name="acknowledgeMethod">The acknowledge method.</param>
        public AeConditionState(ISystemContext context, NodeHandle handle, AddCommentMethodState acknowledgeMethod)
        :
            base(null)
        {
            AeParsedNodeId parsedNodeId = (AeParsedNodeId)handle.ParsedNodeId;

            this.NodeId = handle.NodeId;

            this.TypeDefinitionId = AeParsedNodeId.Construct(
                Constants.CONDITION_EVENT, 
                parsedNodeId.CategoryId, 
                parsedNodeId.ConditionName, 
                parsedNodeId.NamespaceIndex);

            this.Acknowledge = acknowledgeMethod;
            this.AddChild(acknowledgeMethod);
        }
        #endregion

        #region Public Properties
        #endregion

        #region Private Fields
        #endregion
    }
}
