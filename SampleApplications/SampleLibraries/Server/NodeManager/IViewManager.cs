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
using System.Reflection;
using System.Collections.Generic;

namespace Opc.Ua.Server
{    
#if LEGACY_CORENODEMANAGER
    /// <summary>
    /// An interface to an object manages one or more views.
    /// </summary>
    [Obsolete("The IViewManager interface is obsolete and is not supported. See Opc.Ua.Server.CustomNodeManager for a replacement.")]
    public interface IViewManager
    {                
        /// <summary>
        /// Determines whether a node is in a view.
        /// </summary>
        bool IsNodeInView(ViewDescription description, NodeId nodeId);
        
        /// <summary>
        /// Determines whether a reference is in a view.
        /// </summary>
        bool IsReferenceInView(ViewDescription description, ReferenceDescription reference);
    }
#endif
}
