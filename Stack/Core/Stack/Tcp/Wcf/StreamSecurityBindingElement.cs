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
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.Text;

namespace Opc.Ua.Bindings
{    
    /// <summary>
    /// A class that publishes the secuirty capabilities for a binding element.
    /// </summary>
    public class SecurityCapabilities : ISecurityCapabilities
    {
        #region ISecurityCapabilities Members
        /// <summary cref="ISecurityCapabilities.SupportedRequestProtectionLevel" />
        public System.Net.Security.ProtectionLevel SupportedRequestProtectionLevel
        {
            get { return System.Net.Security.ProtectionLevel.EncryptAndSign; }
        }

        /// <summary cref="ISecurityCapabilities.SupportedResponseProtectionLevel" />
        public System.Net.Security.ProtectionLevel SupportedResponseProtectionLevel
        {
            get { return System.Net.Security.ProtectionLevel.EncryptAndSign; }
        }

        /// <summary cref="ISecurityCapabilities.SupportsClientAuthentication" />
        public bool SupportsClientAuthentication
        {
            get { return false; }
        }

        /// <summary cref="ISecurityCapabilities.SupportsClientWindowsIdentity" />
        public bool SupportsClientWindowsIdentity
        {
            get { return false; }
        }

        /// <summary cref="ISecurityCapabilities.SupportsServerAuthentication" />
        public bool SupportsServerAuthentication
        {
            get { return false; }
        }
        #endregion
    }
}
