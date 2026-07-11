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
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;

namespace Opc.Ua
{
    /// <summary>
    /// Stores a range.
    /// </summary>
    public partial class Range
    {
        /// <summary>
        /// Initializes the object with the high and low limits.
        /// </summary>
        public Range(double high, double low)
        {
            m_low  = low;
            m_high = high;

            // swap values if high is not actually higher.
            if (low > high)
            {
                m_high = low;
                m_low  = high;
            }
        }

        /// <summary>
        /// Returns the difference between high and low.
        /// </summary>
        public double Magnitude
        {
            get { return Math.Abs(m_high - m_low); }
        }
    }
}
