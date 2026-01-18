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

namespace Opc.Ua
{   
    /// <summary>
	/// An interface to a service request.
	/// </summary>
    public interface IServiceRequest : IEncodeable
    {
        /// <summary>
        /// The header for the request.
        /// </summary>
        /// <value>The request header.</value>
        RequestHeader RequestHeader { get; set; }
    }
    
    /// <summary>
	/// An interface to a service response.
	/// </summary>
    public interface IServiceResponse : IEncodeable 
    {
        /// <summary>
        /// The header for the response.
        /// </summary>
        /// <value>The response header.</value>
        ResponseHeader ResponseHeader { get; }
    }
    
    /// <summary>
	/// An interface to a service message.
	/// </summary>
    public interface IServiceMessage
    {
        /// <summary>
        /// Returns the request contained in the message.
        /// </summary>
        /// <returns></returns>
        IServiceRequest GetRequest();
    
        /// <summary>
        /// Creates an instance of a response message.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        object CreateResponse(IServiceResponse response);
    }
}
