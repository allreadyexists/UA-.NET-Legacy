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
using System.ServiceModel.Channels;

namespace Opc.Ua
{
    /// <summary>
    /// Describes how to connect to an endpoint.
    /// </summary>
    public partial class EndpointConfiguration
    {
        #region Constructors
        /// <summary>
        /// Creates an instance of a configuration with reasonable default values.
        /// </summary>
        public static EndpointConfiguration Create()
        {
            EndpointConfiguration configuration = new EndpointConfiguration();

            configuration.OperationTimeout      = 120000;
            configuration.UseBinaryEncoding     = true;
            configuration.MaxArrayLength        = UInt16.MaxValue;
            configuration.MaxByteStringLength   = UInt16.MaxValue*16;
            configuration.MaxMessageSize        = UInt16.MaxValue*64;
            configuration.MaxStringLength       = UInt16.MaxValue;
            configuration.MaxBufferSize         = UInt16.MaxValue;
            configuration.ChannelLifetime       = 120000;
            configuration.SecurityTokenLifetime = 3600000;

            return configuration;
        }

        /// <summary>
        /// Creates an instance of a configuration with reasonable default values.
        /// </summary>
        public static EndpointConfiguration Create(ApplicationConfiguration applicationConfiguration)
        {
            if (applicationConfiguration == null || applicationConfiguration.TransportQuotas == null)
            {
                return Create();
            }

            EndpointConfiguration configuration = new EndpointConfiguration();
            
            configuration.OperationTimeout      = applicationConfiguration.TransportQuotas.OperationTimeout;
            configuration.UseBinaryEncoding     = true;
            configuration.MaxArrayLength        = applicationConfiguration.TransportQuotas.MaxArrayLength;
            configuration.MaxByteStringLength   = applicationConfiguration.TransportQuotas.MaxByteStringLength;
            configuration.MaxMessageSize        = applicationConfiguration.TransportQuotas.MaxMessageSize;
            configuration.MaxStringLength       = applicationConfiguration.TransportQuotas.MaxStringLength;
            configuration.MaxBufferSize         = applicationConfiguration.TransportQuotas.MaxBufferSize;
            configuration.ChannelLifetime       = applicationConfiguration.TransportQuotas.ChannelLifetime;
            configuration.SecurityTokenLifetime = applicationConfiguration.TransportQuotas.SecurityTokenLifetime; 

            return configuration;
        }
        #endregion
    }
}
