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
using System.Configuration;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;

using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.Runtime.Serialization;

using System.Security.Cryptography.X509Certificates;

using Opc.Ua.Bindings;
using Opc.Ua.Server;

namespace Opc.Ua.Sample
{
    /// <summary>
    /// A factory used by the WCF framework to create new instances of UA servers.
    /// </summary>
	public class ServiceHostFactory : ServiceHostFactoryBase
	{
        #region Overridden Members
        /// <summary>
        /// Creates a new instance of a service host.
        /// </summary>
	    public override ServiceHostBase CreateServiceHost(string constructorString, Uri[] baseAddresses)
	    {
            // load the configuration.
            SampleConfiguration configuration = SampleConfiguration.Load("Opc.Ua.Server", ApplicationType.Server);

            // create the object that implements the server.
            SampleServer server = new SampleServer();

            // return the default host.
            return server.Start(configuration, baseAddresses);
        }
        #endregion
    }
}
