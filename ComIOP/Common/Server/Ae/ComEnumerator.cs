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
using System.Collections;
using System.Text;
using System.Runtime.InteropServices;

using OpcRcw.Comn;

namespace Opc.Ua.Com.Server.Ae
{
	/// <summary>
	/// A class that implements the COM-DA interfaces.
	/// </summary>
	public class EnumUnknown : IEnumUnknown
	{	
		/// <summary>
		/// Initializes the object with a set of interface pointers.
		/// </summary>
		internal EnumUnknown(ICollection unknowns)
		{
			if (unknowns != null)
			{
				foreach (object unknown in unknowns)
				{
					m_unknowns.Add(unknown);
				}
			}
		}

		#region EnumUnknown Members
		/// <remarks/>
		public void Skip(int celt)
		{
			lock (m_lock)
			{
				try
				{
					m_index += celt;

					if (m_index > m_unknowns.Count)
					{
						m_index = m_unknowns.Count;
					}
				}
				catch (Exception e)
				{
					throw ComUtils.CreateComException(e);
				}
			}
		}

		/// <remarks/>
		public void Clone(out IEnumUnknown ppenum)
		{
			lock (m_lock)
			{
				try
				{
					ppenum = new EnumUnknown(m_unknowns);
				}
				catch (Exception e)
				{
					throw ComUtils.CreateComException(e);
				}
			}
		}

		/// <remarks/>
		public void Reset()
		{
			lock (m_lock)
			{
				try
				{
					m_index = 0;
				}
				catch (Exception e)
				{
					throw ComUtils.CreateComException(e);
				}
			}
		}

		/// <remarks/>
		public void RemoteNext(int celt, IntPtr rgelt, out int pceltFetched)
		{
			lock (m_lock)
			{
				try
				{
					pceltFetched = 0;
					
					if (m_index >= m_unknowns.Count)
					{
                        return;
					}

                    object[] unknowns = new object[celt];

					for (int ii = 0; ii < m_unknowns.Count - m_index && ii < unknowns.Length; ii++)
					{
						unknowns[ii] = m_unknowns[m_index+ii];
						pceltFetched++;
					}
                    
		            IntPtr[] pointers = new IntPtr[pceltFetched];
                    Marshal.Copy(rgelt, pointers, 0, pceltFetched);

		            for (int ii = 0; ii < pceltFetched; ii++)
		            {
                        pointers[ii] = Marshal.GetIUnknownForObject(unknowns[ii]);
		            }

                    Marshal.Copy(pointers, 0, rgelt, pceltFetched);

					m_index += pceltFetched;
				}
				catch (Exception e)
				{
					throw ComUtils.CreateComException(e);
				}
			}
		}
		#endregion
		
		#region Private Members
        private object m_lock = new object();
		private ArrayList m_unknowns = new ArrayList();
		private int m_index = 0;
		#endregion
	}

	/// <summary>
	/// A class that implements the COM-DA interfaces.
	/// </summary>
	public class EnumString : OpcRcw.Comn.IEnumString
	{	
		/// <summary>
		/// Initializes the object with a set of interface pointers.
		/// </summary>
		internal EnumString(ICollection strings)
		{
			if (strings != null)
			{
				foreach (object instance in strings)
				{
					m_strings.Add(instance);
				}
			}
		}

		#region EnumString Members
		/// <remarks/>
		public void Skip(int celt)
		{
			lock (m_lock)
			{
				try
				{
					m_index += celt;

					if (m_index > m_strings.Count)
					{
						m_index = m_strings.Count;
					}
				}
				catch (Exception e)
				{
					throw ComUtils.CreateComException(e);
				}
			}
		}

		/// <remarks/>
		public void Clone(out OpcRcw.Comn.IEnumString ppenum)
		{
			lock (m_lock)
			{
				try
				{
					ppenum = new EnumString(m_strings);
				}
				catch (Exception e)
				{
					throw ComUtils.CreateComException(e);
				}
			}
		}

        /// <remarks/>
		public void Reset()
		{
			lock (m_lock)
			{
				try
				{
					m_index = 0;
				}
				catch (Exception e)
				{
					throw ComUtils.CreateComException(e);
				}
			}
		}
        
		/// <remarks/>
		public int RemoteNext(int celt, IntPtr rgelt, out int pceltFetched)
		{
            pceltFetched = 0;

			lock (m_lock)
			{
				try
				{                    
					pceltFetched = 0;

					if (m_index >= m_strings.Count)
					{
				        return ResultIds.S_FALSE;
					}
                    
                    string [] strings = new string[celt];

					for (int ii = 0; ii < m_strings.Count - m_index && ii < strings.Length; ii++)
					{
						strings[ii] = (string)m_strings[m_index+ii];
						pceltFetched++;
					}
                        
		            IntPtr[] pointers = new IntPtr[pceltFetched];
                    Marshal.Copy(rgelt, pointers, 0, pceltFetched);

		            for (int ii = 0; ii < pceltFetched; ii++)
		            {
                        pointers[ii] = Marshal.StringToCoTaskMemUni(strings[ii]);
		            }

                    Marshal.Copy(pointers, 0, rgelt, pceltFetched);

					m_index += pceltFetched;
				   
                    return ResultIds.S_OK;
				}
				catch (Exception)
				{
				    return ResultIds.E_FAIL;
				}
			}
		}
		#endregion
		
		#region Private Members
        private object m_lock = new object();
		private ArrayList m_strings = new ArrayList();
		private int m_index = 0;
		#endregion
	}
}
