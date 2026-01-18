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
    /// A list of event field values returned in a NotificationMessage.
    /// </summary>
	public partial class EventFieldList
	{
        #region Public Properties
        /// <summary>
        /// The handle cast to a notification message.
        /// </summary>
        public NotificationMessage Message
        {
            get { return m_handle as NotificationMessage; }
            set { m_handle = value; }
        }

        /// <summary>
        /// A handle associated withe the event instance.
        /// </summary>
        public object Handle
        {
            get { return m_handle; }
            set { m_handle = value; }
        }
        #endregion

        #region Private Fields
        private object m_handle;
        #endregion
    }
}
