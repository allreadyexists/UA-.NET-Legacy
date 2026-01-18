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

namespace Opc.Ua.Security
{
    /// <summary>
    /// Implemented by types that have knownledge of an application configuration.
    /// </summary>
    public interface ISecurityConfigurationManager
    {
        /// <summary>
        /// Exports the security configuration for an application identified by a file or url.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>The security configuration.</returns>
        SecuredApplication ReadConfiguration(string filePath);

        /// <summary>
        /// Updates the security configuration for an application identified by a file or url.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="configuration">The configuration.</param>
        void WriteConfiguration(string filePath, SecuredApplication configuration);
    }

    #region SecurityConfigurationManagerFactory Class
    /// <summary>
    /// A class used to create instances of ISecurityConfigurationManager.
    /// </summary>
    public static class SecurityConfigurationManagerFactory
    {
        /// <summary>
        /// Returns an instance of the type identified by the assembly qualified name.
        /// </summary>
        /// <param name="typeName">Name of the type.</param>
        /// <returns>The new instance.</returns>
        public static ISecurityConfigurationManager CreateInstance(string typeName)
        {
            if (String.IsNullOrEmpty(typeName))
            {
                return new SecurityConfigurationManager();
            }

            Type type = Type.GetType(typeName);

            if (type == null)
            {
                throw ServiceResultException.Create(
                    StatusCodes.BadNotSupported,
                    "Cannot load type: {0}",
                    typeName);
            }

            ISecurityConfigurationManager configuration = Activator.CreateInstance(type) as ISecurityConfigurationManager;

            if (configuration == null)
            {
                throw ServiceResultException.Create(
                    StatusCodes.BadNotSupported,
                    "Type does not support the ISecurityConfigurationManager interface: {0}",
                    typeName);
            }

            return configuration;
        }
    }
    #endregion
}
