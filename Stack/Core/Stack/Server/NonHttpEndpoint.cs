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
using System.ServiceModel;

namespace Opc.Ua
{
    #if USE_WCF_FOR_UATCP
    /// <summary>
    /// A endpoint object used by clients to access a UA service via non-HTTP endpoints.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.CodeGenerator", "1.0.0.0")]
    [ServiceBehavior(Namespace = Namespaces.OpcUaWsdl, InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public partial class NonHttpSessionEndpoint : SessionEndpoint
    {
    }

    /// <summary>
    /// A endpoint object used by clients to access a UA service via non-HTTP endpoints.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.CodeGenerator", "1.0.0.0")]
    [ServiceBehavior(Namespace = Namespaces.OpcUaWsdl, InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public partial class NonHttpDiscoveryEndpoint : DiscoveryEndpoint
    {
    }
    #endif
}
