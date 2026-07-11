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
using System.Globalization;

namespace Opc.Ua.Server
{
    /*
     * To correctly get the interpolated value, the raw data passed to this aggregator needs to have "GOOD" boundary values. That means, it's 
     * the user responsibility to find out a good value before/after the period of interest if the bounding values of that period are not good.
     */
    public class InterpolativeAggregate : FloatInterpolatingCalculator
    {
        public override void UpdateBoundingValues(TimeSlice bucket, AggregateState state)
        {
            base.UpdateBoundingValues(bucket, state);
            UpdatePriorPoint(bucket.EarlyBound, state);
        }
        public override DataValue Compute(IAggregationContext context, TimeSlice bucket, AggregateState state)
        {
            DataValue retval = new DataValue { SourceTimestamp = bucket.From };
            StatusCode code = StatusCodes.BadNoData;
            DataValue boundValue = context.IsReverseAggregation ? bucket.LateBound.Value : bucket.EarlyBound.Value;
            if (boundValue != null)
            {
                code = bucket.EarlyBound.Value.StatusCode.Code;
                code.AggregateBits = bucket.EarlyBound.Value.StatusCode.AggregateBits;
                retval.Value = Convert.ToDouble(bucket.EarlyBound.Value.Value, CultureInfo.InvariantCulture);
            }
            if (bucket.Incomplete) code.AggregateBits |= AggregateBits.Partial;
            retval.StatusCode = code;
            return retval;
        }
    }

}
