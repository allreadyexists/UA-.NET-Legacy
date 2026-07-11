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
    /// An interface to an object monitors for events and reports them when they occur.
    /// </summary>
    [Obsolete("The IEventSource interface is obsolete and is not supported. See Opc.Ua.Server.CustomNodeManager for a replacement.")]
    public interface IEventSource
    {
        /// <summary>
        /// Subscribes/unsubscribes to events for the specified notifier.
        /// </summary>
        /// <remarks>
        /// This method may be called multiple times for the name monitoredItemId if the
        /// context for that MonitoredItem changes (i.e. UserIdentity and/or Locales).
        /// </remarks>
        void SubscribeToEvents(
            OperationContext    context,
            object              notifier, 
            uint                subscriptionId,
            IEventMonitoredItem monitoredItem,
            bool                unsubscribe);
        
        /// <summary>
        /// Subscribes/unsubscribes to all events from the source.
        /// </summary>
        /// <remarks>
        /// This method may be called multiple times for the name monitoredItemId if the
        /// context for that MonitoredItem changes (i.e. UserIdentity and/or Locales).
        /// </remarks>
        void SubscribeToAllEvents(            
            OperationContext    context,
            uint                subscriptionId,
            IEventMonitoredItem monitoredItem,
            bool                unsubscribe);

        /// <summary>
        /// Tells the source to refresh all conditions.
        /// </summary>
        void ConditionRefresh(            
            OperationContext           context,
            IList<IEventMonitoredItem> monitoredItems);
    }
#endif
}
