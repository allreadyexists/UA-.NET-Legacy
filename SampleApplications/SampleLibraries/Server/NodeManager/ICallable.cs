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
    /// An interface to a object that exposes methods
    /// </summary>
    [Obsolete("The ICallable interface is obsolete and is not supported. See Opc.Ua.Server.CustomNodeManager for a replacement.")]
    public interface ICallable
    {                        
        /// <summary>
        /// Calls a method defined on a object.
        /// </summary>
        /// <remarks>
        /// The caller ensures that there are the correct number of input arguments and that they
        /// have the correct data type and array size. The implementor may return other validation 
        /// errors for input arguments.
        /// 
        /// Arguments that were not specified are passed as null.
        /// 
        /// If an input argument is invalid the implementor must return BadInvalidArgument and set 
        /// the appropriate errors in the argumentErrors list.
        /// </remarks>
        ServiceResult Call(
            OperationContext     context, 
            NodeId               methodId, 
            object               methodHandle, 
            NodeId               objectId, 
            IList<object>        inputArguments,
            IList<ServiceResult> argumentErrors, 
            IList<object>        outputArguments);
    }
#endif
}
