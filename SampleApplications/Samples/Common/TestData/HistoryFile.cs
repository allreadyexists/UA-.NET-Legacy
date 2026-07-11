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
using System.Xml;
using System.IO;
using System.Text;
using System.Reflection;
using System.Threading;
using System.Globalization;
using Opc.Ua;
using Opc.Ua.Server;

namespace TestData
{
    /// <summary>
    /// Wraps a file which contains a list of historical values.
    /// </summary>
    internal class HistoryFile : IHistoryDataSource
    {
        #region Constructors
        /// <summary>
        /// Creates a new file.
        /// </summary>
        internal HistoryFile(object dataLock, List<HistoryEntry> entries)
        {
            m_lock = dataLock;
            m_entries = entries;
        }
        #endregion
        
        #region IHistoryReader Members
        /// <summary>
        /// Returns the next value in the archive.
        /// </summary>
        /// <param name="startTime">The starting time for the search.</param>
        /// <param name="isForward">Whether to search forward in time.</param>
        /// <param name="isReadModified">Whether to return modified data.</param>
        /// <param name="position">A index that must be passed to the NextRaw call. </param>
        /// <returns>The DataValue.</returns>
        public DataValue FirstRaw(DateTime startTime, bool isForward, bool isReadModified, out int position)
        {
            position = -1;
            
            lock (m_lock)
            {
                if (isForward)
                {
                    for (int ii = 0; ii < m_entries.Count; ii++)
                    {
                        if (m_entries[ii].Value.ServerTimestamp >= startTime)
                        {
                            position = ii;
                            break;
                        }
                    }
                }
                else
                {
                    for (int ii = m_entries.Count-1; ii >= 0; ii--)
                    {
                        if (m_entries[ii].Value.ServerTimestamp <= startTime)
                        {
                            position = ii;
                            break;
                        }
                    }
                }

                if (position < 0 || position >= m_entries.Count)
                {
                    return null;
                }

                HistoryEntry entry = m_entries[position];

                DataValue value = new DataValue();

                value.Value = entry.Value.Value;
                value.ServerTimestamp = entry.Value.ServerTimestamp;
                value.SourceTimestamp = entry.Value.SourceTimestamp;
                value.StatusCode = entry.Value.StatusCode;

                return value;
            }
        }
        
        /// <summary>
        /// Returns the next value in the archive.
        /// </summary>
        /// <param name="lastTime">The timestamp of the last value returned.</param>
        /// <param name="isForward">Whether to search forward in time.</param>
        /// <param name="isReadModified">Whether to return modified data.</param>
        /// <param name="position">A index previously returned by the reader.</param>
        /// <returns>The DataValue.</returns>
        public DataValue NextRaw(DateTime lastTime, bool isForward, bool isReadModified, ref int position)
        {
            position++;

            lock (m_lock)
            {
                if (position < 0 || position >= m_entries.Count)
                {
                    return null;
                }

                HistoryEntry entry = m_entries[position];

                DataValue value = new DataValue();

                value.Value = entry.Value.Value;
                value.ServerTimestamp = entry.Value.ServerTimestamp;
                value.SourceTimestamp = entry.Value.SourceTimestamp;
                value.StatusCode = entry.Value.StatusCode;

                return value;
            }
        }
        #endregion
        
        #region Private Fields
        private object m_lock = new object();
        private List<HistoryEntry> m_entries;
        #endregion
    }
}
