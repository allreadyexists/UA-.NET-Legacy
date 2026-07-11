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

namespace Quickstarts.DataAccessServer
{
    /// <summary>
    /// Defines the possible tag types
    /// </summary>
    public enum UnderlyingSystemTagType
    {
        /// <summary>
        /// The tag has no special characteristics.
        /// </summary>
        Normal = 0,

        /// <summary>
        /// The tag is an analog value with a high and low range.
        /// </summary>
        Analog = 1,

        /// <summary>
        /// The tag is a digital value with a true and false state.
        /// </summary>
        Digital = 2,

        /// <summary>
        /// The tag is a enumerated value with set of names states.
        /// </summary>
        Enumerated = 3
    }
}
