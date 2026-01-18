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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Opc.Ua.Client;
using Opc.Ua.Client.Controls;

namespace Opc.Ua.Sample.Controls
{
    public partial class AddressSpaceDlg : Form
    {
        #region Constructors
        public AddressSpaceDlg()
        {
            InitializeComponent();
            this.Icon = ClientUtils.GetAppIcon();
            m_SessionClosing = new EventHandler(Session_Closing);
        }
        #endregion
        
        #region Private Fields
        private Session m_session;
        private EventHandler m_SessionClosing;
        #endregion

        #region Public Interface
        /// <summary>
        /// Displays the address space with the specified view
        /// </summary>
        public void Show(Session session, BrowseViewType viewType, NodeId viewId)
        {   
            if (session == null) throw new ArgumentNullException("session");
            
            if (m_session != null)
            {
                m_session.SessionClosing -= m_SessionClosing;
            }

            m_session = session;            
            m_session.SessionClosing += m_SessionClosing;
            
            BrowseCTRL.SetView(session, viewType, viewId);
            Show();
            BringToFront();
        }
        #endregion
        
        private void Session_Closing(object sender, EventArgs e)
        {
            if (Object.ReferenceEquals(sender, m_session))
            {
                m_session.SessionClosing -= m_SessionClosing;
                m_session = null;
                Close();
            }
        }

        private void AddressSpaceDlg_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_session != null)
            {
                m_session.SessionClosing -= m_SessionClosing;
                m_session = null;
            }
        }
    }
}
