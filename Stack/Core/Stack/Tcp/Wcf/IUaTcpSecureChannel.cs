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
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using System.Xml;

using Opc.Ua.Bindings;

namespace Opc.Ua.Bindings
{        
    /// <summary>
    /// Uses to access information about the secure channel
    /// </summary>
    public interface IUaTcpSecureChannel
    {
        /// <summary>
        /// Returns the endpoint description used by the channel.
        /// </summary>
        EndpointDescription EndpointDescription { get; }
    }
}
