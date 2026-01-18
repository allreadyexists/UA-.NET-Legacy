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
    public abstract class StartEndAggregate : NonInterpolatingCalculator
    {
        protected abstract DataValue GetDataValue(List<DataValue> dvList);
        public override DataValue Compute(IAggregationContext context, TimeSlice bucket, AggregateState state)
        {
            List<DataValue> l = new List<DataValue>(bucket.Values);
            DataValue dv = l.Count > 0 ? GetDataValue(l) : null;
            if (SteppedVariable && dv == null)
                dv = bucket.LateBound.Value;

            DataValue retval = new DataValue();
            StatusCode code = StatusCodes.BadNoData;
            if (dv != null)
            {
                code = StatusCode.IsNotGood(dv.StatusCode)
                    ? StatusCodes.UncertainDataSubNormal
                    : StatusCodes.Good;
                retval.SourceTimestamp = dv.SourceTimestamp;
                retval.Value = dv.Value;
                code.AggregateBits = AggregateBits.Raw;
                if (bucket.Incomplete) code.AggregateBits |= AggregateBits.Partial;
            }
            else
            {
                retval.SourceTimestamp = bucket.From;
            }
            retval.StatusCode = code;
            return retval;
        }
    }

    public class EndAggregate : StartEndAggregate
    {
        protected override DataValue GetDataValue(List<DataValue> dvList)
        {
            return dvList[dvList.Count - 1];
        }
    }
}
