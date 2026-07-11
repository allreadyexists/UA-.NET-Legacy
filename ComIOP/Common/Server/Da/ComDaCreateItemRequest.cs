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
    public class ComDaCreateItemRequest
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
        /// Gets or sets the access path.
        /// </summary>
        /// <value>The access path.</value>
        public string AccessPath
        {
            get { return m_accessPath; }
            set { m_accessPath = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this item is active.
        /// </summary>
        /// <value><c>true</c> if active; otherwise, <c>false</c>.</value>
        public bool Active
        {
            get { return m_active; }
            set { m_active = value; }
        }

        /// <summary>
        /// Gets or sets the client handle.
        /// </summary>
        /// <value>The client handle.</value>
        public int ClientHandle
        {
            get { return m_clientHandle; }
            set { m_clientHandle = value; }
        }

        /// <summary>
        /// Gets or sets the requested data type.
        /// </summary>
        /// <value>The requested data type.</value>
        public short RequestedDataType
        {
            get { return m_requestedDataType; }
            set { m_requestedDataType = value; }
        }

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
        /// Gets or sets the canonical data type for the item.
        /// </summary>
        /// <value>The canonical data type.</value>
        public short CanonicalDataType
        {
            get { return m_canonicalDataType; }
            set { m_canonicalDataType = value; }
        }

        /// <summary>
        /// Gets or sets the access rights for the item.
        /// </summary>
        /// <value>The access rights.</value>
        public int AccessRights
        {
            get { return m_accessRights; }
            set { m_accessRights = value; }
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
        private string m_accessPath;
        private bool m_active;
        private int m_clientHandle;
        private short m_requestedDataType;
        private int m_serverHandle;
        private short m_canonicalDataType;
        private int m_accessRights;
        private int m_error;
        #endregion
    }
}
