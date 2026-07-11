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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Opc.Ua.Server;


namespace Opc.Ua.Com.Client
{
    /// <summary>
    /// A handle that describes how to access a node/attribute via an i/o manager.
    /// </summary>
    public class ComMonitoredItem : MonitoredItem
    {
        #region Constructors
        /// <summary>
        /// Initializes the object with its node type.
        /// </summary>
        public ComMonitoredItem(
            IServerInternal server,
            INodeManager nodeManager,
            object mangerHandle,
            uint subscriptionId,
            uint id,
            Session session,
            ReadValueId itemToMonitor,
            DiagnosticsMasks diagnosticsMasks,
            TimestampsToReturn timestampsToReturn,
            MonitoringMode monitoringMode,
            uint clientHandle,
            MonitoringFilter originalFilter,
            MonitoringFilter filterToUse,
            Range range,
            double samplingInterval,
            uint queueSize,
            bool discardOldest,
            double sourceSamplingInterval)
            : base(server,
                    nodeManager,
                    mangerHandle,
                    subscriptionId,
                    id,
                    session,
                    itemToMonitor,
                    diagnosticsMasks,
                    timestampsToReturn,
                    monitoringMode,
                    clientHandle,
                    originalFilter,
                    filterToUse,
                    range,
                    samplingInterval,
                    queueSize,
                    discardOldest,
                    sourceSamplingInterval)
        {
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Publishes a single data change notifications.
        /// </summary>
        protected override bool Publish(OperationContext context,
            Queue<MonitoredItemNotification> notifications,
            Queue<DiagnosticInfo> diagnostics,
            DataValue value,
            ServiceResult error)
        {
            bool result = base.Publish(context, notifications, diagnostics, value, error);
            return result;
        }
        #endregion
    }
}
