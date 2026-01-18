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
using System.Threading;
using System.Security.Principal;

namespace Opc.Ua.Server
{    
    /// <summary>
    /// An object that manages all events raised within the server.
    /// </summary>
    public class EventManager : IDisposable
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of a sampling group.
        /// </summary>
        public EventManager(IServerInternal server, uint maxQueueSize)
        {
            if (server == null) throw new ArgumentNullException("server");

            m_server = server;
            m_monitoredItems = new Dictionary<uint,IEventMonitoredItem>();
            m_maxEventQueueSize = maxQueueSize;
        }
        #endregion
                      
        #region IDisposable Members
        /// <summary>
        /// Frees any unmanaged resources.
        /// </summary>
        public void Dispose()
        {   
            Dispose(true);
        }

        /// <summary>
        /// An overrideable version of the Dispose.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {  
            if (disposing)
            {
                List<IEventMonitoredItem> monitoredItems = null;

                lock (m_lock)
                {
                    monitoredItems = new List<IEventMonitoredItem>(m_monitoredItems.Values);
                    m_monitoredItems.Clear();
                }

                foreach (IEventMonitoredItem monitoredItem in monitoredItems)
                {
                    Utils.SilentDispose(monitoredItem);
                }
            }
        }
        #endregion
                 
        #region Public Methods
        /// <summary>
        /// Reports an event.
        /// </summary>
        public static void ReportEvent(IFilterTarget e, IList<IEventMonitoredItem> monitoredItems)
        {
            if (e == null) throw new ArgumentNullException("e");
            
            foreach (IEventMonitoredItem monitoredItem in monitoredItems)
            {
                monitoredItem.QueueEvent(e);
            }
        }
        
        /// <summary>
        /// Creates a set of monitored items.
        /// </summary>
        [Obsolete("Replaced by variant that includes the publishingInterval")]
        public MonitoredItem CreateMonitoredItem(
            OperationContext           context,
            INodeManager               nodeManager,
            object                     handle,
            uint                       subscriptionId,
            uint                       monitoredItemId,
            TimestampsToReturn         timestampsToReturn,
            MonitoredItemCreateRequest itemToCreate,
            EventFilter                filter)
        {
            return CreateMonitoredItem(
                context,
                nodeManager,
                handle,
                subscriptionId,
                monitoredItemId,
                timestampsToReturn,
                0,
                itemToCreate,
                filter);
        }

        /// <summary>
        /// Creates a set of monitored items.
        /// </summary>
        public MonitoredItem CreateMonitoredItem(
            OperationContext           context,
            INodeManager               nodeManager,
            object                     handle,
            uint                       subscriptionId,
            uint                       monitoredItemId,
            TimestampsToReturn         timestampsToReturn,
            double                     publishingInterval,
            MonitoredItemCreateRequest itemToCreate,
            EventFilter                filter)
        {
            lock (m_lock)
            {
                // calculate sampling interval.
                double samplingInterval = itemToCreate.RequestedParameters.SamplingInterval;
                
                if (samplingInterval < 0)
                {
                    samplingInterval = publishingInterval;
                }
                
                // limit the queue size.
                uint queueSize = itemToCreate.RequestedParameters.QueueSize;

                if (queueSize > m_maxEventQueueSize)
                {
                    queueSize = m_maxEventQueueSize;
                }
                
                // create the monitored item.
                MonitoredItem monitoredItem = new MonitoredItem(
                    m_server,
                    nodeManager,
                    handle,
                    subscriptionId,
                    monitoredItemId,
                    context.Session,
                    itemToCreate.ItemToMonitor,
                    context.DiagnosticsMask,
                    timestampsToReturn,
                    itemToCreate.MonitoringMode,
                    itemToCreate.RequestedParameters.ClientHandle,
                    filter,
                    filter,
                    null,
                    samplingInterval,
                    queueSize,
                    itemToCreate.RequestedParameters.DiscardOldest,
                    MinimumSamplingIntervals.Continuous);

                // save the monitored item.
                m_monitoredItems.Add(monitoredItemId, monitoredItem);

                return monitoredItem;
            }
        }
                
        /// <summary>
        /// Modifies a monitored item.
        /// </summary>
        public void ModifyMonitoredItem(
            OperationContext           context,
            IEventMonitoredItem        monitoredItem,
            TimestampsToReturn         timestampsToReturn,
            MonitoredItemModifyRequest itemToModify,
            EventFilter                filter)
        {
            lock (m_lock)
            {
                // should never be called with items that it does not own.
                if (!m_monitoredItems.ContainsKey(monitoredItem.Id))
                {
                    return;
                }

                // limit the queue size.
                uint queueSize = itemToModify.RequestedParameters.QueueSize;

                if (queueSize > m_maxEventQueueSize)
                {
                    queueSize = m_maxEventQueueSize;
                }

                // modify the attributes.
                monitoredItem.ModifyAttributes(
                    context.DiagnosticsMask,
                    timestampsToReturn,
                    itemToModify.RequestedParameters.ClientHandle,
                    filter,
                    filter,
                    null,
                    itemToModify.RequestedParameters.SamplingInterval,
                    queueSize,
                    itemToModify.RequestedParameters.DiscardOldest);
            }           
        }

        /// <summary>
        /// Deletes a monitored item.
        /// </summary>
        public void DeleteMonitoredItem(uint monitoredItemId)
        {
            lock (m_lock)
            {
                m_monitoredItems.Remove(monitoredItemId);
            }
        }

        /// <summary>
        /// Returns the currently active monitored items. 
        /// </summary>
        public IList<IEventMonitoredItem> GetMonitoredItems()
        {
            lock (m_lock)
            {
                return new List<IEventMonitoredItem>(m_monitoredItems.Values);
            }
        }
        #endregion

        #region Private Fields
        private object m_lock = new object();
        private IServerInternal m_server;
        private Dictionary<uint, IEventMonitoredItem> m_monitoredItems;
        private uint m_maxEventQueueSize;
        #endregion
    }
}
