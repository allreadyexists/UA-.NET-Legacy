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
using System.Threading;

namespace Opc.Ua
{
    /// <summary>
    /// A class that allows threads to determine who, if anyone, has the lock on an object.
    /// </summary>
    public class SafeLock 
    {
        /// <summary>
        /// Acquires the lock.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the lock state is inconsistent.</exception>
        public void Enter()
        {
            System.Threading.Monitor.Enter(this);

            if (m_refs == 0)
            {
                int result = Interlocked.CompareExchange(ref m_owner, Thread.CurrentThread.ManagedThreadId, -1);

                if (result != -1)
                {
                    throw new InvalidOperationException("Operation failed because Lock object is in an invalid state.");
                }
            }

            m_refs++;
        }

        /// <summary>
        /// Attempts to acquire the lock.
        /// </summary>
        /// <param name="timeout">The number of milliseconds to wait.</param>
        /// <returns>True if the lock was acquired.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the lock state is inconsistent.</exception>
        public bool TryEnter(int timeout)
        {
            if (!System.Threading.Monitor.TryEnter(this, timeout))
            {
                return false;
            }

            if (m_refs == 0)
            {
                int result = Interlocked.CompareExchange(ref m_owner, Thread.CurrentThread.ManagedThreadId, -1);

                if (result != -1)
                {
                    throw new InvalidOperationException("Operation failed because Lock object is in an invalid state.");
                }
            }

            m_refs++;

            return true;
        }
        
        /// <summary>
        /// Releases the lock.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the lock state is inconsistent.</exception>
        public void Exit()
        {
            m_refs--;

            if (m_refs == 0)
            {
                int threadId = Thread.CurrentThread.ManagedThreadId;

                int result = Interlocked.CompareExchange(ref m_owner, -1, threadId);

                if (result != threadId)
                {
                    throw new InvalidOperationException("Operation failed because Lock object is in an invalid state.");
                }
            }

            System.Threading.Monitor.Exit(this);
        }

        /// <summary>
        /// Checks if the current thread has acquired the lock.
        /// </summary>
        /// <returns>True if the current thread owns the lock.</returns>
        public bool HasLock()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;

            int result = Interlocked.CompareExchange(ref m_owner, threadId, threadId);

            if (result != threadId)
            {
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// The ManagedThreadId for the Thread that owns the lock. -1 if no thread owns the lock.
        /// </summary>
        private int m_owner = -1;
        
        /// <summary>
        /// The number of times Enter has been called.
        /// </summary>
        private int m_refs = 0;
    }

    /// <summary>
    /// A helper object that can be used in a using() clause to acquire/release a SafeLock.
    /// </summary>
    public sealed class Lock : IDisposable
    {
        /// <summary>
        /// Acquires the lock on the SafeLock object.
        /// </summary>
        public Lock(SafeLock safeLock)
        {
            m_safeLock = safeLock;
            m_safeLock.Enter();
        }
        
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
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!m_disposed)
                {
                    m_safeLock.Exit();
                    m_disposed = true;
                }
            }
        }
        #endregion
        
        #region Private Fields
        private SafeLock m_safeLock;
        private bool m_disposed;
        #endregion
    }
}
