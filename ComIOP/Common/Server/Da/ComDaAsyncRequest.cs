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
    /// Specifies the parameters for an async request.
    /// </summary>
    public class ComDaAsnycRequest
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets the cancel id.
        /// </summary>
        /// <value>The cancel id.</value>
        public int CancelId
        {
            get { return m_cancelId; }
            set { m_cancelId = value; }
        }

        /// <summary>
        /// Gets or sets a flag indicating that the request was cancelled.
        /// </summary>
        /// <value>True if cancelled.</value>
        public bool Cancelled
        {
            get { return m_cancelled; }
            set { m_cancelled = value; }
        }

        /// <summary>
        /// Gets or sets the transaction id.
        /// </summary>
        /// <value>The transaction id.</value>
        public int TransactionId
        {
            get { return m_transactionId; }
            set { m_transactionId = value; }
        }

        /// <summary>
        /// Gets or sets the server handles.
        /// </summary>
        /// <value>The server handles.</value>
        public int[] ServerHandles
        {
            get { return m_serverHandles; }
            set { m_serverHandles = value; }
        }

        /// <summary>
        /// Gets or sets the client handles.
        /// </summary>
        /// <value>The client handles.</value>
        public int[] ClientHandles
        {
            get { return m_clientHandles; }
            set { m_clientHandles = value; }
        }
        #endregion

        #region Private Fields
        private int m_transactionId;
        private int m_cancelId;
        private bool m_cancelled;
        private int[] m_serverHandles;
        private int[] m_clientHandles;
        #endregion
    }

    /// <summary>
    /// Specifies the parameters for an async read request.
    /// </summary>
    public class ComDaAsnycReadRequest : ComDaAsnycRequest
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets the max age.
        /// </summary>
        /// <value>The max age.</value>
        public uint MaxAge
        {
            get { return m_maxAge; }
            set { m_maxAge = value; }
        }

        /// <summary>
        /// Gets or sets a flag indicating that it is a read request.
        /// </summary>
        /// <value>The refresh flag.</value>
        public bool IsRefresh
        {
            get { return m_isRefresh; }
            set { m_isRefresh = value; }
        }

        /// <summary>
        /// Gets or sets a flag indicating that it is the first update after activation.
        /// </summary>
        public bool IsFirstUpdate { get; set; }
        #endregion

        #region Private Fields
        private uint m_maxAge;
        private bool m_isRefresh;
        #endregion
    }

    /// <summary>
    /// Specifies the parameters for an async write request.
    /// </summary>
    public class ComDaAsnycWriteRequest : ComDaAsnycRequest
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>The results.</value>
        public DaValue[] Values
        {
            get { return m_values; }
            set { m_values = value; }
        }
        #endregion

        #region Private Fields
        private DaValue[] m_values;
        #endregion
    }
}
