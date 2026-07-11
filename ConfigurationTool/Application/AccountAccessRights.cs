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
using System.Security.Principal;
using System.Security.AccessControl;

namespace Opc.Ua.Configuration
{
    /// <summary>
    /// The objects associated with an application that must be secured.
    /// </summary>
    [Flags]
    public enum SecuredObject
    {
        /// <summary>
        /// The executable file.
        /// </summary>
        ExecutableFile = 0x1,

        /// <summary>
        /// The app.config file.
        /// </summary>
        DotNetAppConfigFile = 0x2,

        /// <summary>
        /// The configuration file.
        /// </summary>
        ConfigurationFile = 0x4,

        /// <summary>
        /// The private key.
        /// </summary>
        PrivateKey = 0x8,

        /// <summary>
        /// The trust list.
        /// </summary>
        TrustList = 0x10,

        /// <summary>
        /// The HTTP endpoint.
        /// </summary>
        HttpEndpoint = 0x20
    }

    /// <summary>
    /// The rights associated with an application that are granted to an account.
    /// </summary>
    public class SecuredObjectAccessRights
    {
        /// <summary>
        /// The account or group.
        /// </summary>
        public IdentityReference Identity;

        /// <summary>
        /// The secured objects that are granted access.
        /// </summary>
        public SecuredObject AllowedObjects;

        /// <summary>
        /// The secured objects that are denied access.
        /// </summary>
        public SecuredObject DeniedObjects;
    }
}
