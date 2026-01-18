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
using System.Text;
using System.Reflection;
using System.ServiceModel.Security;

namespace Opc.Ua.Sample
{
    /// <summary>
    /// Defines constants for key security policies.
    /// </summary>
    public static class SecurityPolicies
    {
        #region Public Constants
        /// <summary>
        /// The base URI for all policy URIs.
        /// </summary>
        public const string BaseUri = "http://opcfoundation.org/UA/SecurityPolicy#";

        /// <summary>
        /// The URI for a policy that uses no security.
        /// </summary>
        public const string None = BaseUri + "None";

        /// <summary>
        /// The URI for the Basic128 security policy.
        /// </summary>
        public const string Basic128 = BaseUri + "Basic128";

        /// <summary>
        /// The URI for the Basic128Rsa15 security policy.
        /// </summary>
        public const string Basic128Rsa15 = BaseUri + "Basic128Rsa15";

        /// <summary>
        /// The URI for the Basic192 security policy.
        /// </summary>
        public const string Basic192 = BaseUri + "Basic192";

        /// <summary>
        /// The URI for the Basic192Rsa15 security policy.
        /// </summary>
        public const string Basic192Rsa15 = BaseUri + "Basic192Rsa15";

        /// <summary>
        /// The URI for the Basic256 security policy.
        /// </summary>
        public const string Basic256 = BaseUri + "Basic256";

        /// <summary>
        /// The URI for the Basic256Rsa15 security policy.
        /// </summary>
        public const string Basic256Rsa15 = BaseUri + "Basic256Rsa15";

        /// <summary>
        /// The URI for the Basic256Sha256 security policy.
        /// </summary>
        public const string Basic256Sha256 = BaseUri + "Basic256Sha256";
        #endregion

        #region Static Methods
        /// <summary>
        /// Returns the uri associated with the display name.
        /// </summary>
        public static string GetUri(string displayName)
        {
            FieldInfo[] fields = typeof(SecurityPolicies).GetFields(BindingFlags.Public | BindingFlags.Static);

            foreach (FieldInfo field in fields)
            {
                if (field.Name == displayName)
                {
                    return (string)field.GetValue(typeof(SecurityPolicies));
                }
            }

            return null;
        }

        /// <summary>
        /// Returns a display name for a security policy uri.
        /// </summary>
        public static string GetDisplayName(string policyUri)
        {
            FieldInfo[] fields = typeof(SecurityPolicies).GetFields(BindingFlags.Public | BindingFlags.Static);

            foreach (FieldInfo field in fields)
            {
                if (policyUri == (string)field.GetValue(typeof(SecurityPolicies)))
                {
                    return field.Name;
                }
            }

            return null;
        }

        /// <summary>
        /// Returns the display names for all security policy uris.
        /// </summary>
        public static string[] GetDisplayNames()
        {
            FieldInfo[] fields = typeof(SecurityPolicies).GetFields(BindingFlags.Public | BindingFlags.Static);

            int ii = 0;

            string[] names = new string[fields.Length];

            foreach (FieldInfo field in fields)
            {
                names[ii++] = field.Name;
            }

            return names;
        }

        /// <summary>
        /// Returns a WCF SecurityAlgorithmSuite for a UA Security Policy
        /// </summary>
        public static SecurityAlgorithmSuite ToSecurityAlgorithmSuite(string policyUri)
        {
            switch (policyUri)
            {
                case None:
                {
                    return SecurityAlgorithmSuite.Default;
                }

                case Basic128:
                {
                    return SecurityAlgorithmSuite.Basic128;
                }

                case Basic128Rsa15:
                {
                    return SecurityAlgorithmSuite.Basic128Rsa15;
                }

                case Basic192:
                {
                    return SecurityAlgorithmSuite.Basic192;
                }

                case Basic192Rsa15:
                {
                    return SecurityAlgorithmSuite.Basic192Rsa15;
                }

                case Basic256:
                {
                    return SecurityAlgorithmSuite.Basic256;
                }

                case Basic256Rsa15:
                {
                    return SecurityAlgorithmSuite.Basic256Rsa15;
                }

                case Basic256Sha256:
                {
                    return SecurityAlgorithmSuite.Basic256Sha256;
                }

                default:
                {
                    throw new ApplicationException(String.Format(
                        "{0} is not a valid UA security policy URI.", 
                        policyUri));
                }
            }
        }
        #endregion
    }

    /// <summary>
    /// Common profiles that UA applications may support.
    /// </summary>
    public static class Profiles
    {
        /// <summary>
        /// Communicates with UA TCP, UA Security and UA Binary.
        /// </summary>
        public const string UaTcpTransport = "http://opcfoundation.org/UA/profiles/transport/uatcp";

        /// <summary>
        /// Communicates with SOAP 1.2, WS Security and UA XML.
        /// </summary>
        public const string WsHttpXmlTransport = "http://opcfoundation.org/UA/profiles/transport/wsxml";

        /// <summary>
        /// Communicates with SOAP 1.2, WS Security and UA XML or UA Binary.
        /// </summary>
        public const string WsHttpXmlOrBinaryTransport = "http://opcfoundation.org/UA/profiles/transport/wsxmlorbinary";

        /// <summary>
        /// Communicates with SOAP 1.2, WS Security and UA Binary.
        /// </summary>
        public const string WsHttpBinaryTransport = "http://opcfoundation.org/UA/profiles/transport/wsbinary";
    }
}
