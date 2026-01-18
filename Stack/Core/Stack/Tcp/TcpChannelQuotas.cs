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
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;

namespace Opc.Ua.Bindings
{
    /// <summary>
    /// Stores various configuration parameters used by the channel.
    /// </summary>
    public class TcpChannelQuotas
    {
        #region Constructors
        /// <summary>
        /// Creates an object with default values.
        /// </summary>
        public TcpChannelQuotas()
        {
            m_messageContext = ServiceMessageContext.GlobalContext;
            m_maxMessageSize = TcpMessageLimits.DefaultMaxMessageSize;
            m_maxBufferSize = TcpMessageLimits.DefaultMaxBufferSize;
            m_channelLifetime = TcpMessageLimits.DefaultChannelLifetime;
            m_securityTokenLifetime = TcpMessageLimits.DefaultSecurityTokenLifeTime;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// The context to use when encoding/decoding messages.
        /// </summary>
        public ServiceMessageContext MessageContext
        {
            get 
            { 
                lock (m_lock)
                {
                    return m_messageContext;
                }
            }

            set  
            {
                lock (m_lock)
                {
                    m_messageContext = value; 
                }
            }
        }

        /// <summary>
        /// Validator to use when handling certificates.
        /// </summary>
        public X509CertificateValidator CertificateValidator
        {
            get 
            { 
                lock (m_lock)
                {
                    return m_certificateValidator;
                }
            }

            set  
            {
                lock (m_lock)
                {
                    m_certificateValidator = value; 
                }
            }
        }
        
        /// <summary>
        /// The maximum size for a message sent or received.
        /// </summary>
        public int MaxMessageSize
        {
            get 
            { 
                lock (m_lock)
                {
                    return m_maxMessageSize; 
                }
            }
            
            set 
            { 
                lock (m_lock)
                {
                    m_maxMessageSize = value; 
                }
            }
        }

        /// <summary>
        /// The maximum size for the send or receive buffers.
        /// </summary>
        public int MaxBufferSize
        {
            get 
            { 
                lock (m_lock)
                {
                    return m_maxBufferSize; 
                }
            }
                        
            set 
            { 
                lock (m_lock)
                {
                    m_maxBufferSize = value; 
                }
            }
        }

        /// <summary>
        /// The default lifetime for the channel in milliseconds.
        /// </summary>
        public int ChannelLifetime
        {
            get 
            { 
                lock (m_lock)
                {
                    return m_channelLifetime;   
                }
            }
            
            set 
            { 
                lock (m_lock)
                {
                    m_channelLifetime = value;
                }
            }
        }

        /// <summary>
        /// The default lifetime for a security token in milliseconds.
        /// </summary>
        public int SecurityTokenLifetime
        {
            get 
            { 
                lock (m_lock)
                {
                    return m_securityTokenLifetime;
                }
            }
            
            set 
            { 
                lock (m_lock)
                {
                    m_securityTokenLifetime = value;
                }
            }
        }
        #endregion

        #region Private Fields
        private object m_lock = new object();
        private int m_maxMessageSize;
        private int m_maxBufferSize;
        private int m_channelLifetime;
        private int m_securityTokenLifetime;
        private ServiceMessageContext m_messageContext;
        private X509CertificateValidator m_certificateValidator;
        #endregion
    }
}
