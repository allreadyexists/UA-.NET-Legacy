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
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;

namespace Opc.Ua.Configuration
{
    /// <summary>
    /// Enumerates the hosts available on the network.
    /// </summary>
    public class HostEnumerator
    {
		#region Constructors
        /// <summary>
        /// Creates the object.
        /// </summary>
        public HostEnumerator()
        {
        }
		#endregion

        #region Public Methods
        /// <summary>
        /// Raised when a batch of hosts has been discovered (called from a background thread).
        /// </summary>
        public event EventHandler<HostEnumeratorEventArgs> HostsDiscovered
        {
            add { m_HostsDiscovered += value; }
            remove { m_HostsDiscovered -= value; }
        }

        /// <summary>
        /// Starts enumerating the hosts.
        /// </summary>
        public void Start(string domain)
        {
            Interlocked.Exchange(ref m_stopped, 0);
            Interlocked.Exchange(ref m_domain, domain);
            System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(OnEnumerate));
        }

        /// <summary>
        /// Stops enumerating the hosts.
        /// </summary>
        public void Stop()
        {
            Interlocked.Exchange(ref m_stopped, 1);
        }
		#endregion
        
		#region Private Methods
		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
	    private struct SERVER_INFO_100
		{
			public uint sv100_platform_id;
			[MarshalAs(UnmanagedType.LPWStr)]
			public string sv100_name;
		} 	

		private const uint LEVEL_SERVER_INFO_100 = 100;
		private const uint LEVEL_SERVER_INFO_101 = 101;

		private const int  MAX_PREFERRED_LENGTH  = -1;

		private const uint SV_TYPE_WORKSTATION   = 0x00000001;
		private const uint SV_TYPE_SERVER        = 0x00000002;

		[DllImport("Netapi32.dll")]
		private static extern int NetServerEnum(
			IntPtr     servername,
			uint       level,
			out IntPtr bufptr,
			int        prefmaxlen,
			out int    entriesread,
			out int    totalentries,
			uint       servertype,
			[MarshalAs(UnmanagedType.LPWStr)]
			string     domain,
			IntPtr     resume_handle);

		[DllImport("Netapi32.dll")]	
		private static extern int NetApiBufferFree(IntPtr buffer);
        
        private const int ERROR_MORE_DATA = 234;
        private const int NERR_Success = 0;
        
		/// <summary>
		/// Enumerates computers on the local network.
		/// </summary>
		private void OnEnumerate(object state)
		{
			IntPtr pInfo;

			int entriesRead = 0;
			int totalEntries = 0;
            int resumeHandle = 0;
            int result = ERROR_MORE_DATA;
            
            GCHandle dwResumeHandle = GCHandle.Alloc(resumeHandle, GCHandleType.Pinned);

            try
            {
                while (m_stopped == 0 && result == ERROR_MORE_DATA)
                {
                    // enumerate the first batch of servers.
			        result = NetServerEnum(
				        IntPtr.Zero,
				        LEVEL_SERVER_INFO_100,
				        out pInfo,
				        MAX_PREFERRED_LENGTH,
				        out entriesRead,
				        out totalEntries,
				        SV_TYPE_WORKSTATION | SV_TYPE_SERVER,
				        m_domain,
				        dwResumeHandle.AddrOfPinnedObject());		

                    // check for fatal error.
			        if ((result != NERR_Success) && (result != ERROR_MORE_DATA))
			        {
                        Utils.Trace("Could not enumerate hosts on network. Error = {0}", result);
                        return;
			        }

                    // copy host names from the returned structures.
			        string[] hostnames = new string[entriesRead];

			        IntPtr pos = pInfo;

			        for (int ii = 0; ii < entriesRead; ii++)
			        {
				        SERVER_INFO_100 info = (SERVER_INFO_100)Marshal.PtrToStructure(pos, typeof(SERVER_INFO_100));
        				
				        hostnames[ii] = info.sv100_name;

				        pos = (IntPtr)(pos.ToInt64() + Marshal.SizeOf(typeof(SERVER_INFO_100)));
			        }

			        NetApiBufferFree(pInfo);

                    // raise an event.
                    if (m_stopped == 0 && m_HostsDiscovered != null)
                    {
                        try
                        {
                            m_HostsDiscovered(this, new HostEnumeratorEventArgs(hostnames));
                        }
                        catch (Exception e)
                        {
                            Utils.Trace(e, "Unexpected exception raising HostsDiscovered event.");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Utils.Trace(e, "Unexpected exception calling NetServerEnum.");
            }
            finally
            {
                if (dwResumeHandle.IsAllocated)
                {
                    dwResumeHandle.Free();
                }
            }
		}
		#endregion
        
        #region HostEnumeratorEventArgs Class
        private int m_stopped;
        private string m_domain;
        private event EventHandler<HostEnumeratorEventArgs> m_HostsDiscovered;
		#endregion
    }

    #region HostEnumeratorEventArgs Class
    /// <summary>
    /// The arguments provided when a batch of hosts is discovered.
    /// </summary>
    public class HostEnumeratorEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes the object with a batch of host names.
        /// </summary>
        public HostEnumeratorEventArgs(IList<string> hostnames)
        {
            m_hostnames = hostnames;
        }

        /// <summary>
        /// The list of hostnames found.
        /// </summary>
        public IList<string> Hostnames
        {
            get { return m_hostnames; }
        }

        private IList<string> m_hostnames;
    }
	#endregion
}
