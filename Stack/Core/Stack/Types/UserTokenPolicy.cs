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
using System.Text;
using System.ServiceModel.Security;
using System.Security.Cryptography.X509Certificates;

namespace Opc.Ua
{
    /// <summary>
    /// Defines constants for key user token policies.
    /// </summary>
    public partial class UserTokenPolicy : IFormattable
    {
        #region Constructors
        /// <summary>
        /// Creates an empty token policy with the specified token type.
        /// </summary>
        public UserTokenPolicy(UserTokenType tokenType)
        {
            Initialize();
            m_tokenType = tokenType;
        }
        #endregion

        #region IFormattable Members
        /// <summary>
        /// Returns the object formatted as a string.
        /// </summary>
        public override string ToString()
        {
            return m_tokenType.ToString();
        }

        /// <summary>
        /// Returns the string representation of the object.
        /// </summary>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null)
            {
                return String.Format(formatProvider, "{0}", ToString());
            }

            throw new FormatException(Utils.Format("Invalid format string: '{0}'.", format));
        }
        #endregion
    }
}
