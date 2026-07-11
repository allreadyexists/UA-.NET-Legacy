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
    public abstract class ComparisonAggregate : NonInterpolatingCalculator
    {
        protected abstract bool Comparison(DataValue value1, DataValue value2); // true if keep value1.

        public override DataValue Compute(IAggregationContext context, TimeSlice bucket, AggregateState state)
        {
            int numGood = 0;
            int numBad = 0;
            DataValue valueToKeep = new DataValue() { SourceTimestamp = bucket.From, StatusCode = StatusCodes.BadNoData };
            bool moreData = false;
            bool hasGoodData = false;
            foreach (DataValue dv in bucket.Values)
            {
                if (state.RawValueIsGood(dv))
                {
                    hasGoodData = true;
                    if (valueToKeep.StatusCode == StatusCodes.BadNoData)
                    {
                        valueToKeep = dv;
                    }
                    else
                    {
                        moreData = valueToKeep == dv;
                        if (Comparison(dv, valueToKeep))
                        {
                            valueToKeep = dv;
                        }
                    }
                    numGood++;
                }
                else
                {
                    numBad++;
                    if (!hasGoodData)
                        valueToKeep = dv;
                }
            }
            DataValue retval = valueToKeep.StatusCode == StatusCodes.BadNoData ? valueToKeep : (DataValue)valueToKeep.Clone();
            if (hasGoodData)
            {
                StatusCode code = StatusCodes.Good;
                code = ComputeStatus(context, numGood, numBad, bucket).Code;
                code.AggregateBits = moreData ? AggregateBits.ExtraData : AggregateBits.Raw;
                if (bucket.Incomplete) code.AggregateBits |= AggregateBits.Partial;
                retval.StatusCode = code;
            } // numGood = 0, hasGoodData = false beyond this point, i.e., no good data
            else if(numBad > 0)
            {
                retval.Value = null;
                retval.StatusCode = StatusCodes.Bad;
                retval.StatusCode = retval.StatusCode.SetAggregateBits(AggregateBits.Raw);
            }
            return retval;
        }
    }

    public class MinimumActualTimeAggregate : ComparisonAggregate
    {
        protected override bool Comparison(DataValue value1, DataValue value2)
        {
            return Convert.ToDouble(value1.Value) < Convert.ToDouble(value2.Value);
        }
    }

}
