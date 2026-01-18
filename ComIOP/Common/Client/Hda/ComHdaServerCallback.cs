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
using Opc.Ua.Server;
using Opc.Ua.Com;
using Opc.Ua.Com.Client;
using OpcRcw.Hda;

namespace Opc.Ua.Com.Client
{
    /// <summary>
    /// A class that implements the IOPCHDA_DataCallback interface.
    /// </summary>
    internal class ComHdaDataCallback : OpcRcw.Hda.IOPCHDA_DataCallback, IDisposable
    {
	    #region Constructors
	    /// <summary>
	    /// Initializes the object with the containing subscription object.
	    /// </summary>
        public ComHdaDataCallback(ComHdaClient server)
	    { 
            // save group.
            m_server = server;

		    // create connection point.
            m_connectionPoint = new ConnectionPoint(server.Unknown, typeof(OpcRcw.Hda.IOPCHDA_DataCallback).GUID);

		    // advise.
		    m_connectionPoint.Advise(this);
	    }
	    #endregion
        
        #region IDisposable Members        
        /// <summary>
        /// Frees any unmanaged resources.
        /// </summary>
        public void Dispose()
        {   
            Dispose(true);
        }

        /// <summary>
        /// An overrideable version of the Dispose.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (m_connectionPoint != null)
            {
                if (disposing)
                {
                    m_connectionPoint.Dispose();
                    m_connectionPoint = null;
                }
            }
        }
        #endregion

	    #region Public Properties
        /// <summary>
        /// Whether the callback is connected.
        /// </summary>
        public bool Connected 
        {
            get 
            {
                return m_connectionPoint != null;
            }
        }
        #endregion

	    #region IOPCDataCallback Members
        /// <summary>
        /// Called when a data change arrives.
        /// </summary>
        public void OnDataChange(
            int dwTransactionID,
            int hrStatus,
            int dwNumItems,
            OPCHDA_ITEM[] pItemValues,
            int[] phrErrors)
        {
            try
            {
                // TBD
            }
            catch (Exception e)
            {
                Utils.Trace(e, "Unexpected error processing OnDataChange callback.");
            }
        }

        /// <summary>
        /// Called when an async read completes.
        /// </summary>
        public void OnReadComplete(
            int dwTransactionID, 
            int hrStatus,
            int dwNumItems,  
            OPCHDA_ITEM[] pItemValues,
            int[] phrErrors)
        {
            try
            {
                // TBD
            }
            catch (Exception e)
            {
                Utils.Trace(e, "Unexpected error processing OnReadComplete callback.");
            }
        }

        /// <summary>
        /// Called when an async read modified completes.
        /// </summary>
        public void OnReadModifiedComplete(
            int dwTransactionID, 
            int hrStatus,
            int dwNumItems, 
            OPCHDA_MODIFIEDITEM[] pItemValues,
            int[] phrErrors)
        {
            try
            {
                // TBD
            }
            catch (Exception e)
            {
                Utils.Trace(e, "Unexpected error processing OnReadModifiedComplete callback.");
            }
        }

        /// <summary>
        /// Called when an async read attributes completes.
        /// </summary>
        public void OnReadAttributeComplete(
            int dwTransactionID, 
            int hrStatus,
            int hClient, 
            int dwNumItems, 
            OPCHDA_ATTRIBUTE[] pAttributeValues,
            int[] phrErrors)
        {
            try
            {
                // TBD
            }
            catch (Exception e)
            {
                Utils.Trace(e, "Unexpected error processing OnReadAttributeComplete callback.");
            }
        }
        
        /// <summary>
        /// Called when an async read annotations completes.
        /// </summary>
        public  void OnReadAnnotations(
            int dwTransactionID, 
            int hrStatus,
            int dwNumItems, 
            OPCHDA_ANNOTATION[] pAnnotationValues,
            int[] phrErrors)
        {
            try
            {
                // TBD
            }
            catch (Exception e)
            {
                Utils.Trace(e, "Unexpected error processing OnReadAnnotations callback.");
            }
        }

        /// <summary>
        /// Called when an async insert annotations completes.
        /// </summary>
        public void OnInsertAnnotations (
            int dwTransactionID, 
            int hrStatus,
            int dwCount, 
            int[] phClients, 
            int[] phrErrors)
        {
            try
            {
                // TBD
            }
            catch (Exception e)
            {
                Utils.Trace(e, "Unexpected error processing OnInsertAnnotations callback.");
            }
        }
        
        /// <summary>
        /// Called when a playback result arrives.
        /// </summary>
        public void OnPlayback (
            int dwTransactionID, 
            int hrStatus,
            int dwNumItems, 
            IntPtr ppItemValues, 
            int[] phrErrors)
        {
            try
            {
                // TBD
            }
            catch (Exception e)
            {
                Utils.Trace(e, "Unexpected error processing OnPlayback callback.");
            }
        }
        
        /// <summary>
        /// Called when a async update completes.
        /// </summary>
        public void OnUpdateComplete (
            int dwTransactionID, 
            int hrStatus,
            int dwCount, 
            int[] phClients, 
            int[] phrErrors)
        {
            try
            {
                // TBD
            }
            catch (Exception e)
            {
                Utils.Trace(e, "Unexpected error processing OnUpdateComplete callback.");
            }
        }

        /// <summary>
        /// Called when a async opeartion cancel completes.
        /// </summary>
        public void OnCancelComplete(
            int dwCancelID)
        {
            try
            {
                // TBD
            }
            catch (Exception e)
            {
                Utils.Trace(e, "Unexpected error processing OnCancelComplete callback.");
            }
        }
	    #endregion

	    #region Private Members
	    private ComHdaClient m_server;
	    private ConnectionPoint m_connectionPoint;
	    #endregion
    }
}
