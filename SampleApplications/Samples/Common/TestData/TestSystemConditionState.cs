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
using Opc.Ua;

namespace TestData
{
    public partial class TestSystemConditionState
    {
        #region Initialization
        /// <summary>
        /// Initializes the object as a collection of counters which change value on read.
        /// </summary>
        protected override void OnAfterCreate(ISystemContext context, NodeState node)
        {
            base.OnAfterCreate(context, node);

            this.MonitoredNodeCount.OnSimpleReadValue = OnReadMonitoredNodeCount;
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Reads the value for the MonitoredNodeCount.
        /// </summary>
        protected virtual ServiceResult OnReadMonitoredNodeCount(
            ISystemContext context, 
            NodeState node, 
            ref object value)
        {
            TestDataSystem system = context.SystemHandle as TestDataSystem;

            if (system == null)
            {
                return StatusCodes.BadOutOfService;
            }

            value = system.MonitoredNodeCount;
            return ServiceResult.Good;
        }
        #endregion
    }
}
