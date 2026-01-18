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

#pragma warning disable 0618

namespace Opc.Ua.Server
{    
#if LEGACY_CORENODEMANAGER
    /// <summary>
    /// An interface to an object that produces current data for multiple variables.
    /// </summary>
    [Obsolete("The IDataProducer interface is obsolete and is not supported. See Opc.Ua.Server.CustomNodeManager for a replacement.")]
    public interface IDataProducer
    {
        /// <summary>
        /// Reads the value for the variable from from the producer.
        /// </summary>
        /// <remarks>
        /// Returns true if the value/status/timestamp were changed.
        /// </remarks>
        bool ReadValue(
            NodeSource        source, 
            object            producerHandle,
            out object        value, 
            out DateTime      timestamp,
            out ServiceResult status);

        /// <summary>
        /// Writes the value for the variable from to the producer.
        /// </summary>
        ServiceResult WriteValue(
            NodeSource source, 
            object     producerHandle,
            object     value, 
            DateTime   timestamp, 
            StatusCode status);
    }
    
    /// <summary>
    /// An interface to an object that produces historical data for multiple variables.
    /// </summary>
    [Obsolete("The IDataHistoryProducer interface is obsolete and is not supported. See Opc.Ua.Server.CustomNodeManager for a replacement.")]
    public interface IDataHistoryProducer
    {                        
        /// <summary>
        /// Reads raw data from the historian.
        /// </summary>
        void ReadRaw(
            OperationContext          context,
            ReadRawModifiedDetails    details,
            TimestampsToReturn        timestampsToReturn, 
            bool                      releaseContinuationPoints, 
            IList<RequestHandle>      handles,
            IList<HistoryReadValueId> nodesToRead, 
            IList<HistoryReadResult>  results, 
            IList<ServiceResult>      errors);

        /// <summary>
        /// Reads raw data from the historian at the specified time.
        /// </summary>
        void ReadAtTime(
            OperationContext          context,
            ReadAtTimeDetails         details,
            TimestampsToReturn        timestampsToReturn, 
            bool                      releaseContinuationPoints, 
            IList<RequestHandle>      handles,
            IList<HistoryReadValueId> nodesToRead, 
            IList<HistoryReadResult>  results, 
            IList<ServiceResult>      errors);

        /// <summary>
        /// Reads processed data from the historian.
        /// </summary>
        void ReadProcessed(
            OperationContext          context,
            ReadProcessedDetails      details,
            TimestampsToReturn        timestampsToReturn, 
            bool                      releaseContinuationPoints, 
            IList<RequestHandle>      handles,
            IList<HistoryReadValueId> nodesToRead, 
            IList<HistoryReadResult>  results, 
            IList<ServiceResult>      errors);

        /// <summary>
        /// Updates raw data in the historian.
        /// </summary>
        void UpdateRaw(
            OperationContext           context,
            IList<RequestHandle>       handles,
            IList<UpdateDataDetails>   nodesToUpdate, 
            IList<HistoryUpdateResult> results, 
            IList<ServiceResult>       errors);
        
        /// <summary>
        /// Deletes raw data in the historian.
        /// </summary>
        void DeleteRaw(
            OperationContext                context,
            IList<RequestHandle>            handles,
            IList<DeleteRawModifiedDetails> nodesToDelete, 
            IList<HistoryUpdateResult>      results, 
            IList<ServiceResult>            errors);
        
        /// <summary>
        /// Deletes raw data in the historian at the specified time.
        /// </summary>
        void DeleteAtTime(
            OperationContext           context,
            IList<RequestHandle>       handles,
            IList<DeleteAtTimeDetails> nodesToDelete, 
            IList<HistoryUpdateResult> results, 
            IList<ServiceResult>       errors);
    }
#endif
}
