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
using System.IO;
using System.Runtime.Serialization;
using Opc.Ua.Configuration;

namespace Opc.Ua.Configuration
{
    /// <summary>
    /// Stores information about an account.
    /// </summary>
    [DataContract(Namespace="http://opcfoundation.org/UA/SDK/CertificateToolConfiguration.xsd")]
    public class CertificateToolConfiguration
    {
        #region Constructors
        /// <summary>
        /// Creates an empty object.
        /// </summary>
        public CertificateToolConfiguration()
        {
            m_applications = new List<ConfigureableApplication>();
            m_stores = new List<CertificateStoreIdentifier>();
        }

        /// <summary>
        /// Initializes the object during deserialization.
        /// </summary>
        [OnDeserializing()]
        private void Initialize(StreamingContext context)
        {
            m_applications = new List<ConfigureableApplication>();
            m_stores = new List<CertificateStoreIdentifier>();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// The applications known to the tool.
        /// </summary>
        [DataMember(Order = 1)]
        public List<ConfigureableApplication> Applications
        {
            get
            { 
                return m_applications;  
            } 
            
            set 
            { 
                m_applications = value; 

                if (m_applications == null)
                {
                    m_applications = new List<ConfigureableApplication>();
                }
            }
        }

        /// <summary>
        /// The certificate stores known to the tool.
        /// </summary>
        [DataMember(Order = 2)]
        public List<CertificateStoreIdentifier> Stores
        {
            get
            { 
                return m_stores;  
            } 
            
            set 
            { 
                m_stores = value; 

                if (m_stores == null)
                {
                    m_stores = new List<CertificateStoreIdentifier>();
                }
            }
        }
        #endregion 

        #region Private Fields
        private List<ConfigureableApplication> m_applications;
        private List<CertificateStoreIdentifier> m_stores;
        #endregion 
    }
    
    #region ConfigureableApplication Class
    /// <summary>
    /// An application which can be configured by the certificate tool.
    /// </summary>
    [DataContract(Namespace="http://opcfoundation.org/UA/SDK/CertificateToolConfiguration.xsd")]
    public class ConfigureableApplication
    {
        #region Public Properties
        /// <summary>
        /// The location of the configuration file for the application.
        /// </summary>
        [DataMember(Order = 1)]
        public string ConfigurationFile
        {
            get { return m_configurationFile;  } 
            set { m_configurationFile = value; }
        }
        
        /// <summary>
        /// The assembly qualified type name for a class that implements ISecurityConfiguration
        /// </summary>
        [DataMember(Order = 2)]
        public string LoaderType
        {
            get { return m_loaderType;  } 
            set { m_loaderType = value; }
        }
        #endregion 

        #region Private Fields
        private string m_configurationFile;
        private string m_loaderType;
        #endregion 
    }
    #endregion 
}
