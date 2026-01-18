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
using System.ServiceModel.Channels;

namespace Opc.Ua.Bindings
{
    /// <summary>
    /// A dummy binding for the UA-TCP .NET implementation. 
    /// </summary>
    public class UaTcpBinding : BaseBinding
    {
        #region Constructors
        /// <summary>
        /// Initializes the binding.
        /// </summary>
        /// <param name="namespaceUris">The namespace uris.</param>
        /// <param name="factory">The factory.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="descriptions">The descriptions.</param>
        public UaTcpBinding(
            NamespaceTable               namespaceUris,
            EncodeableFactory            factory,
            EndpointConfiguration        configuration,
            params EndpointDescription[] descriptions)
        :
            base(namespaceUris, factory, configuration)
        {
            m_transport = new UaTcpTransportBindingElement();

            m_transport.MessageContext = base.MessageContext;
            m_transport.Configuration = configuration;

            if (descriptions != null)
            {
                m_transport.Descriptions = new EndpointDescriptionCollection(descriptions);
            }

            m_transport.MaxBufferPoolSize = Int32.MaxValue;
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
            get { return Utils.UriSchemeOpcTcp; } 
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
            elements.Add(m_transport);
            return elements.Clone();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// The service host which is configured to use the binding.
        /// </summary>
        /// <remarks>
        /// This property is *only* used by the UA TCP channel implementation to bypass the WCF channel 
        /// handling on the server side. It is a hack which is necessary because the WCF framework
        /// loses requests during stress testing. 
        /// </remarks>
        public virtual void SetServiceHost(ServiceHost serviceHost)
        {
            m_transport.ServiceHost = serviceHost;
        }
        #endregion

        #region Private Fields
        private UaTcpTransportBindingElement m_transport;
        #endregion
    }
}
