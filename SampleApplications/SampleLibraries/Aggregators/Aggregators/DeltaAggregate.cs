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
    public class DeltaAggregate : NonInterpolatingCalculator
    {
        public override DataValue Compute(IAggregationContext context, TimeSlice bucket, AggregateState state)
        {
            int numGood = 0;
            int numBad = 0;
            DataValue firstGoodDv = null;
            DataValue lastGoodDv = null;
            DataValue lastDv = null;
            bool uncertainDataSubNormal = false;
            double delta = double.NaN;
            
            foreach (DataValue dv in bucket.Values)
            {
                if (state.RawValueIsGood(dv))
                {
                    if (firstGoodDv == null)
                    {
                        firstGoodDv = dv;
                    }
                    lastGoodDv = dv;                   
                    numGood++;
                }
                else
                {
                    // check for non-good value occuring before first good value
                    if (firstGoodDv == null)
                        uncertainDataSubNormal = true;
                    numBad++;
                }
                lastDv = dv;
            }
            if (firstGoodDv != null)
            {
                double fv = Convert.ToDouble(firstGoodDv.Value);
                double lv = Convert.ToDouble(lastGoodDv.Value);
                delta = lv - fv;
            }
            
            // check for non-good value occuring after latest good value
            if (!uncertainDataSubNormal && lastGoodDv != null && lastGoodDv.SourceTimestamp < lastDv.SourceTimestamp)
                uncertainDataSubNormal = true;

            StatusCode code = (uncertainDataSubNormal)
                ? StatusCodes.UncertainDataSubNormal
                : (numGood > 0) ? StatusCodes.Good : StatusCodes.BadNoData;
            DataValue retval = new DataValue { SourceTimestamp = bucket.From };
            if (!double.IsNaN(delta))
                retval.Value = delta;
            code.AggregateBits = AggregateBits.Calculated;
            if (bucket.Incomplete) code.AggregateBits |= AggregateBits.Partial;
            retval.StatusCode = code;
            return retval;
        }
    }
}
