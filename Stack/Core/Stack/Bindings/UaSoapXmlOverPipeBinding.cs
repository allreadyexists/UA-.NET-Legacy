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
using System.ServiceModel.Channels;
using System.ServiceModel.Security;

namespace Opc.Ua.Bindings
{
    /// <summary>
    /// The binding for the .NET PIPE protocol
    /// </summary>
    public class UaSoapXmlOverPipeBinding : BaseBinding
    {
        #region Constructors
        /// <summary>
        /// Initializes the binding.
        /// </summary>
        /// <param name="namespaceUris">The namespace uris.</param>
        /// <param name="factory">The factory.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="description">The description.</param>
        public UaSoapXmlOverPipeBinding(
            NamespaceTable        namespaceUris,
            EncodeableFactory     factory,
            EndpointConfiguration configuration,
            EndpointDescription   description)
        :
            base(namespaceUris, factory, configuration)
        {                   
            if (description != null && description.SecurityMode != MessageSecurityMode.None)
            {
                SymmetricSecurityBindingElement bootstrap = (SymmetricSecurityBindingElement)SecurityBindingElement.CreateMutualCertificateBindingElement();
                
                bootstrap.MessageProtectionOrder       = MessageProtectionOrder.SignBeforeEncryptAndEncryptSignature;
                bootstrap.DefaultAlgorithmSuite        = SecurityPolicies.ToSecurityAlgorithmSuite(description.SecurityPolicyUri);
                bootstrap.IncludeTimestamp             = true;
                bootstrap.MessageSecurityVersion       = MessageSecurityVersion.WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10;
                // bootstrap.MessageSecurityVersion       = MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10;
                bootstrap.RequireSignatureConfirmation = false;
                bootstrap.SecurityHeaderLayout         = SecurityHeaderLayout.Strict;
                
                m_security = (SymmetricSecurityBindingElement)SecurityBindingElement.CreateSecureConversationBindingElement(bootstrap, true);
                
                m_security.MessageProtectionOrder       = MessageProtectionOrder.EncryptBeforeSign;
                m_security.DefaultAlgorithmSuite        = SecurityPolicies.ToSecurityAlgorithmSuite(description.SecurityPolicyUri);
                m_security.IncludeTimestamp             = true;
                m_security.MessageSecurityVersion       = MessageSecurityVersion.WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10;
                // m_security.MessageSecurityVersion       = MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10;
                m_security.RequireSignatureConfirmation = false;
                m_security.SecurityHeaderLayout         = SecurityHeaderLayout.Strict;

                m_security.SetKeyDerivation(true);
            }
            
            m_encoding = new BinaryMessageEncodingBindingElement();
           
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

            m_transport = new System.ServiceModel.Channels.NamedPipeTransportBindingElement();

            m_transport.ManualAddressing       = false;
            m_transport.MaxBufferPoolSize      = Int32.MaxValue;
            m_transport.MaxReceivedMessageSize = configuration.MaxMessageSize;
        }
        #endregion
        
        #region Overridden Methods
        /// <summary>
        /// The URL scheme for the UA TCP protocol.
        /// </summary>
        /// <value></value>
        /// <returns>The URI scheme that is used by the channels or listeners that are created by the factories built by the current binding.</returns>
        public override string Scheme 
        {
            get { return Uri.UriSchemeNetPipe; } 
        }

        /// <summary>
        /// Create the set of binding elements that make up this binding.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.ICollection`1"/> object of type <see cref="T:System.ServiceModel.Channels.BindingElement"/> that contains the binding elements from the current binding object in the correct order.
        /// </returns>
        public override BindingElementCollection CreateBindingElements()
        {   
            BindingElementCollection elements = new BindingElementCollection();

            if (m_security != null)
            {
                elements.Add(m_security);
            }

            elements.Add(m_encoding);
            elements.Add(m_transport);

            return elements.Clone();
        }
        #endregion

        #region Private Fields
        private SymmetricSecurityBindingElement m_security;
        private BinaryMessageEncodingBindingElement m_encoding;
        private System.ServiceModel.Channels.NamedPipeTransportBindingElement m_transport;
        #endregion
    }
}
