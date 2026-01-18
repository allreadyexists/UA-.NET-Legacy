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
    public abstract class DurationAggregate : SteppedInterpolatingCalculator
    {
        protected abstract bool RightState(DataValue dv);

        protected override StatusCode ComputeStatus(IAggregationContext context, int numGood, int numBad, TimeSlice bucket)
        {
            StatusCode code = base.ComputeStatus(context, numGood, numBad, bucket);
            if (bucket.EarlyBound.Value == null || StatusCode.IsNotGood(bucket.EarlyBound.Value.StatusCode))
                code = StatusCodes.Uncertain;
            return code;
        }
        public override DataValue Compute(IAggregationContext context, TimeSlice bucket, AggregateState state)
        {
            int numGood = 0;
            int numBad = 0;
            DataValue previous = RightState(bucket.EarlyBound.Value) ? bucket.EarlyBound.Value : null;
            double total = 0.0;

            DataValue retval = new DataValue { SourceTimestamp = bucket.From };
            StatusCode code = StatusCodes.BadNoData;

            foreach (DataValue v in bucket.Values)
            {
                if (state.RawValueIsGood(v))
                {
                    numGood += 1;
                    if (previous != null)
                        total += (v.SourceTimestamp - previous.SourceTimestamp).TotalMilliseconds;
                    previous = RightState(v) ? v : null;
                }
                else
                {
                    numBad += 1;
                }
            }
            if (previous != null)
                total += (bucket.LateBound.Value.SourceTimestamp - previous.SourceTimestamp).TotalMilliseconds;
            retval.Value = total;
            code = ComputeStatus(context, numGood, numBad, bucket).Code;
            code.AggregateBits = AggregateBits.Calculated;
            if (bucket.Incomplete) code.AggregateBits |= AggregateBits.Partial;
            retval.StatusCode = code;
            return retval;
        }
    }

    public class DurationInState0Aggregate : DurationAggregate
    {
        protected override bool RightState(DataValue dv)
        {
            if(dv != null && dv.Value != null)
                return Convert.ToBoolean(dv.Value) == false;
            return false;
        }
    }
}
