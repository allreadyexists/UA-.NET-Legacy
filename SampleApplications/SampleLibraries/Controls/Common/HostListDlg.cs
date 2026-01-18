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

using Opc.Ua.Configuration;

namespace Opc.Ua.Client.Controls
{
    /// <summary>
    /// Allows the user to browse a list of servers.
    /// </summary>
    public partial class HostListDlg : Form
    {
        #region Constructors
        /// <summary>
        /// Initializes the dialog.
        /// </summary>
        public HostListDlg()
        {
            InitializeComponent();
            this.Icon = ClientUtils.GetAppIcon();
        }
        #endregion
        
        #region Private Fields
        private string m_domain;
        private string m_hostname;
        #endregion
        
        #region Public Interface
        /// <summary>
        /// Displays the dialog.
        /// </summary>
        public string ShowDialog(string domain)
        {
            if (String.IsNullOrEmpty(domain))
            {
                domain = ConfigUtils.GetComputerWorkgroupOrDomain();
            }

            m_domain = domain;

            DomainNameCTRL.Initialize(m_domain, null);
            HostsCTRL.Initialize(m_domain);
            OkBTN.Enabled = false;

            if (ShowDialog() != DialogResult.OK)
            {
                return null;
            }
  
            return m_hostname;
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

        private void DomainNameCTRL_HostSelected(object sender, SelectHostCtrlEventArgs e)
        {
            try
            {
                if (m_domain != e.Hostname)
                {
                    m_domain = e.Hostname;
                    HostsCTRL.Initialize(m_domain);
                    m_hostname = null;
                    OkBTN.Enabled = false;
                }
            }
            catch (Exception exception)
            {
                GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void DomainNameCTRL_HostConnected(object sender, SelectHostCtrlEventArgs e)
        {
            try
            {
                m_domain = e.Hostname;
                HostsCTRL.Initialize(m_domain);
                m_hostname = null;
                OkBTN.Enabled = false;
            }
            catch (Exception exception)
            {
                GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void HostsCTRL_ItemsSelected(object sender, ListItemActionEventArgs e)
        {
            try
            {
                m_hostname = null;

                foreach (string hostname in e.Items)
                {
                    m_hostname = hostname;
                    break;
                }

                OkBTN.Enabled = !String.IsNullOrEmpty(m_hostname);
            }
            catch (Exception exception)
            {
                GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void HostsCTRL_ItemsPicked(object sender, ListItemActionEventArgs e)
        {
            try
            {
                m_hostname = null;

                foreach (string hostname in e.Items)
                {
                    m_hostname = hostname;
                    break;
                }

                if (!String.IsNullOrEmpty(m_hostname))
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
