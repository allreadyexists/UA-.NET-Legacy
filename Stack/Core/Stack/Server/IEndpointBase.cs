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
using System.ServiceModel;

namespace Opc.Ua
{
    
    #if OPCUA_USE_SYNCHRONOUS_ENDPOINTS
    /// <summary>
	/// The base interface for all services exposed by UA servers.
	/// </summary>
    [ServiceContract(Namespace = Namespaces.OpcUaWsdl)]
    public interface IEndpointBase
    {    
        /// <summary>
        /// The operation contract for the InvokeService service.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Response message.</returns>
        [OperationContract(Action = Namespaces.OpcUaWsdl + "/InvokeService", ReplyAction = Namespaces.OpcUaWsdl + "/InvokeServiceResponse")]
        InvokeServiceResponseMessage InvokeService(InvokeServiceMessage request);
    }
    #else
    /// <summary>
    /// The base asynchronous interface for all services exposed by UA servers.
    /// </summary>
    [ServiceContract(Namespace = Namespaces.OpcUaWsdl)]
    public interface IEndpointBase
    {
        /// <summary>
        /// The operation contract for the InvokeService service.
        /// </summary>
        [OperationContractAttribute(AsyncPattern = true, Action = Namespaces.OpcUaWsdl + "/InvokeService", ReplyAction = Namespaces.OpcUaWsdl + "/InvokeServiceResponse")]
        IAsyncResult BeginInvokeService(InvokeServiceMessage request, AsyncCallback callback, object asyncState);

        /// <summary>
        /// The method used to retrieve the results of a InvokeService service request.
        /// </summary>
        InvokeServiceResponseMessage EndInvokeService(IAsyncResult result);
    }
    #endif
}
