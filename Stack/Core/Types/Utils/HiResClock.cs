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
using System.Runtime.InteropServices;

namespace Opc.Ua
{
    /// <summary>
    /// Produces high resolution timestamps.
    /// </summary>
    public class HiResClock
    {
        /// <summary>
        /// Returns the current UTC time (bugs in HALs on some computers can result in time jumping backwards).
        /// </summary>
        public static DateTime UtcNow
        {
            get
            {
                #if !SILVERLIGHT
                if (s_Default.m_disabled)
                {
                    return DateTime.UtcNow;
                }

                long counter = 0;

                if (NativeMethods.QueryPerformanceCounter(out counter) == 0)
                {
                    return DateTime.UtcNow;
                }

                decimal ticks = (counter - s_Default.m_baseline)*s_Default.m_ratio;

                return new DateTime((long)ticks + s_Default.m_offset);
                #else
                return DateTime.UtcNow;                
                #endif
            }
        }

        /// <summary>
        /// Disables the hi-res clock (may be necessary on some machines with bugs in the HAL).
        /// </summary>
        public static bool Disabled
        {
            get
            {
                return s_Default.m_disabled;
            }

            set
            {
                s_Default.m_disabled = value;
            }
        }

        /// <summary>
        /// Constructs a class.
        /// </summary>
        private HiResClock()
        {
            #if !SILVERLIGHT
            if (NativeMethods.QueryPerformanceFrequency(out m_frequency) == 0)
            {
                m_frequency = TimeSpan.TicksPerSecond;
            }

            m_offset = DateTime.UtcNow.Ticks;

            if (NativeMethods.QueryPerformanceCounter(out m_baseline) == 0)
            {
                m_baseline = m_offset;
            }

            m_ratio = ((decimal)TimeSpan.TicksPerSecond)/m_frequency;
            #endif
        }

        /// <summary>
        /// Defines a global instance.
        /// </summary>
        private static readonly HiResClock s_Default = new HiResClock();

        /// <summary>
        /// Defines the native methods used by the class.
        /// </summary>
        private static class NativeMethods
        {
            [DllImport("Kernel32.dll")]
            public static extern int QueryPerformanceFrequency(out long lpFrequency);

            [DllImport("Kernel32.dll")]
            public static extern int QueryPerformanceCounter(out long lpFrequency);
        }
        
        #if !SILVERLIGHT
        private long m_frequency;
        private long m_baseline;
        private long m_offset;
        private decimal m_ratio;
        #endif

        private bool m_disabled;
    }

}
