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
using Opc.Ua;

namespace Opc.Ua.Com.Client
{
    /// <summary>
    /// Stores the value of an HDA attribute.
    /// </summary>
    public class HdaAttributeValue
    {
        #region Public Members
        /// <summary>
        /// Initializes a new instance of the <see cref="HdaAttributeValue"/> class.
        /// </summary>
        public HdaAttributeValue()
        {
        }
        #endregion

        #region Public Members
        /// <summary>
        /// Gets or sets the attribute id.
        /// </summary>
        /// <value>The attribute id.</value>
        public uint AttributeId
        {
            get { return m_attributeId; }
            set { m_attributeId = value; }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public object Value
        {
            get { return m_value; }
            set { m_value = value; }
        }

        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        /// <value>The timestamp.</value>
        public DateTime Timestamp
        {
            get { return m_timestamp; }
            set { m_timestamp = value; }
        }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>The error.</value>
        public int Error
        {
            get { return m_error; }
            set { m_error = value; }
        }
        #endregion

        #region Private Fields
        private uint m_attributeId;
        private object m_value;
        private DateTime m_timestamp;
        private int m_error;
        #endregion
    }
}
