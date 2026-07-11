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
using System.Runtime.Serialization;
using System.Collections.Generic;
using Opc.Ua.Server;

namespace Quickstarts.ComDataAccessServer
{
    /// <summary>
    /// Stores the configuration for UA that wraps a COM server. 
    /// </summary>
    [DataContract(Namespace = Namespaces.ComInterop)]
    public class ComWrapperServerConfiguration
    {
        #region Constructors
        /// <summary>
        /// The default constructor.
        /// </summary>
        public ComWrapperServerConfiguration()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes the object during deserialization.
        /// </summary>
        [OnDeserializing()]
        private void Initialize(StreamingContext context)
        {
            Initialize();
        }

        /// <summary>
        /// Sets private members to default values.
        /// </summary>
        private void Initialize()
        {
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// The list of COM servers wrapped by the UA server. 
        /// </summary>
        [DataMember(Order=1)]
        public ComClientConfigurationCollection WrappedServers
        {
            get { return m_wrappedServers; }
            set { m_wrappedServers = value; }
        }     
        #endregion

        #region Private Members
        private ComClientConfigurationCollection m_wrappedServers;
        #endregion
    }
}
