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
    /// Stores the history of an HDA item.
    /// </summary>
    public class HdaItemHistoryData
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="HdaItemHistoryData"/> class.
        /// </summary>
        public HdaItemHistoryData()
        {
        }
        #endregion

        #region Public Members
        /// <summary>
        /// Gets or sets the server handle.
        /// </summary>
        /// <value>The server handle.</value>
        public int ServerHandle
        {
            get { return m_serverHandle; }
            set { m_serverHandle = value; }
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

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public object[] Values
        {
            get { return m_values; }
            set { m_values = value; }
        }

        /// <summary>
        /// Gets or sets the qualities.
        /// </summary>
        /// <value>The qualities.</value>
        public int[] Qualities
        {
            get { return m_qualities; }
            set { m_qualities = value; }
        }

        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        /// <value>The timestamp.</value>
        public DateTime[] Timestamps
        {
            get { return m_timestamps; }
            set { m_timestamps = value; }
        }

        /// <summary>
        /// Gets or sets the modifications.
        /// </summary>
        /// <value>The modifications.</value>
        public ModificationInfo[] Modifications
        {
            get { return m_modifications; }
            set { m_modifications = value; }
        }
        #endregion

        #region Private Fields
        private int m_serverHandle;
        private object[] m_values;
        private int[] m_qualities;
        private DateTime[] m_timestamps;
        private ModificationInfo[] m_modifications;
        private int m_error;
        #endregion
    }
}
