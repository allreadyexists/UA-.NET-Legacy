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

namespace FileSystem
{
    /// <summary>
    /// Wraps a CSV file which contains a set of named values.
    /// </summary>
    /// <remarks>
    /// One tag per line with three fields: name, data type and value.
    /// The data type is the name of a BuiltInType (see the BuiltInType enumeration).
    /// </remarks>
    public class CsvFile
    {
        #region Constructors
        /// <summary>
        /// Creates an empty file,
        /// </summary>
        public CsvFile()
        {
            m_entries = new List<Entry>();
        }
        #endregion
        
        #region Public Members
        /// <summary>
        /// The timestamp associated with the file.
        /// </summary>
        public DateTime Timestamp
        {
            get { return m_timestamp; }
        }

        /// <summary>
        /// Returns the value with the specified name.
        /// </summary>
        public object GetValue(string name)
        {
            for (int ii = 0; ii < m_entries.Count; ii++)
            {                    
                Entry entry = m_entries[ii];

                if (entry.Name == name)
                {
                    return entry.Value;
                }
            }

            return null;
        }

        /// <summary>
        /// Sets the value with the specified name.
        /// </summary>
        public void SetValue(string name, object value)
        {
            Entry entry;

            Opc.Ua.TypeInfo type = Opc.Ua.TypeInfo.Construct(value);

            for (int ii = 0; ii < m_entries.Count; ii++)
            {                    
                entry = m_entries[ii];

                if (entry.Name == name)
                {
                    entry.Value = value;
                    entry.DataType = type.BuiltInType;
                    m_entries[ii] = entry;
                    return;
                }
            }

            entry = new Entry();

            entry.Name = name;
            entry.DataType = type.BuiltInType;
            entry.Value = value;

            m_entries.Add(entry);
        }

        /// <summary>
        /// Loads the tags from the specified file.
        /// </summary>
        public void Load(FileInfo source)
        {
            m_entries.Clear();

            using (StreamReader reader = new StreamReader(source.OpenRead()))
            {
                for (string line = reader.ReadLine(); line != null; line = reader.ReadLine())
                {
                    string[] columns = line.Split(',');

                    if (columns == null || columns.Length < 3)
                    {
                        continue;
                    }

                    string name  = columns[0].Trim();
                    string type  = columns[1].Trim(); 
                    string value = columns[2].Trim(); 
                    
                    Entry entry = new Entry();

                    entry.Name = name;
                    entry.DataType = BuiltInType.String;
                    entry.Value = value;

                    try
                    {
                        BuiltInType datatype = (BuiltInType)Enum.Parse(typeof(BuiltInType), type);

                        switch (datatype)
                        {
                            case BuiltInType.Double:
                            {
                                entry.DataType = BuiltInType.Double;
                                entry.Value = Convert.ToDouble(value);
                                break;
                            }
                        }
                    }
                    catch
                    {
                        continue;
                    }

                    m_entries.Add(entry);
                }
            }

            m_timestamp = DateTime.UtcNow;
        }
        
        /// <summary>
        /// Saves the tags to the specified file.
        /// </summary>
        public void Save(FileInfo source)
        {
            using (StreamWriter writer = new StreamWriter(source.OpenWrite()))
            {
                for (int ii = 0; ii < m_entries.Count; ii++)
                {                    
                    Entry entry = m_entries[ii];

                    writer.Write(entry.Name);
                    writer.Write(", ");
                    writer.Write(entry.DataType);
                    writer.Write(", ");
                    writer.Write(entry.Value);
                    writer.Write("\r\n");
                }
            }

            m_timestamp = DateTime.UtcNow;
        }
        #endregion
        
        #region Entry Structure
        /// <summary>
        /// An entry in the CSV file.
        /// </summary>
        private struct Entry
        {
            public string Name;
            public BuiltInType DataType;
            public object Value;
        }
        #endregion
        
        #region Private Fields
        private DateTime m_timestamp;
        private List<Entry> m_entries;
        #endregion
    }
}
