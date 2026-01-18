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
using System.Collections;
using System.Diagnostics;
using System.Xml;
using System.Threading;

namespace Opc.Ua.Server 
{
    /// <summary>
    /// The current publishing state for a subcription.
    /// </summary>  
    public enum PublishingState
    {
        /// <summary>
        /// The subscription is not ready to publish.
        /// </summary>
        Idle,

        /// <summary>
        /// The subscription has notifications that are ready to publish.
        /// </summary>
        NotificationsAvailable,

        /// <summary>
        /// The has already indicated that it is waiting for a publish request.
        /// </summary>
        WaitingForPublish,

        /// <summary>
        /// The subscription has expired.
        /// </summary>
        Expired
    }
}
