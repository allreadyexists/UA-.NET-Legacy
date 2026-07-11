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
    public class AverageAggregate : TotalAggregate
    {
        protected override StatusCode ComputeStatus(IAggregationContext context, int numGood, int numBad, TimeSlice bucket)
        {
            return numBad + numGood == 0 ? StatusCodes.BadNoData : base.ComputeStatus(context, numGood, numBad, bucket);
        }

        public override DataValue Compute(IAggregationContext context, TimeSlice bucket, AggregateState state)
        {
            DataValue initValue = bucket.EarlyBound.Value;
            IEnumerator<DataValue> enumerator = bucket.Values.GetEnumerator();
            if (enumerator.MoveNext()) // first element
            {
                if(initValue == null && bucket.From != enumerator.Current.SourceTimestamp)
                    bucket.Incomplete = true;
            }
            DataValue retVal = base.Compute(context, bucket, state);
            if (GoodDataCount > 0)
                retVal.Value = Convert.ToDouble(retVal.Value) / GoodDataCount;
            else
                retVal.Value = null;
            return retVal;
        }
    }
}
