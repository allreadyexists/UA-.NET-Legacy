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

using System.Xml;
using System.Xml.Serialization;

namespace Opc.Ua.Schema.Binary
{
    /// <summary>
    /// A description of type,
    /// </summary>
    public partial class TypeDescription
    {
        /// <summary>
        /// The qualifed name for the type.
        /// </summary>
        [XmlIgnore()]
        public XmlQualifiedName QName
        {
            get { return m_qname;  }
            set { m_qname = value; }
        }

        private XmlQualifiedName m_qname;       
    }
}
