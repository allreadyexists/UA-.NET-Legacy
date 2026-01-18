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
using System.Globalization;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Xml;

namespace Opc.Ua
{
    /// <summary>
    /// Adds constructors, comparison functions and format capabilities to the NodeId class.
    /// </summary>
    public partial class NodeId : IComparable, IFormattable
    {
        #region Constructors
        /// <summary>
        /// Initializes a null node id.
        /// </summary>
        private NodeId()
        {
        }

        /// <summary>
        /// Initializes the node id with an identifier.
        /// </summary>
        public NodeId(string identifier)
        {
            Identifier = identifier;
        }

        /// <summary>
        /// Initializes the node id with an identifier.
        /// </summary>
        public NodeId(ExpandedNodeId nodeId)
        {
            if (!NodeId.IsNull(nodeId))
            {
                Identifier = nodeId.Identifier;
            }
        }

        /// <summary>
        /// Initializes the node id with an identifier.
        /// </summary>
        public NodeId(uint identifier)
        {
            Identifier = String.Format("i={0}", identifier);
        }

        /// <summary>
        /// Initializes the node id with an identifier.
        /// </summary>
        public NodeId(System.Guid identifier, ushort namespaceIndex)
        {
            Identifier = String.Format("ns={1};g={0}", identifier, namespaceIndex);
        }
        #endregion

        #region Static Members
        /// <summary>
        /// Checks if the node id is null.
        /// </summary>
        public static bool IsNull(NodeId nodeId)
        {
            if (nodeId == null || String.IsNullOrEmpty(nodeId.Identifier))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if the node id is null.
        /// </summary>
        public static bool IsNull(ExpandedNodeId nodeId)
        {
            if (nodeId == null || String.IsNullOrEmpty(nodeId.Identifier))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns an instance of a null NodeId.
        /// </summary>
        public static NodeId Null 
        {
            get { return m_null; }
        }

        private static readonly NodeId m_null = new NodeId();
        #endregion
        
        #region Public Methods
        /// <summary>
        /// Formats a node id as a string.
        /// </summary>
        public string Format()
        {
            StringBuilder buffer = new StringBuilder();
            Format(buffer);
            return buffer.ToString();
        }
        
        /// <summary>
        /// Formats the NodeId as a string and appends it to the buffer.
        /// </summary>
        public void Format(StringBuilder buffer)
        {
            if (Identifier != null)
            {
                buffer.Append(Identifier);
            }
        }

        /// <summary>
        /// Returns the string representation of a NodeId.
        /// </summary>
        public override string ToString()
        {
            return ToString(null, null);
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

            string id = obj as string;

            if (id == null)
            {
                NodeId nodeId = obj as NodeId;

                if (nodeId != null)
                {
                    id = nodeId.Identifier;
                }
                else
                {
                    ExpandedNodeId expandedNodeId = obj as ExpandedNodeId;

                    if (expandedNodeId != null)
                    {
                        id = expandedNodeId.Identifier;
                    }
                }
            }

            if (id == null)
            {
                return (Identifier != null)?-1:0;
            }

            if (Identifier != null)
            {
                return Identifier.CompareTo(id);
            }

            return +1;
        }

        /// <summary>
        /// Returns true if a is greater than b.
        /// </summary>
        public static bool operator >(NodeId a, NodeId b)
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
        public static bool operator <(NodeId a, NodeId b)
        {
            if (!Object.ReferenceEquals(a, null))
            {
                return a.CompareTo(b) < 0;
            }

            return true;
        }
        #endregion

        #region IFormattable Members
        /// <summary>
        /// Returns the string representation of a NodeId.
        /// </summary>
        public string ToString(string format, IFormatProvider provider)
        {
            if (format == null)
            {
                return String.Format(provider, "{0}", Format());
            }

            throw new FormatException(String.Format("Invalid format string: '{0}'.", format));
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
            if (Identifier == null)
            {
                return 0;
            }

            return Identifier.GetHashCode();
        }

        /// <summary>
        /// Returns true if the objects are equal.
        /// </summary>
        public static bool operator ==(NodeId a, object b)
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
        public static bool operator !=(NodeId a, object b)
        {
            if (Object.ReferenceEquals(a, null))
            {
                return !Object.ReferenceEquals(b, null);
            }

            return (a.CompareTo(b) != 0);
        }
		#endregion
    }
}
