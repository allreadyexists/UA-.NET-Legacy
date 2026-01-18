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
using Opc.Ua;
using Opc.Ua.Com;
using Opc.Ua.Com.Client;

namespace Quickstarts.ComDataAccessServer
{
    /// <summary>
    /// Prompts the user to select a COM server to wrap.
    /// </summary>
    public partial class SelectComServerDlg : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectComServerDlg"/> class.
        /// </summary>
        public SelectComServerDlg()
        {
            InitializeComponent();
        }

        private bool m_delete;
        private ApplicationConfiguration m_configuration;

        /// <summary>
        /// Updates the list of servers.
        /// </summary>
        private void UpdateServers()
        {
            // populate the list of servers.
            Opc.Ua.Com.ServerFactory factory = new ServerFactory();

            try
            {
                ServersLV.Items.Clear();

                string hostName = HostCB.Text;

                if (HostCB.SelectedIndex != -1)
                {
                    hostName = (string)HostCB.SelectedItem;
                }

                factory.Connect(hostName, null);

                Uri[] serverUrls = factory.GetAvailableServers(Specification.Da20, Specification.Da30);

                for (int ii = 0; ii < serverUrls.Length; ii++)
                {
                    ComServerDescription server = factory.ParseUrl(serverUrls[ii]);

                    ListViewItem item = new ListViewItem(server.ProgId);
                    item.SubItems.Add(server.Description);
                    item.Tag = server;

                    ServersLV.Items.Add(item);
                }

                for (int ii = 0; ii < ServersLV.Columns.Count; ii++)
                {
                    ServersLV.Columns[ii].Width = -2;
                }
            }
            finally
            {
                factory.Disconnect();
            }
        }

        /// <summary>
        /// Shows the dialog.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns>True if the configuration was updated.</returns>
        public bool ShowDialogForUpdate(ApplicationConfiguration configuration)
        {
            this.Text = "Select COM Server to Wrap";
            m_delete = false;
            m_configuration = configuration;
            AddressPN.Visible = true;
            HostCB.Text = "localhost";

            UpdateServers();

            // show the dialog.
            if (ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Shows the dialog.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns>True if the configuration was updated.</returns>
        public bool ShowDialogForDelete(ApplicationConfiguration configuration)
        {
            this.Text = "Select Wrapped COM Server to Delete";
            m_delete = true;
            m_configuration = configuration;
            AddressPN.Visible = false;

            // extract the wrapper configuration from the application configuration.
            ComWrapperServerConfiguration wrapperConfig = m_configuration.ParseExtension<ComWrapperServerConfiguration>();

            if (wrapperConfig == null)
            {
                wrapperConfig = new ComWrapperServerConfiguration();
            }

            if (wrapperConfig.WrappedServers == null)
            {
                wrapperConfig.WrappedServers = new ComClientConfigurationCollection();
            }

            // populate the list of wrapped servers.
            for (int ii = 0; ii < wrapperConfig.WrappedServers.Count; ii++)
            {
                ComClientConfiguration server = wrapperConfig.WrappedServers[ii];

                ListViewItem item = new ListViewItem(server.ServerUrl);
                item.SubItems.Add(server.ServerName);
                item.Tag = server;

                ServersLV.Items.Add(item);
            }

            for (int ii = 0; ii < ServersLV.Columns.Count; ii++)
            {
                ServersLV.Columns[ii].Width = -2;
            }

            // show the dialog.
            if (ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Handles the Click event of the OkBTN control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OkBTN_Click(object sender, EventArgs e)
        {
            try
            {
                if (ServersLV.SelectedItems.Count != 1)
                {
                    return;
                }
                
                // extract the wrapper configuration from the application configuration.
                ComWrapperServerConfiguration wrapperConfig = m_configuration.ParseExtension<ComWrapperServerConfiguration>();

                if (wrapperConfig == null)
                {
                    wrapperConfig = new ComWrapperServerConfiguration();
                }

                if (wrapperConfig.WrappedServers == null)
                {
                    wrapperConfig.WrappedServers = new ComClientConfigurationCollection();
                }

                if (!m_delete)
                {
                    // get the selected server.
                    ComServerDescription server = (ComServerDescription)ServersLV.SelectedItems[0].Tag;

                    // create a new new COM client entry for the selected server.
                    ComDaClientConfiguration  clientConfig = new ComDaClientConfiguration();

                    clientConfig.ServerUrl = server.Url;
                    clientConfig.ServerName = server.VersionIndependentProgId;
                    clientConfig.MaxReconnectWait = 100000;

                    wrapperConfig.WrappedServers.Add(clientConfig);

                    // update the configuration.
                    m_configuration.UpdateExtension<ComWrapperServerConfiguration>(null, wrapperConfig);
                }
                else
                {
                    // get the selected server.
                    ComDaClientConfiguration server = (ComDaClientConfiguration)ServersLV.SelectedItems[0].Tag;

                    for (int ii = 0; ii < wrapperConfig.WrappedServers.Count; ii++)
                    {
                        if (wrapperConfig.WrappedServers[ii].ServerUrl == server.ServerUrl)
                        {
                            wrapperConfig.WrappedServers.RemoveAt(ii);
                            break;
                        }
                    }

                    // update the configuration.
                    m_configuration.UpdateExtension<ComWrapperServerConfiguration>(null, wrapperConfig);
                }

                // close the dialog.
                DialogResult = DialogResult.OK;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void HostCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                UpdateServers();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void RefreshBTN_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateServers();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
