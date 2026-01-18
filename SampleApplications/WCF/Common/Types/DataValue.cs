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
using System.Xml;

namespace Opc.Ua
{
    /// <summary>
    /// Adds constructors and format capabilities to the DataValue class.
	/// </summary>
    public partial class DataValue : IFormattable
    {
        #region IFormattable Members
        /// <summary>
        /// Returns the string representation of the object.
        /// </summary>
        public override string ToString()
        {
            return ToString(null, null);
        }

        /// <summary>
        /// Returns the string representation of the object.
        /// </summary>
        public string ToString(string format, IFormatProvider provider)
        {
            if (format == null)
            {
                if (StatusCode.IsBad(StatusCode))
                {
                    return String.Format(provider, "{0}", StatusCode);
                }
                
                return String.Format(provider, "{0}", Value);
            }

            throw new FormatException(String.Format("Invalid format string: '{0}'.", format));
        }
        #endregion
    }
}
