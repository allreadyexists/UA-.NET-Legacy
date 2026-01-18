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
    public class CountAggregate : NonInterpolatingCalculator
    {
        protected override StatusCode ComputeStatus(IAggregationContext context, int numGood, int numBad, TimeSlice bucket)
        {
            StatusCode code = base.ComputeStatus(context, numGood, numBad, bucket);
            if (code.CodeBits == StatusCodes.GoodNoData)    // can be removed if GoodNoData is used.
                code = StatusCodes.Good;
            return code;
        }
        public override DataValue Compute(IAggregationContext context, TimeSlice bucket, AggregateState state)
        {
            int numGood = 0;
            int numBad = 0;
            foreach (DataValue v in bucket.Values)
            {
                if (state.RawValueIsGood(v))
                {
                    numGood += 1;
                }
                else
                {
                    numBad += 1;
                }
            }
            StatusCode code = StatusCodes.Good;
            DataValue retval = new DataValue { SourceTimestamp = bucket.From };
            retval.Value = numGood;
            code = ComputeStatus(context, numGood, numBad, bucket).Code;
            code.AggregateBits = AggregateBits.Calculated;
            if (bucket.Incomplete) code.AggregateBits |= AggregateBits.Partial;
            retval.StatusCode = code;
            return retval;
        }
    }
}
