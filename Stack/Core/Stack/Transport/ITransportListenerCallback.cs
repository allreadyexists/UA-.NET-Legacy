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
using System.Text;

namespace Opc.Ua
{
    /// <summary>
    /// This is an interface to a object that receives notifications from the listener when a message arrives.
    /// </summary>
    public interface ITransportListenerCallback
    {
        /// <summary>
        /// Begins processing a request received via a binary encoded channel.
        /// </summary>
        /// <param name="channeId">A unique identifier for the secure channel which is the source of the request.</param>
        /// <param name="endpointDescription">The description of the endpoint which the secure channel is using.</param>
        /// <param name="request">The incoming request.</param>
        /// <param name="callback">The callback.</param>
        /// <param name="callbackData">The callback data.</param>
        /// <returns>The result which must be passed to the EndProcessRequest method.</returns>
        /// <seealso cref="EndProcessRequest" />
        /// <seealso cref="ITransportListener" />
        IAsyncResult BeginProcessRequest(
            string channeId,
            EndpointDescription endpointDescription,
            IServiceRequest request,
            AsyncCallback callback,
            object callbackData);

        /// <summary>
        /// Ends processing a request received via a binary encoded channel.
        /// </summary>
        /// <param name="result">The result returned by the BeginProcessRequest method.</param>
        /// <returns>The response to return over the secure channel.</returns>
        /// <seealso cref="BeginProcessRequest" />
        IServiceResponse EndProcessRequest(IAsyncResult result);
    }
}
