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
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Opc.Ua.Security.Certificates
{
    /// <summary>
    /// Supporting functions for X509 extensions.
    /// </summary>
    public static class X509Extensions
    {
        /// <summary>
        /// Find a typed extension in a certificate.
        /// </summary>
        /// <typeparam name="T">The type of the extension.</typeparam>
        /// <param name="certificate">The certificate with extensions.</param>
        public static T FindExtension<T>(this X509Certificate2 certificate) where T : X509Extension
        {
            return FindExtension<T>(certificate.Extensions);
        }

        /// <summary>
        /// Find a typed extension in a extension collection.
        /// </summary>
        /// <typeparam name="T">The type of the extension.</typeparam>
        /// <param name="extensions">The extensions to search.</param>
        public static T FindExtension<T>(this X509ExtensionCollection extensions) where T : X509Extension
        {
            if (extensions == null) throw new ArgumentNullException("extensions");
            lock (extensions.SyncRoot)
            {
                // search known custom extensions
                if (typeof(T) == typeof(X509AuthorityKeyIdentifierExtension))
                {
                    var extension = extensions.Cast<X509Extension>().FirstOrDefault(e => (
                        e.Oid.Value == X509AuthorityKeyIdentifierExtension.AuthorityKeyIdentifierOid ||
                        e.Oid.Value == X509AuthorityKeyIdentifierExtension.AuthorityKeyIdentifier2Oid)
                    );
                    if (extension != null)
                    {
                        return new X509AuthorityKeyIdentifierExtension(extension, extension.Critical) as T;
                    }
                }

                if (typeof(T) == typeof(X509SubjectAltNameExtension))
                {
                    var extension = extensions.Cast<X509Extension>().FirstOrDefault(e => (
                        e.Oid.Value == X509SubjectAltNameExtension.SubjectAltNameOid ||
                        e.Oid.Value == X509SubjectAltNameExtension.SubjectAltName2Oid)
                    );
                    if (extension != null)
                    {
                        return new X509SubjectAltNameExtension(extension, extension.Critical) as T;
                    }
                }

                // search builtin extension
                return extensions.OfType<T>().FirstOrDefault();
            }
        }
    }
}
