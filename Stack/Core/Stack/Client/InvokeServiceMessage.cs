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

using System.ServiceModel;

namespace Opc.Ua
{
    /// <summary>
    /// The message contract for the InvokeService service.
    /// </summary>
    [MessageContract(IsWrapped=false)]
    public class InvokeServiceMessage
    {    
        /// <summary>
        /// The body of the message.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        [MessageBodyMember(Namespace=Namespaces.OpcUaXsd, Order=0)]
        public byte[] InvokeServiceRequest;
        
        /// <summary>
        /// Initializes an empty message.
        /// </summary>    
        public InvokeServiceMessage()
        {
        }
            
        /// <summary>
        /// Initializes the message with the body.
        /// </summary>
        /// <param name="InvokeServiceRequest">The invoke service request.</param>
        public InvokeServiceMessage(byte[] InvokeServiceRequest)
        {
            this.InvokeServiceRequest = InvokeServiceRequest;
        }
    }

    /// <summary>
    /// The message contract for the InvokeService service response.
    /// </summary>
    [MessageContract(IsWrapped=false)]
    public class InvokeServiceResponseMessage
    {    
        /// <summary>
        /// The body of the message.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
        [MessageBodyMember(Namespace=Namespaces.OpcUaXsd, Order=0)]
        public byte[] InvokeServiceResponse;
        
        /// <summary>
        /// Initializes an empty message.
        /// </summary>
        public InvokeServiceResponseMessage()
        {
        }
            
        /// <summary>
        /// Initializes the message with the body.
        /// </summary>
        /// <param name="InvokeServiceResponse">The invoke service response.</param>
        public InvokeServiceResponseMessage(byte[] InvokeServiceResponse)
        {
            this.InvokeServiceResponse = InvokeServiceResponse;
        }
    }
}
