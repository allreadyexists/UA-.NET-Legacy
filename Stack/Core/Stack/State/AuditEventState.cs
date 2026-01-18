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

namespace Opc.Ua
{
    public partial class AuditEventState
    {
        #region Initialization
        /// <summary>
        /// Initializes a new event.
        /// </summary>
        /// <param name="context">The current system context.</param>
        /// <param name="source">The source of the event.</param>
        /// <param name="severity">The severity for the event.</param>
        /// <param name="message">The default message.</param>
        /// <param name="status">Whether the operation that caused the event succeeded.</param>
        /// <param name="actionTimestamp">When the operation started.</param>
        public virtual void Initialize(
            ISystemContext context, 
            NodeState source, 
            EventSeverity severity,
            LocalizedText message,
            bool status,
            DateTime actionTimestamp)
        {
            base.Initialize(context, source, severity, message);

            m_status = new PropertyState<bool>(this);
            m_status.Value = status;

            if (actionTimestamp != DateTime.MinValue)
            {
                m_actionTimeStamp = new PropertyState<DateTime>(this);
                m_actionTimeStamp.Value = actionTimestamp;
            }

            if (context.NamespaceUris != null)
            {
                m_serverId = new PropertyState<string>(this);
                m_serverId.Value = context.NamespaceUris.GetString(1);
            }

            if (context.AuditEntryId != null)
            {
                m_clientAuditEntryId = new PropertyState<string>(this);
                m_clientAuditEntryId.Value = context.AuditEntryId;
            }

            if (context.UserIdentity != null)
            {
                m_clientUserId = new PropertyState<string>(this);
                m_clientUserId.Value = context.UserIdentity.DisplayName;
            }
        }
        #endregion
    }
}
