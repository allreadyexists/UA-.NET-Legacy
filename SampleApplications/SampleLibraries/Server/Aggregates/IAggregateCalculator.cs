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

namespace Opc.Ua.Server
{
    /// <summary>
    /// An interface that captures the original active API of the AggregateCalculator class
    /// required to integrate with the subscription code.
    /// </summary>
    public interface IAggregateCalculator
    {
        /// <summary>
        /// The aggregate function applied by the calculator.
        /// </summary>
        NodeId AggregateId { get; }

        /// <summary>
        /// Pushes the next raw value into the stream.
        /// </summary>
        /// <param name="value">The data value to append to the stream.</param>
        /// <returns>True if successful, false if the source timestamp has been superceeded by values already in the stream.</returns>
        bool QueueRawValue(DataValue value);

        /// <summary>
        /// Returns the next processed value.
        /// </summary>
        /// <param name="returnPartial">If true a partial interval should be processed.</param>
        /// <returns>The processed value. Null if nothing available and returnPartial is false.</returns>
        DataValue GetProcessedValue(bool returnPartial);

        /// <summary>
        /// Returns true if the specified time is later than the end of the current interval.
        /// </summary>
        /// <remarks>Return true if time flows forward and the time is later than the end time.</remarks>
        bool HasEndTimePassed(DateTime currentTime);
    }
}
