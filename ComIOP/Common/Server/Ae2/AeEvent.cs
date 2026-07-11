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
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices;
using Opc.Ua.Client;
using OpcRcw.Hda;

namespace Opc.Ua.Com.Server
{
    /// <summary>
    /// Stores an event received from the UA server.
    /// </summary>
    public class AeEvent
    {
        /// <summary>
        /// A number assigned by the proxy to the event when it arrives.
        /// </summary>
        public int Cookie { get; set; }

        /// <summary>
        /// The event id.
        /// </summary>
        public byte[] EventId { get; set; }

        /// <summary>
        /// The event type.
        /// </summary>
        public NodeId EventType { get; set; }

        /// <summary>
        /// The event source.
        /// </summary>
        public string SourceName { get; set; }

        /// <summary>
        /// The event time.
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// The event message.
        /// </summary>
        public LocalizedText Message { get; set; }

        /// <summary>
        /// The event severity.
        /// </summary>
        public ushort Severity { get; set; }

        /// <summary>
        /// The user that triggered the audit event.
        /// </summary>
        public string AuditUserId { get; set; }

        /// <summary>
        /// The NodeId of the condition (used for acknowledging).
        /// </summary>
        public NodeId ConditionId { get; set; }

        /// <summary>
        /// The condition branch which the event belongs to.
        /// </summary>
        public NodeId BranchId { get; set; }

        /// <summary>
        /// The name of the condition.
        /// </summary>
        public string ConditionName { get; set; }

        /// <summary>
        /// The last comment.
        /// </summary>
        public LocalizedText Comment { get; set; }

        /// <summary>
        /// The user that added the comment.
        /// </summary>
        public string CommentUserId { get; set; }

        /// <summary>
        /// The qualilty of the underlying data source.
        /// </summary>
        public StatusCode Quality { get; set; }

        /// <summary>
        /// The current Enabled state (Conditions).
        /// </summary>
        public bool EnabledState { get; set; }

        /// <summary>
        /// The current Acknowledged state (Conditions).
        /// </summary>
        public bool AckedState { get; set; }

        /// <summary>
        /// The current Active state (Alarms).
        /// </summary>
        public bool ActiveState { get; set; }

        /// <summary>
        /// When the condition transitioned into the Active state (Alarms).
        /// </summary>
        public DateTime ActiveTime { get; set; }

        /// <summary>
        /// The current Limit state (ExclusiveLimitConditions).
        /// </summary>
        public LocalizedText LimitState { get; set; }

        /// <summary>
        /// The current HighHigh state (NonExclusiveLimitConditions).
        /// </summary>
        public LocalizedText HighHighState { get; set; }

        /// <summary>
        /// The current High state (NonExclusiveLimitConditions).
        /// </summary>
        public LocalizedText HighState { get; set; }

        /// <summary>
        /// The current Low state (NonExclusiveLimitConditions).
        /// </summary>
        public LocalizedText LowState { get; set; }

        /// <summary>
        /// The current LowLow state (NonExclusiveLimitConditions).
        /// </summary>
        public LocalizedText LowLowState { get; set; }

        /// <summary>
        /// The category for the event type.
        /// </summary>
        public AeEventCategory Category { get; set; }

        /// <summary>
        /// The attribute values requested for the category.
        /// </summary>
        public object[] AttributeValues { get; set; }
    }
}
