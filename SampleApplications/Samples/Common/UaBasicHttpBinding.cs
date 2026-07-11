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
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;
using System.Text;

namespace Opc.Ua.Samples
{
    /// <summary>
    /// The binding for the UA TCP protocol
    /// </summary>
    public class UaBasicHttpBinding : Opc.Ua.Bindings.BaseBinding
    {
        #region Constructors
        /// <summary>
        /// Initializes the binding.
        /// </summary>
        public UaBasicHttpBinding(
            NamespaceTable        namespaceUris,
            EncodeableFactory     factory,
            EndpointConfiguration configuration,
            EndpointDescription   description)
        :
            base(namespaceUris, factory, configuration)
        {                       
            if (description != null && description.SecurityMode != MessageSecurityMode.None)
            {
                // TBD
            }
                        
            m_encoding  = new TextMessageEncodingBindingElement(MessageVersion.Soap12WSAddressing10, Encoding.UTF8);
           
            // WCF does not distinguish between arrays and byte string.
            int maxArrayLength = configuration.MaxArrayLength;

            if (configuration.MaxArrayLength < configuration.MaxByteStringLength)
            {
                maxArrayLength = configuration.MaxByteStringLength;
            }

            m_encoding.ReaderQuotas.MaxArrayLength         = maxArrayLength;
            m_encoding.ReaderQuotas.MaxStringContentLength = configuration.MaxStringLength;
            m_encoding.ReaderQuotas.MaxBytesPerRead        = Int32.MaxValue;
            m_encoding.ReaderQuotas.MaxDepth               = Int32.MaxValue;
            m_encoding.ReaderQuotas.MaxNameTableCharCount  = Int32.MaxValue;

            m_transport = new HttpTransportBindingElement();

            m_transport.AllowCookies           = false;
            m_transport.AuthenticationScheme   = System.Net.AuthenticationSchemes.Anonymous;
            m_transport.BypassProxyOnLocal     = true;
            m_transport.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
            m_transport.KeepAliveEnabled       = true;
            m_transport.ManualAddressing       = false;
            m_transport.MaxBufferPoolSize      = Int32.MaxValue;
            m_transport.MaxBufferSize          = configuration.MaxMessageSize;
            m_transport.MaxReceivedMessageSize = configuration.MaxMessageSize;
            m_transport.TransferMode           = TransferMode.Buffered;
            m_transport.UseDefaultWebProxy     = false;
        }
        #endregion
        
        #region Overridden Methods
        /// <summary>
        /// The URL scheme for the UA TCP protocol.
        /// </summary>
        public override string Scheme 
        {
            get { return Utils.UriSchemeHttp; } 
        }
        
        /// <summary>
        /// Create the set of binding elements that make up this binding. 
        /// </summary>
        public override BindingElementCollection CreateBindingElements()
        {   
            BindingElementCollection elements = new BindingElementCollection();

            elements.Add(m_encoding);
            elements.Add(m_transport);

            return elements.Clone();
        }
        #endregion

        #region Private Fields
        private TextMessageEncodingBindingElement m_encoding;
        private HttpTransportBindingElement m_transport;
        #endregion
    }
}
