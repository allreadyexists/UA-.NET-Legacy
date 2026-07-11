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
    public class WorstQualityAggregate: NonInterpolatingCalculator
    {
        public override DataValue Compute(IAggregationContext context, TimeSlice bucket, AggregateState state)
        {
            StatusCode returnCode = StatusCodes.BadNoData;
            foreach(DataValue dv in bucket.Values)
            {
                if (returnCode == StatusCodes.BadNoData)
                {
                    returnCode = dv.StatusCode;
                }
                else
                {
                    // StatusCodes.Bad = 0x80000000
                    // StatusCodes.Uncertain = 0x40000000
                    // StatusCodes.Good = 0x00000000
                    uint code = dv.StatusCode.Code >> 28;   // 7 Hexadecimal digits = 28 binary digits.
                    switch (code)
                    {
                        case 8: // 8 is maximum
                            returnCode = StatusCodes.Bad;
                            break;
                        case 4:
                            if(StatusCode.IsNotBad(returnCode))
                                returnCode = StatusCodes.Uncertain;
                            break;
                        case 0: // 0 is minimum 
                            break;
                        default:
                            Debug.Assert(true, "should not touch this line");
                            throw new Exception(String.Format("Unknown error in WorstQuality aggregate calculation, code = {0}", dv.StatusCode));
                    }
                }
            }
            DataValue retVal = new DataValue() { SourceTimestamp = bucket.From };
            if (returnCode != StatusCodes.BadNoData)
            {
                retVal.Value = returnCode;
                StatusCode status = StatusCodes.Good;
                status.AggregateBits |= AggregateBits.Calculated;
                if (bucket.Incomplete) status.AggregateBits |= AggregateBits.Partial;
                retVal.StatusCode = status;
            }
            else
            {
                retVal.StatusCode = returnCode;
            }
            return retVal;
        }
    }
}
