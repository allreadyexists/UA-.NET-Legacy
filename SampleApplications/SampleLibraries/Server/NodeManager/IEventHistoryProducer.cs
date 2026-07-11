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
    /// An interface to a source that stores the event history associated many Objects or Views.
    /// </summary>
    [Obsolete("The IEventHistoryProducer interface is obsolete and is not supported. See Opc.Ua.Server.CustomNodeManager for a replacement.")]
    public interface IEventHistoryProducer
    {                        
        /// <summary>
        /// Reads events from the historian.
        /// </summary>
        void ReadEvents(
            OperationContext          context,
            ReadEventDetails          details,
            TimestampsToReturn        timestampsToReturn, 
            bool                      releaseContinuationPoints, 
            IList<RequestHandle>      handles,
            IList<HistoryReadValueId> nodesToRead, 
            IList<HistoryReadResult>  results, 
            IList<ServiceResult>      errors);

        /// <summary>
        /// Updates events in the historian.
        /// </summary>
        void UpdateEvents(
            OperationContext           context,
            IList<RequestHandle>       handles,
            IList<UpdateEventDetails>  nodesToUpdate, 
            IList<HistoryUpdateResult> results, 
            IList<ServiceResult>       errors);
                
        /// <summary>
        /// Deletes events in the historian.
        /// </summary>
        void DeleteEvents(
            OperationContext           context,
            IList<RequestHandle>       handles,
            IList<DeleteEventDetails>  eventsToDelete, 
            IList<HistoryUpdateResult> results, 
            IList<ServiceResult>       errors);
    }
#endif
}
