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
    /// Used to report asynchronous events produced by an HDA server.
    /// </summary>
    public interface IComHdaDataCallback : IDisposable
    {
        /// <summary>
        /// Called when a data change operation completes.
        /// </summary>
	    void OnDataChange(
		    int transactionId, 
		    List<HdaReadRequest> results);

        /// <summary>
        /// Called when a read operation completes.
        /// </summary>
	    void OnReadComplete(
		    int transactionId, 
		    List<HdaReadRequest> results);

        /// <summary>
        /// Called when a read modified operation completes.
        /// </summary>
	    void OnReadModifiedComplete(
		    int transactionId, 
		    List<HdaReadRequest> results);

        /// <summary>
        /// Called when a read attributes operation completes.
        /// </summary>
	    void OnReadAttributeComplete(
		    int transactionId, 
		    List<HdaReadRequest> results);

        /// <summary>
        /// Called when a read annotations operation completes.
        /// </summary>
	    void OnReadAnnotations(
		    int transactionId, 
		    List<HdaReadRequest> results);

        /// <summary>
        /// Called when an insert annotations operation completes.
        /// </summary>
	    void OnInsertAnnotations(
            int transactionId,
            List<HdaUpdateRequest> results); 

        /// <summary>
        /// Called when a update operation completes.
        /// </summary>
	    void OnUpdateComplete(
		    int transactionId, 
		    List<HdaUpdateRequest> results); 

        /// <summary>
        /// Called when a cancel operation completes.
        /// </summary>
	    void OnCancelComplete(int transactionId);
    }
}
