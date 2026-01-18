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
using System.ServiceModel;
using System.Runtime.Serialization;

namespace Opc.Ua
{
    /// <summary>
    /// Stores a block of encypted data.
    /// </summary>
    public class EncryptedData
    {
        #region Private Members
        /// <summary>
        /// The algorithm used to encrypt the data.
        /// </summary>
        public string Algorithm
        {
            get { return m_algorithm; }
            set { m_algorithm = value; }
        }

        /// <summary>
        /// The encrypted data.
        /// </summary>
        public byte[] Data
        {
            get { return m_data; }
            set { m_data = value; }
        }
        #endregion

        #region Private Members
        private string m_algorithm;
        private byte[] m_data;
        #endregion
    }
}
