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
    public interface IComDaGroupCallback : IDisposable
    {
        /// <summary>
        /// Called when a data change or read complete occurs.
        /// </summary>
        /// <param name="groupHandle">The group handle.</param>
        /// <param name="isRefresh">If set to <c>true</c> it is a response to a refresh request.</param>
        /// <param name="cancelId">The cancel id.</param>
        /// <param name="transactionId">The transaction id.</param>
        /// <param name="clientHandles">The client handles.</param>
        /// <param name="values">The values.</param>
        void ReadCompleted(
            int groupHandle,
            bool isRefresh,
            int cancelId,
            int transactionId,
		    int[] clientHandles,
		    DaValue[] values);

        /// <summary>
        /// Called when a write complete occurs.
        /// </summary>
        /// <param name="groupHandle">The group handle.</param>
        /// <param name="transactionId">The transaction id.</param>
        /// <param name="clientHandles">The client handles.</param>
        /// <param name="errors">The errors.</param>
	    void WriteCompleted(
		    int groupHandle,
		    int transactionId,
		    int[] clientHandles,
            int[] errors);

        /// <summary>
        /// Called when a cancel succeeds.
        /// </summary>
        /// <param name="groupHandle">The group handle.</param>
        /// <param name="transactionId">The transaction id.</param>
	    void CancelSucceeded(
		    int groupHandle,
		    int transactionId);
    }
}
