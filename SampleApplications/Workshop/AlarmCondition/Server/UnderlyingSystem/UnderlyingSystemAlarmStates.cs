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

namespace Quickstarts.AlarmConditionServer
{
    /// <summary>
    /// Defines the possible states for the condition.
    /// </summary>
    [Flags]
    public enum UnderlyingSystemAlarmStates
    {
        /// <summary>
        /// The condition state is unknown.
        /// </summary>
        Undefined = 0x0,

        /// <summary>
        /// The condition is enabled and will produce events.
        /// </summary>
        Enabled = 0x1,

        /// <summary>
        /// The condition requires acknowledgement by the user. 
        /// </summary>
        Acknowledged = 0x2,

        /// <summary>
        /// The condition requires that the used confirm that action was taken. 
        /// </summary>
        Confirmed = 0x4,

        /// <summary>
        /// The condition is active. 
        /// </summary>
        Active = 0x8,

        /// <summary>
        /// The condition has been suppressed by the system.
        /// </summary>
        Suppressed = 0x10,

        /// <summary>
        /// The condition has been shelved by the user.
        /// </summary>
        Shelved = 0x20,

        /// <summary>
        /// The condition has exceeed the high-high limit.
        /// </summary>
        HighHigh = 0x40,

        /// <summary>
        /// The condition has exceeed the high limit.
        /// </summary>
        High = 0x80,

        /// <summary>
        /// The condition has exceeed the low limit.
        /// </summary>
        Low = 0x100,

        /// <summary>
        /// The condition has exceeed the low-low limit.
        /// </summary>
        LowLow = 0x200,

        /// <summary>
        /// A mask used to clear all limit bits.
        /// </summary>
        Limits = 0x3C0,

        /// <summary>
        /// The condition has deleted.
        /// </summary>
        Deleted = 0x400
    }
}
