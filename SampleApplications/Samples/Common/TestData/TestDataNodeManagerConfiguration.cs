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
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Collections.Generic;

using Opc.Ua.Server;

namespace TestData
{
    /// <summary>
    /// Stores the configuration the test node manager
    /// </summary>
    [DataContract(Namespace = Namespaces.TestData)]
    public class TestDataNodeManagerConfiguration
    {
        #region Constructors
        /// <summary>
        /// The default constructor.
        /// </summary>
        public TestDataNodeManagerConfiguration()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes the object during deserialization.
        /// </summary>
        [OnDeserializing()]
        private void Initialize(StreamingContext context)
        {
            Initialize();
        }

        /// <summary>
        /// Sets private members to default values.
        /// </summary>
        private void Initialize()
        {
            m_saveFilePath = null;
            m_maxQueueSize = 100;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// The path to the file that stores state of the node manager.
        /// </summary>
        [DataMember(Order = 1)]
        public string SaveFilePath
        {
            get { return m_saveFilePath; }
            set { m_saveFilePath = value; }
        }

        /// <summary>
        /// The maximum length for a monitored item sampling queue.
        /// </summary>
        [DataMember(Order = 2)]
        public uint MaxQueueSize
        {
            get { return m_maxQueueSize; }
            set { m_maxQueueSize = value; }
        }

        /// <summary>
        /// The next unused value that can be assigned to new nodes.
        /// </summary>
        [DataMember(Order = 3)]
        public uint NextUnusedId
        {
            get { return m_nextUnusedId; }
            set { m_nextUnusedId = value; }
        }
        #endregion

        #region Private Members
        private string m_saveFilePath;
        private uint m_maxQueueSize;
        private uint m_nextUnusedId;
        #endregion
    }
}
