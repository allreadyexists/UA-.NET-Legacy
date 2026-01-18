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
using System.Xml;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace Opc.Ua
{
    /// <summary>
    /// A thread safe table of string constants.
    /// </summary>
    public class StringTable
    {
        #region Constructors
        /// <summary>
        /// Creates an empty collection.
        /// </summary>
        public StringTable()
        {
            m_strings = new List<string>();
        }
        
        /// <summary>
        /// Copies a list of strings.
        /// </summary>
        public StringTable(IEnumerable<string> strings)
        {
            Update(strings);
        }
        #endregion
        
        #region Public Members
        /// <summary>
        /// The synchronization object.
        /// </summary>
        public object SyncRoot
        {
            get { return m_lock; }
        }

        /// <summary>
        /// Updates the table of namespace uris.
        /// </summary>
        public virtual void Update(IEnumerable<string> strings)
        {
            if (strings == null) throw new ArgumentNullException("strings");

            lock (m_lock)
            {
                m_strings = new List<string>(strings);
            }       
        }

        /// <summary>
        /// Adds a string to the end of the table.
        /// </summary>
        public virtual int Append(string value)
        {
            lock (m_lock)
            {
                m_strings.Add(value);
                return m_strings.Count-1;
            }
        }

        /// <summary>
        /// Returns the namespace uri at the specified index.
        /// </summary>
        public string GetString(uint index)
        {
            lock (m_lock)
            {
                if (index >= 0 && index < m_strings.Count)
                {
                    return m_strings[(int)index];
                }

                return null;
            }
        }

        /// <summary>
        /// Returns the index of the specified namespace uri.
        /// </summary>
        public int GetIndex(string value)
        {
            lock (m_lock)
            {
                if (String.IsNullOrEmpty(value))
                {
                    return -1;
                }

                return m_strings.IndexOf(value);                
            }
        }   

        /// <summary>
        /// Returns the index of the specified namespace uri, adds it if it does not exist.
        /// </summary>
        public ushort GetIndexOrAppend(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("value");
            }

            lock (m_lock)
            {
                int index = m_strings.IndexOf(value);    
            
                if (index == -1)
                {                
                    m_strings.Add(value);
                    return (ushort)(m_strings.Count-1);
                }

                return (ushort)index;
            }
        }   

        /// <summary>
        /// Returns the contexts of the table.
        /// </summary>
        public string[] ToArray()
        {
            lock (m_lock)
            {
                return m_strings.ToArray();
            }
        }

        /// <summary>
        /// Returns the number of entries in the table.
        /// </summary>
        public int Count
        {
            get
            {
                lock (m_lock)
                {
                    return m_strings.Count;
                }
            }
        }
        #endregion
        
        #region Private Fields
        private object m_lock = new object();
        private List<string> m_strings;
        #endregion
    }

    /// <summary>
    /// The table of namespace uris for a server.
    /// </summary>
    public class NamespaceTable : StringTable
    {
        #region Constructors
        /// <summary>
        /// Creates an empty collection.
        /// </summary>
        public NamespaceTable()
        {
            Append(Namespaces.OpcUa);
        }
        
        /// <summary>
        /// Copies a list of strings.
        /// </summary>
        public NamespaceTable(IEnumerable<string> namespaceUris)
        {
            Update(namespaceUris);
        }
        #endregion
        
        #region Public Members
        /// <summary>
        /// Updates the table of namespace uris.
        /// </summary>
        public override void Update(IEnumerable<string> namespaceUris)
        {
            if (namespaceUris == null) throw new ArgumentNullException("namespaceUris");

            // check that first entry is the UA namespace.
            int ii = 0;

            foreach (string namespaceUri in namespaceUris)
            {
                if (ii == 0 && namespaceUri != Namespaces.OpcUa)
                {
                    throw new ArgumentException("The first namespace in the table must be the OPC-UA namespace.");
                }
                
                ii++;

                if (ii == 2)
                {
                    break;
                }
            }

            base.Update(namespaceUris);
        }
        #endregion
    }
}
