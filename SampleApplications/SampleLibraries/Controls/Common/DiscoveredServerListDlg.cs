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
using System.Reflection;

namespace Opc.Ua.Client.Controls
{
    /// <summary>
    /// Allows the user to browse a list of servers.
    /// </summary>
    public partial class DiscoveredServerListDlg : Form
    {
        #region Constructors
        /// <summary>
        /// Initializes the dialog.
        /// </summary>
        public DiscoveredServerListDlg()
        {
            InitializeComponent();
            this.Icon = ClientUtils.GetAppIcon();
        }
        #endregion
        
        #region Private Fields
        private string m_hostname;
        private ApplicationDescription m_server;
        private ApplicationConfiguration m_configuration;
        #endregion
        
        #region Public Interface
        /// <summary>
        /// Displays the dialog.
        /// </summary>
        public ApplicationDescription ShowDialog(string hostname, ApplicationConfiguration configuration)
        {
            m_configuration = configuration;

            if (String.IsNullOrEmpty(hostname))
            {
                hostname = System.Net.Dns.GetHostName();
            }

            m_hostname = hostname;
            List<string> hostnames = new List<string>();

            HostNameCTRL.Initialize(hostname, hostnames);
            ServersCTRL.Initialize(hostname, configuration);

            OkBTN.Enabled = false;

            if (ShowDialog() != DialogResult.OK)
            {
                return null;
            }
  
            return m_server;
        }
        #endregion
        
        #region Event Handlers
        private void OkBTN_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.OK;
            }
            catch (Exception exception)
            {
                GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void HostNameCTRL_HostSelected(object sender, SelectHostCtrlEventArgs e)
        {
            try
            {
                if (m_hostname != e.Hostname)
                {
                    m_hostname = e.Hostname;
                    ServersCTRL.Initialize(m_hostname, m_configuration);
                    m_server = null;
                    OkBTN.Enabled = false;
                }
            }
            catch (Exception exception)
            {
                GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void HostNameCTRL_HostConnected(object sender, SelectHostCtrlEventArgs e)
        {
            try
            {
                m_hostname = e.Hostname;
                ServersCTRL.Initialize(m_hostname, m_configuration);
                m_server = null;
                OkBTN.Enabled = false;
            }
            catch (Exception exception)
            {
                GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void ServersCTRL_ItemsSelected(object sender, ListItemActionEventArgs e)
        {
            try
            {
                m_server = null;

                foreach (ApplicationDescription server in e.Items)
                {
                    m_server = server;
                    break;
                }

                OkBTN.Enabled = m_server != null;
            }
            catch (Exception exception)
            {
                GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void ServersCTRL_ItemsPicked(object sender, ListItemActionEventArgs e)
        {
            try
            {
                m_server = null;

                foreach (ApplicationDescription server in e.Items)
                {
                    m_server = server;
                    break;
                }

                if (m_server != null)
                {
                    DialogResult = DialogResult.OK;
                }
            }
            catch (Exception exception)
            {
                GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }
        #endregion
    }
}
