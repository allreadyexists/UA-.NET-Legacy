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
using System.Diagnostics;

namespace Opc.Ua.Server
{
    public class TimeAverageAggregate : TotalizeAverageAggregate
    {
        public override DataValue Compute(IAggregationContext context, TimeSlice bucket, AggregateState state)
        {
            DataValue initValue = bucket.EarlyBound.Value, finalValue = bucket.LateBound.Value;
            IEnumerator<DataValue> enumerator = bucket.Values.GetEnumerator();
            if (initValue == null && enumerator.MoveNext()) // first element
            {
                initValue = enumerator.Current;
                bucket.Incomplete = true;
            }
            if (finalValue == null)
            {
                while (enumerator.MoveNext())
                {
                    finalValue = enumerator.Current;
                }
                bucket.Incomplete = true;
            }
            DataValue retVal = base.Compute(context, bucket, state);
            if (retVal.StatusCode.CodeBits == StatusCodes.BadNoData)
                retVal.Value = null;
            else
                retVal.Value = Convert.ToDouble(retVal.Value) / 
                    Math.Abs((finalValue.SourceTimestamp - initValue.SourceTimestamp).TotalMilliseconds); // revisit
            return retVal;
        }
    }
}
