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

namespace Opc.Ua.Com.Client
{        
    /// <summary>
    /// A class that implements the IOPCShutdown interface.
    /// </summary>
    public class ShutdownCallback : OpcRcw.Comn.IOPCShutdown, IDisposable
    {
	    #region Constructors
	    /// <summary>
	    /// Initializes the object with the containing subscription object.
	    /// </summary>
	    public ShutdownCallback(object server, ServerShutdownEventHandler handler)
	    { 
		    try
		    {
                m_server  = server;
                m_handler = handler;

			    // create connection point.
			    m_connectionPoint = new ConnectionPoint(server, typeof(OpcRcw.Comn.IOPCShutdown).GUID);

			    // advise.
			    m_connectionPoint.Advise(this);
		    }
		    catch (Exception e)
		    {
			    throw new ServiceResultException(e, StatusCodes.BadOutOfService);
		    }
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
                m_connectionPoint.Dispose();
                m_connectionPoint = null;
            }
        }
        #endregion

	    #region IOPCShutdown Members
	    /// <summary>
	    /// Called when a data changed event is received.
	    /// </summary>
	    public void ShutdownRequest(string szReason)
	    {
		    try
		    {
                if (m_handler != null)
                {
                    m_handler(m_server, szReason);
                }
		    }
		    catch (Exception e) 
		    { 
                Utils.Trace(e, "Unexpected error processing callback.");
		    }
	    }
	    #endregion

	    #region Private Members
	    private object m_server;
	    private ServerShutdownEventHandler m_handler;
	    private ConnectionPoint m_connectionPoint;
	    #endregion
    }
    
    /// <summary>
    /// A delegate used to receive server shutdown events.
    /// </summary>
    public delegate void ServerShutdownEventHandler(object sender, string reason);
}
