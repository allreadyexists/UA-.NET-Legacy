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

namespace Opc.Ua.Com.Server
{
    /// <summary>
    /// Stores information an element in the DA server address space.
    /// </summary>
    public class DaProperty
    {
        #region Public Members
        /// <summary>
        /// Initializes a new instance of the <see cref="DaProperty"/> class.
        /// </summary>
        public DaProperty()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DaProperty"/> class.
        /// </summary>
        /// <param name="propertyId">The property id.</param>
        public DaProperty(int propertyId)
        {
            m_propertyId = propertyId;
            m_name = PropertyIds.GetDescription(m_propertyId);
            m_dataType = (short)PropertyIds.GetVarType(m_propertyId);
        }
        #endregion

        #region Public Members
        /// <summary>
        /// Gets or sets the property id.
        /// </summary>
        /// <value>The property id.</value>
        public int PropertyId
        {
            get { return m_propertyId; }
            set { m_propertyId = value; }
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
        /// Gets or sets the item id.
        /// </summary>
        /// <value>The item id.</value>
        public string ItemId
        {
            get { return m_itemId; }
            set { m_itemId = value; }
        }

        /// <summary>
        /// Gets or sets the COM data type.
        /// </summary>
        /// <value>The COM data type for the property.</value>
        public short DataType
        {
            get { return m_dataType; }
            set { m_dataType = value; }
        }
        #endregion

        #region Private Fields
        private int m_propertyId;
        private string m_name;
        private string m_itemId;
        private short m_dataType;
        #endregion
    }
}
