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

namespace Opc.Ua
{
    /// <summary>
    /// The possible states for a limit alarm.
    /// </summary>
    [Flags]
    public enum LimitAlarmStates
    {
        /// <summary>
        /// The alarm ia inactive.
        /// </summary>
        Inactive = 0x0,

        /// <summary>
        /// The alarm is in the HighHigh state.
        /// </summary>
        HighHigh = 0x1,

        /// <summary>
        /// The alarm is in the High state.
        /// </summary>
        High = 0x2,

        /// <summary>
        /// The alarm is in the Low state.
        /// </summary>
        Low = 0x4,

        /// <summary>
        /// The alarm is in the LowLow state.
        /// </summary>
        LowLow  =0x8
    }
}
