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
using System.Windows.Forms;
using System.Threading;

namespace Opc.Ua.Client.Controls
{
    /// <summary>
    /// This class is used to work around a bug in the MS VPC implementation. 
    /// </summary>    
    /// <remarks>
    /// Clipborad operations will fail if this class is not used on VPCs with the 
    /// virtual machine additions installed.
    /// </remarks>
    public static class ClipboardHack
    {
        #region Public Methods
        /// <summary>
        /// Retrieves the data from the clipboard.
        /// </summary>
        public static object GetData(string format)
        {
            lock (m_lock)
            {
                m_format = format;
                m_data = null;
                m_error = null;

                Thread thread = new Thread(new ThreadStart(GetClipboardPrivate));
                thread.IsBackground = true;
                
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();

                if (m_error != null)
                {
                    throw new ServiceResultException(m_error, StatusCodes.BadUnexpectedError);
                }

                return m_data;
            }
        }
        
        /// <summary>
        /// Saves the data in the clipboard.
        /// </summary>
        public static void SetData(string format, object data)
        {
            lock (m_lock)
            {
                m_format = format;
                m_data = data;
                m_error = null;

                Thread thread = new Thread(new ThreadStart(SetClipboardPrivate));
                thread.IsBackground = true;
                
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();

                if (m_error != null)
                {
                    throw new ServiceResultException(m_error, StatusCodes.BadUnexpectedError);
                }
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Gets the data in the clipboard if it is the correct format.
        /// </summary>
        private static void GetClipboardPrivate()
        {
            try
            {
                m_error = null;

                if (String.IsNullOrEmpty(m_format) || !Clipboard.ContainsData(m_format))
                { 
                    m_data = null;
                    return;
                }
                
                m_data = Clipboard.GetData(m_format);
            }
            catch (Exception e)
            {
                m_error = e;
            }
        }
        
        /// <summary>
        /// Saves the data in the clipboard if it is the correct format.
        /// </summary>
        private static void SetClipboardPrivate()
        {
            try
            {
                m_error = null;

                if (String.IsNullOrEmpty(m_format) || m_data == null)
                { 
                    return;
                }

                Clipboard.SetData(m_format, m_data);
            }
            catch (Exception e)
            {
                m_error = e;
            }
        }
        #endregion

        #region Private Fields
        private static object m_lock = new object();
        private static string m_format = null;
        private static object m_data = null;
        private static Exception m_error = null;
        #endregion
    }
}
