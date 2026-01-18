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
using System.Text;

namespace Opc.Ua.Bindings
{
    /// <summary>
    /// The binding for the UA native stack
    /// </summary>
    public abstract class BaseBinding : Binding
    {
        #region Constructors
        /// <summary>
        /// Initializes the binding.
        /// </summary>
        protected BaseBinding(
            NamespaceTable        namespaceUris,
            EncodeableFactory     factory,
            EndpointConfiguration configuration)
        {
            m_messageContext = new ServiceMessageContext();
            
            m_messageContext.MaxStringLength     = configuration.MaxStringLength;
            m_messageContext.MaxByteStringLength = configuration.MaxByteStringLength;
            m_messageContext.MaxArrayLength      = configuration.MaxArrayLength;
            m_messageContext.MaxMessageSize      = configuration.MaxMessageSize;
            m_messageContext.Factory             = factory;
            m_messageContext.NamespaceUris       = namespaceUris;
        }
        #endregion
        
        #region Public Properties
        /// <summary>
        /// The message context to use with the binding.
        /// </summary>
        public ServiceMessageContext MessageContext
        {
            get { return m_messageContext; }
            set { m_messageContext = value; }
        }
        #endregion

        #region Private Fields
        private ServiceMessageContext m_messageContext;
        #endregion
    }
}
