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

namespace Opc.Ua.Client
{
    /// <summary>
    /// Attempts to reconnect to the server.
    /// </summary>
    public class SessionReconnectHandler : IDisposable
    {
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
            if (disposing)
            {
                lock (m_lock)
                {
                    if (m_reconnectTimer != null)
                    {
                        m_reconnectTimer.Dispose();
                        m_reconnectTimer = null;
                    }
                }
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets the session managed by the handler.
        /// </summary>
        /// <value>The session.</value>
        public Session Session
        {
            get { return m_session; }
        }

        /// <summary>
        /// Begins the reconnect process.
        /// </summary>
        public void BeginReconnect(Session session, int reconnectPeriod, EventHandler callback)
        {
            lock (m_lock)
            {
                if (m_reconnectTimer != null)
                {
                    throw new ServiceResultException(StatusCodes.BadInvalidState);
                }

                m_session = session;
                m_reconnectFailed = false;
                m_reconnectPeriod = reconnectPeriod;
                m_callback = callback;
                m_reconnectTimer = new System.Threading.Timer(OnReconnect, null, reconnectPeriod, Timeout.Infinite);
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Called when the reconnect timer expires.
        /// </summary>
        private void OnReconnect(object state)
        {
            try
            {
                // check for exit.
                if (m_reconnectTimer == null)
                {
                    return;
                }

                // do the reconnect.
                if (DoReconnect())
                {
                    lock (m_lock)
                    {
                        if (m_reconnectTimer != null)
                        {
                            m_reconnectTimer.Dispose();
                            m_reconnectTimer = null;
                        }
                    }

                    // notify the caller.
                    m_callback(this, null);
                }
            }
            catch (Exception exception)
            {
                Utils.Trace(exception, "Unexpected error during reconnect.");
            }
            finally
            {
                lock (m_lock)
                {
                    if (m_reconnectTimer != null)
                    {
                        m_reconnectTimer.Change(m_reconnectPeriod, Timeout.Infinite);
                    }
                }
            }
        }

        /// <summary>
        /// Reconnects to the server.
        /// </summary>
        private bool DoReconnect()
        {
            // try a reconnect.
            if (!m_reconnectFailed)
            {
                try
                {
                    m_session.Reconnect();

                    // monitored items should start updating on their own.
                    return true;
                }
                catch (Exception exception)
                {
                    // recreate the session if it has been closed.
                    ServiceResultException sre = exception as ServiceResultException;

                    // check if the server endpoint could not be reached.
                    if ((sre != null && (sre.StatusCode == StatusCodes.BadTcpInternalError || sre.StatusCode == StatusCodes.BadCommunicationError)) ||
                        exception is System.ServiceModel.EndpointNotFoundException)
                    {
                        // check if reconnecting is still an option.
                        if (m_session.LastKeepAliveTime.AddMilliseconds(m_session.SessionTimeout) > DateTime.UtcNow)
                        {
                            Utils.Trace("Calling OnReconnectSession in {0} ms.", m_reconnectPeriod);

                            return false;
                        }
                    }

                    m_reconnectFailed = true;
                }
            }

            // re-create the session.
            try
            {
                Session session = Session.Recreate(m_session);
                m_session.Close();
                m_session = session;
                return true;
            }
            catch (Exception exception)
            {
                Utils.Trace("Could not reconnect the Session. {0}", exception.Message);
                return false;
            }
        }
        #endregion

        #region Private Fields
        private object m_lock = new object();
        private Session m_session;
        private bool m_reconnectFailed;
        private int m_reconnectPeriod;
        private Timer m_reconnectTimer;
        private EventHandler m_callback;
        #endregion
    }
}
