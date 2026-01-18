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
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Runtime.Serialization;
using System.Reflection;
using System.Threading;
using Opc.Ua;
using Opc.Ua.Server;

namespace Boiler
{       
    /// <summary>
    /// A object representing a generic controller.
    /// </summary>
    public partial class GenericControllerState
    {
        #region Public Interface
        /// <summary>
        /// Updates the measurement and calculates the new control output.
        /// </summary>
        public double UpdateMeasurement(AnalogItemState<double> source)
        {
            Range range = source.EURange.Value;
            m_measurement.Value = source.Value;

            // clamp the setpoint.
            if (range != null)
            {
                if (m_setPoint.Value > range.High)
                {
                    m_setPoint.Value = range.High;
                }
                
                if (m_setPoint.Value < range.Low)
                {
                    m_setPoint.Value = range.Low;
                }                
            }

            // calculate error.
            m_controlOut.Value = m_setPoint.Value - m_measurement.Value;

            if (range != null)
            {
                m_controlOut.Value /= range.Magnitude;

                if (Math.Abs(m_controlOut.Value) > 1.0)
                {
                    m_controlOut.Value = (m_controlOut.Value < 0)?-1.0:+1.0;
                }
            }

            // return the new output.
            return m_controlOut.Value;            
        }
        #endregion
    }
}
