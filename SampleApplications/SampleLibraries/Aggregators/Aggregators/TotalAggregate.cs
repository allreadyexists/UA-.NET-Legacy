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
    public class TotalAggregate : FloatInterpolatingCalculator
    {
        protected int GoodDataCount { get; set; }

        protected override StatusCode ComputeStatus(IAggregationContext context, int numGood, int numBad, TimeSlice bucket)
        {
            if (numGood + numBad == 0)
                return StatusCodes.GoodNoData;
            if (numGood == 0 && numBad > 0)
                return StatusCodes.Bad;
            return base.ComputeStatus(context, numGood, numBad, bucket);
        }

        public override DataValue Compute(IAggregationContext context, TimeSlice bucket, AggregateState state)
        {
            int numGood = 0;
            int numBad = 0;
            double total = 0.0;
            foreach (DataValue v in bucket.Values)
            {
                if (state.RawValueIsGood(v))
                {
                    numGood += 1;
                    total += Convert.ToDouble(v.Value, CultureInfo.InvariantCulture);
                }
                else
                {
                    numBad += 1;
                }
            }
            DataValue retval = new DataValue { SourceTimestamp = bucket.From };
            StatusCode code = ComputeStatus(context, numGood, numBad, bucket).Code;
            code.AggregateBits = AggregateBits.Calculated;
            if (bucket.Incomplete) code.AggregateBits |= AggregateBits.Partial;
            if (StatusCode.IsNotBad(code))
                retval.Value = total;
            retval.StatusCode = code;
            GoodDataCount = numGood;
            return retval;
        }
    }
}
