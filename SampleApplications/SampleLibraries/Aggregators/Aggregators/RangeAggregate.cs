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
    public class RangeAggregate : NonInterpolatingCalculator
    {

        public override DataValue Compute(IAggregationContext context, TimeSlice bucket, AggregateState state)
        {
            int numGood = 0;
            int numBad = 0;
            double minV = double.MaxValue;
            double maxV = double.MinValue;
            bool uncertainDataSubNormal = false;
            double range = double.NaN;

            foreach (DataValue dv in bucket.Values)
            {
                if (state.RawValueIsGood(dv))
                {
                    double v = Convert.ToDouble(dv.Value);
                    if (minV > v)
                    {
                        minV = v;
                    }
                    if (maxV < v)
                    {
                        maxV = v;
                    }
                    numGood++;
                }
                else
                {
                    uncertainDataSubNormal = true;
                    numBad++;
                }
            }
            if (minV != double.MaxValue && maxV != double.MinValue)
            {
                range = Math.Abs(maxV - minV);
            }

            StatusCode code = (uncertainDataSubNormal)
                ? StatusCodes.UncertainDataSubNormal
                : StatusCodes.Good;
            if (numGood + numBad == 0) code = StatusCodes.BadNoData;
            DataValue retval = new DataValue { SourceTimestamp = bucket.From };
            if (!double.IsNaN(range))
                retval.Value = range;
            code.AggregateBits = AggregateBits.Calculated;
            if (bucket.Incomplete) code.AggregateBits |= AggregateBits.Partial;
            retval.StatusCode = code;
            return retval;
        }
    }
}
