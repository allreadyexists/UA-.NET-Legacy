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
using System.ServiceModel;
using System.Runtime.Serialization;

namespace Opc.Ua
{
    /// <summary>
    /// Adds constructors, comparison functions and format capabilities to the StatusCode class.
    /// </summary>
    public partial class StatusCode : IComparable, IFormattable
    {
        #region Constructors
        /// <summary>
        /// Initializes a null node id.
        /// </summary>
        internal StatusCode()
        {
        }

        /// <summary>
        /// Initializes the node id with an identifier.
        /// </summary>
        public StatusCode(uint identifier)
        {
            Code = identifier;
        }
        #endregion

        #region Static Members
        /// <summary>
        /// Converts a code to a numeric value.
        /// </summary>
        public static uint ToCode(StatusCode statusCode)
        {
            if (statusCode == null)
            {
                return StatusCodes.Good;
            }

            return statusCode.Code;
        }

        /// <summary>
        /// Checks if the status code is bad.
        /// </summary>
        public static bool IsBad(StatusCode statusCode)
        {
            if ((ToCode(statusCode) & 0x80000000) != 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns an instance of a Good StatusCode.
        /// </summary>
        public static StatusCode Good 
        {
            get { return m_good; }
        }

        private static readonly StatusCode m_good = new StatusCode(StatusCodes.Good);
        #endregion

        #region IFormattable Members
        /// <summary>
        /// Returns the string representation of the object.
        /// </summary>
        public string ToString(string format, IFormatProvider provider)
        {
            if (format == null)
            {
                return String.Format(provider, "{0}", StatusCodes.GetBrowseName((0xFFFF0000&ToCode(this))));
            }

            throw new FormatException(String.Format("Invalid format string: '{0}'.", format));
        }
        #endregion

        #region IComparable Members
        /// <summary>
        /// Compares the current instance to the object.
        /// </summary>
        public int CompareTo(object obj)
        {
            // check for null.
            if (Object.ReferenceEquals(obj, null))
            {
                return -1;
            }

            // check for reference comparisons.
            if (Object.ReferenceEquals(this, obj))
            {
                return 0;
            }

            uint id1 = ToCode(this);

            uint? id2 = obj as uint?;

            if (id2 == null)
            {
                StatusCode code = obj as StatusCode;

                if (code != null)
                {
                    id2 = ToCode(code);
                }
            }

            if (id2 == null)
            {
                return -1;
            }

            return id1.CompareTo(id2.Value);
        }

        /// <summary>
        /// Returns true if a is greater than b.
        /// </summary>
        public static bool operator >(StatusCode a, StatusCode b)
        {
            if (!Object.ReferenceEquals(a, null))
            {
                return a.CompareTo(b) > 0;
            }

            return false;
        }

        /// <summary>
        /// Returns true if a is less than b.
        /// </summary>
        public static bool operator <(StatusCode a, StatusCode b)
        {
            if (!Object.ReferenceEquals(a, null))
            {
                return a.CompareTo(b) < 0;
            }

            return true;
        }
        #endregion

        #region Comparison Functions
        /// <summary>
        /// Determines if the specified object is equal to the NodeId.
        /// </summary>
        public override bool Equals(object obj)
        {
            return (CompareTo(obj) == 0);
        }

        /// <summary>
        /// Returns a unique hashcode for the NodeId
        /// </summary>
        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }

        /// <summary>
        /// Returns true if the objects are equal.
        /// </summary>
        public static bool operator ==(StatusCode a, object b)
        {
            if (Object.ReferenceEquals(a, null))
            {
                return Object.ReferenceEquals(b, null);
            }

            return (a.CompareTo(b) == 0);
        }

        /// <summary>
        /// Returns true if the objects are not equal.
        /// </summary>
        public static bool operator !=(StatusCode a, object b)
        {
            if (Object.ReferenceEquals(a, null))
            {
                return !Object.ReferenceEquals(b, null);
            }

            return (a.CompareTo(b) != 0);
        }
        #endregion
    }

    #region Standard StatusCodes
    /// <summary>
    /// Defines standard status codes.
    /// </summary>
    /// <remarks>
    /// Defines standard status codes.
    /// </remarks>
    public static partial class StatusCodes
    {
        /// <summary>
        /// The operation completed successfully.
        /// </summary>
        public const uint Good = 0x00000000;

        /// <summary>
        /// The operation completed however its outputs may not be usable.
        /// </summary>
        public const uint Uncertain = 0x40000000;

        /// <summary>
        /// The operation failed.
        /// </summary>
        public const uint Bad = 0x80000000;
    }
    #endregion

    #region StatusCodeException Class
    /// <summary>
    /// An exception with an associated status code.
    /// </summary>
    public partial class StatusCodeException : Exception
    {
        /// <summary>
        /// Creates a new exception with a numeric code.
        /// </summary>
        public StatusCodeException(uint code, string format, params object[] args) : base(String.Format(format, args))
        {
            m_code = code;
        }

        /// <summary>
        /// Creates a new exception from a StatusCode object.
        /// </summary>
        public StatusCodeException(StatusCode code, string format, params object[] args) : base(String.Format(format, args))
        {
            m_code = StatusCodes.Bad;

            if (code != null)
            {
                m_code = (uint)code.Code;
            }
        }

        /// <summary>
        /// Returns the code associated with the exception.
        /// </summary>
        public uint Code
        {
            get { return m_code; }
        }
            
        private uint m_code;
    }
    #endregion
}
