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
    public class NumberOfTransitionsAggregate : SteppedInterpolatingCalculator
    {
        public override void UpdateBoundingValues(TimeSlice bucket, AggregateState state)
        {
            base.UpdateBoundingValues(bucket, state);
            UpdatePriorPoint(bucket.EarlyBound, state);
        }
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
            int nTransitions = 0;
            long stateCode = -1;
            IEnumerator<DataValue> enumerator = bucket.Values.GetEnumerator();
            bool bucketValueNotEmpty = enumerator.MoveNext();
            if (bucketValueNotEmpty && enumerator.Current != null)
            {
                if (bucket.EarlyBound != null)
                {
                    if (enumerator.Current.SourceTimestamp == bucket.EarlyBound.Timestamp && bucket.EarlyBound.PriorPoint != null)
                    {
                        stateCode = Convert.ToInt32(Convert.ToBoolean(bucket.EarlyBound.PriorPoint.Value));
                    }
                    else if (bucket.EarlyBound.Value != null)
                    {
                        stateCode = Convert.ToInt32(Convert.ToBoolean(bucket.EarlyBound.Value.Value));
                    }
                }
            }

            // viz. UA MultiStateNodeState & TwoStateNodeState, 
            // assume DataValue.Value is either an EnumValueType or a bool
            if (bucketValueNotEmpty)
            {
                do
                {
                    DataValue dv = enumerator.Current;
                    if (state.RawValueIsGood(dv))
                    {
                        EnumValueType ev = dv.Value as EnumValueType;
                        if (ev == null)
                        {
                            bool b;
                            if (bool.TryParse(dv.Value.ToString(), out b))
                            {
                                if (stateCode < 0)
                                    stateCode = b ? 1 : 0;
                                else if (b.CompareTo(Convert.ToBoolean(stateCode)) != 0)
                                {
                                    nTransitions++;
                                    stateCode = b ? 1 : 0;
                                }
                            }
                            else
                                continue;
                        }
                        else
                        {
                            long s = ev.Value;
                            if (stateCode < 0)
                                stateCode = s;
                            else if (!s.Equals(stateCode))
                            {
                                nTransitions++;
                                stateCode = s;
                            }
                        }
                        numGood++;
                    }
                    else
                    {
                        numBad++;
                    }
                } while (enumerator.MoveNext());
            }

            StatusCode code = ComputeStatus(context, numGood, numBad, bucket).Code;
            DataValue retval = new DataValue { SourceTimestamp = bucket.From, Value = nTransitions };
            code.AggregateBits = AggregateBits.Calculated;
            if (bucket.Incomplete) code.AggregateBits |= AggregateBits.Partial;
            retval.StatusCode = code;
            return retval;
        }
    }
}
