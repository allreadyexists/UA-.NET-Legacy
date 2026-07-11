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
    /// Defines the possible tag data types
    /// </summary>
    public enum UnderlyingSystemDataType
    {
        /// <summary>
        /// A 1-byte integer value.
        /// </summary>
        Integer1 = 0,

        /// <summary>
        /// A 2-byte integer value.
        /// </summary>
        Integer2 = 1,

        /// <summary>
        /// A 4-byte integer value.
        /// </summary>
        Integer4 = 2,

        /// <summary>
        /// A 4-byte floating point value.
        /// </summary>
        Real4 = 3,

        /// <summary>
        /// A string value.
        /// </summary>
        String = 4
    }
}
