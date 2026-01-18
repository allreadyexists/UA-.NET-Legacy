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
    /// Stores information about an HDA attribute.
    /// </summary>
    public class HdaAttribute
    {
        #region Public Members
        /// <summary>
        /// Initializes a new instance of the <see cref="HdaAttribute"/> class.
        /// </summary>
        public HdaAttribute()
        {
        }
        #endregion

        #region Public Members
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public uint Id
        {
            get { return m_id; }
            set { m_id = value; }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description
        {
            get { return m_description; }
            set { m_description = value; }
        }

        /// <summary>
        /// Gets or sets the type of the data.
        /// </summary>
        /// <value>The type of the data.</value>
        public short DataType
        {
            get { return m_dataType; }
            set { m_dataType = value; }
        }
        #endregion

        #region Private Fields
        private uint m_id;
        private string m_name;
        private string m_description;
        private short m_dataType;
        #endregion
    }
}
