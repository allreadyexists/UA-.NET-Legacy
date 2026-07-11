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
using System.Text;

namespace Opc.Ua
{
    /// <summary>
    /// Adds constructors, comparison functions and format capabilities to the ExtensionObject class.
	/// </summary>
    public partial class ExtensionObject : IFormattable
    {
        #region Public Methods
        /// <summary>
        /// Creates an empty extension object.
        /// </summary>
        public ExtensionObject()
        {
        }

        /// <summary>
        /// Serializes the value using XML and stores it in the extension object.
        /// </summary>
        public ExtensionObject(ExpandedNodeId typeId, object value)
        {
            TypeId = typeId;

            StringBuilder buffer = new StringBuilder();

            using (XmlWriter writer = XmlWriter.Create(buffer))
            {
                DataContractSerializer serializer = new DataContractSerializer(value.GetType());
                serializer.WriteObject(writer, value);
            }

            XmlDocument document = new XmlDocument();
            document.InnerXml = buffer.ToString();

            ExtensionObjectBody body = new ExtensionObjectBody();
            body.Nodes = new XmlNode[] { document.DocumentElement };
            Body = body;
        }

        /// <summary>
        /// Parses the body and returns an object.
        /// </summary>
        public object ParseBody(Type type)
        {
            if (this.Body != null && this.Body.Nodes != null && this.Body.Nodes.Length > 0)
            {
                using (XmlNodeReader reader = new XmlNodeReader(this.Body.Nodes[0]))
                {
                    DataContractSerializer serializer = new DataContractSerializer(type);
                    return serializer.ReadObject(reader);
                }
            }

            return null;
        }
        #endregion

        #region IFormattable Members
        /// <summary>
        /// Returns the string representation of the object.
        /// </summary>
        /// <remarks>
        /// Returns the string representation of the object.
        /// </remarks>
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
                return String.Format(provider, "{0}", TypeId);
            }

            throw new FormatException(String.Format("Invalid format string: '{0}'.", format));
        }
        #endregion
    }
}
