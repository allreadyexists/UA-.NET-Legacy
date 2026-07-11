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
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using Opc.Ua.Bindings;

namespace Opc.Ua
{
    /// <summary>
	///  A channel object used by clients to access a UA service.
	/// </summary>
    public partial class SessionChannel 
    {
        #region Constructors
        /// <summary>
        /// Creates a new transport channel that supports the ISessionChannel service contract.
        /// </summary>
        /// <param name="configuration">The application configuration.</param>
        /// <param name="description">The description for the endpoint.</param>
        /// <param name="endpointConfiguration">The configuration to use with the endpoint.</param>
        /// <param name="clientCertificate">The client certificate.</param>
        /// <param name="messageContext">The message context to use when serializing the messages.</param>
        /// <returns></returns>
        public static ITransportChannel Create(
            ApplicationConfiguration configuration,
            EndpointDescription description,
            EndpointConfiguration endpointConfiguration,
            X509Certificate2 clientCertificate,
            ServiceMessageContext messageContext)
        {
            // create a UA binary channel.
            ITransportChannel channel = CreateUaBinaryChannel(
                configuration,
                description,
                endpointConfiguration,
                clientCertificate,
                messageContext);
            
            #if !SILVERLIGHT
            // create a WCF XML channel.
            if (channel == null)
            {
                Uri endpointUrl = new Uri(description.EndpointUrl);
                BindingFactory bindingFactory = BindingFactory.Create(configuration, messageContext);
                Binding binding = bindingFactory.Create(endpointUrl.Scheme, description, endpointConfiguration);

                SessionChannel wcfXmlChannel = new SessionChannel();

                wcfXmlChannel.Initialize(
                    configuration,
                    description,
                    endpointConfiguration,
                    binding,
                    clientCertificate,
                    null);

                channel = wcfXmlChannel;
            }
            #endif
            
            return channel;
        }

        /// <summary>
        /// Creates a new transport channel that supports the ISessionChannel service contract.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="description">The description.</param>
        /// <param name="endpointConfiguration">The endpoint configuration.</param>
        /// <param name="clientCertificates">The client certificates.</param>
        /// <param name="messageContext">The message context.</param>
        /// <returns></returns>
        public static ITransportChannel Create(
            ApplicationConfiguration configuration,
            EndpointDescription description,
            EndpointConfiguration endpointConfiguration,
            X509Certificate2Collection clientCertificates,
            ServiceMessageContext messageContext)
        {
            // create a UA binary channel.
            ITransportChannel channel = CreateUaBinaryChannel(
                configuration,
                description,
                endpointConfiguration,
                clientCertificates,
                messageContext);

#if !SILVERLIGHT
            // create a WCF XML channel.
            if (channel == null)
            {
                Uri endpointUrl = new Uri(description.EndpointUrl);
                BindingFactory bindingFactory = BindingFactory.Create(configuration, messageContext);
                Binding binding = bindingFactory.Create(endpointUrl.Scheme, description, endpointConfiguration);

                SessionChannel wcfXmlChannel = new SessionChannel();

                wcfXmlChannel.Initialize(
                    configuration,
                    description,
                    endpointConfiguration,
                    binding,
                    clientCertificates,
                    null);

                channel = wcfXmlChannel;
            }
#endif

            return channel;
        }
        

        #if !SILVERLIGHT
        /// <summary>
        /// Initializes a channel from the EndpointDescription and loads the behavoir from the configuration file.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="description">The description.</param>
        /// <param name="configurationName">Name of the configuration.</param>
        /// <returns></returns>
        [Obsolete("Must use the version that returns a ITransportChannel object.")]
        public static SessionChannel Create(
            ApplicationConfiguration configuration,
            EndpointDescription      description,
            string                   configurationName)
        {
            return Create(configuration, description, null, BindingFactory.Default, null, configurationName);
        }

        /// <summary>
        /// Initializes a channel from the EndpointDescription and loads the behavoir from the configuration file.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="description">The description.</param>
        /// <param name="endpointConfiguration">The endpoint configuration.</param>
        /// <param name="configurationName">Name of the configuration.</param>
        /// <returns></returns>
        [Obsolete("Must use the version that returns a ITransportChannel object.")]
        public static SessionChannel Create(
            ApplicationConfiguration configuration,
            EndpointDescription      description,
            EndpointConfiguration    endpointConfiguration,
            string                   configurationName)
        {
            return Create(configuration, description, endpointConfiguration, BindingFactory.Default, null, configurationName);
        }

        /// <summary>
        /// Initializes a channel from the EndpointDescription.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="description">The description.</param>
        /// <param name="clientCertificate">The client certificate.</param>
        /// <returns></returns>
        [Obsolete("Must use the version that returns a ITransportChannel object.")]
        public static SessionChannel Create(
            ApplicationConfiguration configuration,
            EndpointDescription      description,
            X509Certificate2         clientCertificate)
        {
            return Create(configuration, description, null, BindingFactory.Default, clientCertificate, null);
        }

        /// <summary>
        /// Initializes a channel from the EndpointDescription.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="description">The description.</param>
        /// <param name="endpointConfiguration">The endpoint configuration.</param>
        /// <param name="clientCertificate">The client certificate.</param>
        /// <returns></returns>
        [Obsolete("Must use the version that returns a ITransportChannel object.")]
        public static SessionChannel Create(
            ApplicationConfiguration configuration,
            EndpointDescription      description,
            EndpointConfiguration    endpointConfiguration,
            X509Certificate2         clientCertificate)
        {
            return Create(configuration, description, endpointConfiguration, BindingFactory.Default, clientCertificate, null);
        }

        /// <summary>
        /// Initializes a channel from the EndpointDescription and loads the behavoir from the configuration file.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="description">The description.</param>
        /// <param name="endpointConfiguration">The endpoint configuration.</param>
        /// <param name="bindingFactory">The binding factory.</param>
        /// <param name="clientCertificate">The client certificate.</param>
        /// <param name="configurationName">Name of the configuration.</param>
        /// <returns></returns>
        [Obsolete("Must use the version that returns a ITransportChannel object.")]
        public static SessionChannel Create(
            ApplicationConfiguration configuration,
            EndpointDescription      description,
            EndpointConfiguration    endpointConfiguration,
            BindingFactory           bindingFactory,
            X509Certificate2         clientCertificate,
            string                   configurationName)
        {
            if (description == null)    throw new ArgumentNullException("description");
            if (configuration == null)  throw new ArgumentNullException("configuration");
            if (bindingFactory == null) throw new ArgumentNullException("bindingFactory");
            
            Uri uri = new Uri(description.EndpointUrl);
        
            Binding binding = bindingFactory.Create(uri.Scheme, description, endpointConfiguration); 
          
            return Create(configuration, description, endpointConfiguration, binding, clientCertificate, configurationName);
        }

        /// <summary>
        /// Initializes a channel with the specified Binding and loads the behavoir from the configuration file.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="description">The description.</param>
        /// <param name="endpointConfiguration">The endpoint configuration.</param>
        /// <param name="binding">The binding.</param>
        /// <param name="clientCertificate">The client certificate.</param>
        /// <param name="configurationName">Name of the configuration.</param>
        /// <returns></returns>
        [Obsolete("Must use the version that returns a ITransportChannel object.")]
        public static SessionChannel Create(
            ApplicationConfiguration configuration,
            EndpointDescription      description,
            EndpointConfiguration    endpointConfiguration,
            Binding                  binding,
            X509Certificate2         clientCertificate,
            string                   configurationName)
        {
            SessionChannel channel = new SessionChannel();
            
            channel.Initialize(
                configuration,
                description,
                endpointConfiguration,
                binding,
                clientCertificate,
                configurationName);
            
            return channel;
        }
#endif
        #endregion
    }
}
