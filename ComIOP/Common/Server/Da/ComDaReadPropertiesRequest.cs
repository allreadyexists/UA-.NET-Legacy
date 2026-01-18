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
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using Opc.Ua.Client;

namespace Opc.Ua.Com.Server
{
    /// <summary>
    /// Specifies the parameters used to create a DA group item.
    /// </summary>
    public class ComDaReadPropertiesRequest
    {
        #region Public Properties
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
        /// Gets or sets the values.
        /// </summary>
        /// <value>The values.</value>
        public DaValue[] Values
        {
            get { return m_values; }
            set { m_values = value; }
        }

        /// <summary>
        /// Gets or sets the indexes used to look up the results.
        /// </summary>
        /// <value>The indexes.</value>
        public int[] Indexes
        {
            get { return m_indexes; }
            set { m_indexes = value; }
        }

        /// <summary>
        /// Gets or sets the error associated with the operation.
        /// </summary>
        /// <value>The error.</value>
        public int Error
        {
            get { return m_error; }
            set { m_error = value; }
        }
        #endregion

        #region Private Fields
        private string m_itemId;
        private DaValue[] m_values;
        private int[] m_indexes;
        private int m_error;
        #endregion
    }
}
