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
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;

namespace Opc.Ua
{
    /// <summary>
    /// A datachange returned in a NotificationMessage.
    /// </summary>
	public partial class MonitoredItemNotification
	{
        #region Public Properties
        /// <summary>
        /// The notification message that the item belongs to.
        /// </summary>
        public NotificationMessage Message
        {
            get { return m_message; }
            set { m_message = value; }
        }

        /// <summary>
        /// The diagnostic info associated with the notification.
        /// </summary>
        public DiagnosticInfo DiagnosticInfo
        {
            get { return m_diagnosticInfo; }
            set { m_diagnosticInfo = value; }
        }
        #endregion

        #region Private Fields
        private NotificationMessage m_message;
        private DiagnosticInfo m_diagnosticInfo;
        #endregion
    }
}
