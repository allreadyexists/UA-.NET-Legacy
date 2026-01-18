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
    public class DaValue
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DaValue"/> class.
        /// </summary>
        public DaValue()
        {
        }
        #endregion

        #region Public Members
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
        /// Gets or sets the quality.
        /// </summary>
        /// <value>The quality.</value>
        public short Quality
        {
            get { return (short)(m_quality & 0xFFFF); }
            set { m_quality = Utils.ToUInt32((int)value); }
        }

        /// <summary>
        /// Gets or sets the quality.
        /// </summary>
        /// <value>The quality.</value>
        public uint HdaQuality
        {
            get { return m_quality; }
            set { m_quality = value; }
        }

        /// <summary>
        /// Gets or sets the COM error.
        /// </summary>
        /// <value>The COM error.</value>
        public int Error
        {
            get { return m_error; }
            set { m_error = value; }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <typeparam name="T">The type of value to return.</typeparam>
        /// <returns>The value if no error and a valid value exists. The default value for the type otherwise.</returns>
        public T GetValue<T>()
        {
            if (m_error < 0)
            {
                return default(T);
            }

            if (typeof(T).IsInstanceOfType(m_value))
            {
                return (T)m_value;
            }

            return default(T);
        }
        #endregion

        #region Private Fields
        private object m_value;
        private uint m_quality;
        private DateTime m_timestamp;
        private int m_error;
        #endregion
    }

    /// <summary>
    /// Stores information an element in the DA server address space.
    /// </summary>
    public class DaCacheValue : DaValue
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DaValue"/> class.
        /// </summary>
        public DaCacheValue()
        {
        }
        #endregion

        #region Public Members
        /// <summary>
        /// Gets or sets the cache timestamp.
        /// </summary>
        /// <value>The cache timestamp.</value>
        public DateTime CacheTimestamp
        {
            get { return m_cacheTimestamp; }
            set { m_cacheTimestamp = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the value has changed since the last update.
        /// </summary>
        /// <value><c>true</c> if the value has changed; otherwise, <c>false</c>.</value>
        public bool Changed
        {
            get { return m_changed; }
            set { m_changed = value; }
        }

        /// <summary>
        /// Gets or sets the next entry in the cache.
        /// </summary>
        /// <value>The next entry.</value>
        public DaCacheValue NextEntry
        {
            get { return m_nextEntry; }
            set { m_nextEntry = value; }
        }

        /// <summary>
        /// Gets the latest value in the cache.
        /// </summary>
        /// <param name="value">The value.</param>
        public void GetLatest(DaValue value)
        {
            value.Value = this.Value;
            value.Quality = this.Quality;
            value.Timestamp = this.Timestamp;
            value.Error = this.Error;
        }
        #endregion

        #region Private Fields
        private DateTime m_cacheTimestamp;
        private DaCacheValue m_nextEntry;
        private bool m_changed;
        #endregion
    }
}
