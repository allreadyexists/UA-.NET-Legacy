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
using System.Configuration;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;

namespace Opc.Ua
{
    #region CertificateTrustList Class
    /// <summary>
    /// A list of trusted certificates.
    /// </summary>
    /// <remarks>
    /// Administrators can create a list of trusted certificates by designating all certificates 
    /// in a particular certificate store as trusted and/or by explictly specifying a list of 
    /// individual certificates.
    /// 
    /// A trust list can contain either instance certificates or certification authority certificates.
    /// If the list contains instance certificates the application will trust peers that use the
    /// instance certificate (provided the ApplicationUri and HostName match the certificate).
    /// 
    /// If the list contains certification authority certificates then the application will trust
    /// peers that have certificates issued by one of the authorities.
    /// 
    /// Any certificate could be revoked by the issuer (CAs may issue certificates for other CAs).
    /// The RevocationMode specifies whether this check should be done each time a certificate
    /// in the list are used.
    /// </remarks>
    public partial class CertificateTrustList : CertificateStoreIdentifier
    {
        #region Public Methods
        /// <summary>
        /// Returns the certificates in the trust list.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public X509Certificate2Collection GetCertificates()
        {
            X509Certificate2Collection collection = new X509Certificate2Collection();

            CertificateStoreIdentifier id = new CertificateStoreIdentifier();
            
            id.StoreType = this.StoreType;
            id.StorePath = this.StorePath;

            if (!String.IsNullOrEmpty(id.StorePath))
            {
                try
                {
                    ICertificateStore store = id.OpenStore();
                    
                    try
                    {
                        collection = store.Enumerate();
                    }
                    finally
                    {
                        store.Close();
                    }
                }
                catch (Exception)
                {
                    Utils.Trace("Could not load certificates from store: {0}.", this.StorePath);
                }
            }
            
            foreach (CertificateIdentifier trustedCertificate in TrustedCertificates)
            {
                X509Certificate2 certificate = trustedCertificate.Find();

                if (certificate != null)
                {
                    collection.Add(certificate);
                }
            }

            return collection;
        }
        #endregion
    }
    #endregion
}
