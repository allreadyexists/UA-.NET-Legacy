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
    #region MessageContextExtension Class
    /// <summary>
    /// Uses to add the service message context to the WCF operation context.
    /// </summary>
    public class MessageContextExtension : IExtension<OperationContext>
    {
        /// <summary>
        /// Initializes the object with the message context to use.
        /// </summary>
        public MessageContextExtension(ServiceMessageContext messageContext)
        {
            m_messageContext = messageContext;
        }

        /// <summary>
        /// Returns the message context associated with the current WCF operation context.
        /// </summary>
        public static MessageContextExtension Current
        {
            get 
            {
                OperationContext context = OperationContext.Current;

                if (context != null)
                {
                    return OperationContext.Current.Extensions.Find<MessageContextExtension>();
                }

                return null;
            }
        }

        /// <summary>
        /// Returns the message context associated with the current WCF operation context.
        /// </summary>
        public static ServiceMessageContext CurrentContext
        {
            get 
            {
                MessageContextExtension extension = MessageContextExtension.Current;

                if (extension != null)
                {
                    return extension.MessageContext;
                }

                return ServiceMessageContext.ThreadContext;
            }
        }
            
        /// <summary>
        /// The message context to use.
        /// </summary>
        public ServiceMessageContext MessageContext
        {
            get 
            { 
                return m_messageContext;
            }
        }

        #region IExtension<OperationContext> Members
        /// <summary cref="IExtension{T}.Attach" />
        public void Attach(OperationContext owner)
        {
        }
        
        /// <summary cref="IExtension{T}.Detach" />
        public void Detach(OperationContext owner)
        {
        }
        #endregion
    
        private ServiceMessageContext m_messageContext;
    }
    #endregion
}
