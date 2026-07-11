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
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.IO;
using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Client.Controls;

namespace Quickstarts.PerfTestClient
{
    /// <summary>
    /// The main form for a simple Quickstart Client application.
    /// </summary>
    public partial class MainForm : Form
    {
        #region Constructors
        /// <summary>
        /// Creates an empty form.
        /// </summary>
        private MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creates a form which uses the specified client configuration.
        /// </summary>
        /// <param name="configuration">The configuration to use.</param>
        public MainForm(ApplicationConfiguration configuration)
        {
            InitializeComponent();
            ConnectServerCTRL.Configuration = m_configuration = configuration;
            ConnectServerCTRL.ServerUrl = "opc.tcp://diamond:62522/Quickstarts/PerfTestServer";
            this.Text = m_configuration.ApplicationName;
        }
        #endregion

        #region Private Fields
        private ApplicationConfiguration m_configuration;
        private Session m_session;
        private bool m_connectedOnce;
        private Tester m_tester;
        #endregion

        #region Private Methods
        #endregion

        #region Event Handlers
        /// <summary>
        /// Connects to a server.
        /// </summary>
        private void Server_ConnectMI_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectServerCTRL.Connect();
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException(this.Text, exception);
            }
        }

        /// <summary>
        /// Disconnects from the current session.
        /// </summary>
        private void Server_DisconnectMI_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectServerCTRL.Disconnect();
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException(this.Text, exception);
            }
        }

        /// <summary>
        /// Prompts the user to choose a server on another host.
        /// </summary>
        private void Server_DiscoverMI_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectServerCTRL.Discover(null);
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException(this.Text, exception);
            }
        }

        /// <summary>
        /// Updates the application after connecting to or disconnecting from the server.
        /// </summary>
        private void Server_ConnectComplete(object sender, EventArgs e)
        {
            try
            {
                m_session = ConnectServerCTRL.Session;

                if (m_session == null)
                {
                    if (m_tester != null)
                    {
                        StopTest();
                    }

                    return;
                }

                // set a suitable initial state.
                if (m_session != null && !m_connectedOnce)
                {
                    m_connectedOnce = true;
                }

                LogTB.Clear();

                m_tester = new Tester();
                m_tester.SamplingRate = (int)UpdateRateCTRL.Value;
                m_tester.ItemCount = (int)ItemCountCTRL.Value;
                m_tester.Start(m_session);

                UpdateTimer.Enabled = true;
                StopBTN.Visible = true;
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException(this.Text, exception);
            }
        }

        /// <summary>
        /// Updates the application after a communicate error was detected.
        /// </summary>
        private void Server_ReconnectStarting(object sender, EventArgs e)
        {
            try
            {
                // TBD
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException(this.Text, exception);
            }
        }

        /// <summary>
        /// Updates the application after reconnecting to the server.
        /// </summary>
        private void Server_ReconnectComplete(object sender, EventArgs e)
        {
            try
            {
                m_session = ConnectServerCTRL.Session;
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException(this.Text, exception);
            }
        }

        /// <summary>
        /// Cleans up when the main form closes.
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConnectServerCTRL.Disconnect();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Stops the test.
        /// </summary>
        private void StopTest()
        {
            if (m_tester != null)
            {
                m_tester.Stop();
            }

            UpdateTimer.Enabled = false;
            StopBTN.Visible = false;
        }
        #endregion

        #region Event Handlers
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            try
            {
		        int messageCount = 0;
		        int totalItemUpdateCount = 0;
                DateTime firstMessageTime = DateTime.MinValue;
                DateTime lastMessageTime = DateTime.MinValue;
                int minItemUpdateCount = 0;
                int maxItemUpdateCount = 0;

                m_tester.GetStatistics(
                    out messageCount,
                    out totalItemUpdateCount,
                    out firstMessageTime,
                    out lastMessageTime,
                    out minItemUpdateCount,
                    out maxItemUpdateCount);

                string[] messages = m_tester.GetMessages();

                for (int ii = 0; ii < messages.Length; ii++)
                {
                    LogTB.AppendText(messages[ii]);
                    LogTB.AppendText(Environment.NewLine);
                }

		        MessageCountTB.Text = String.Format("{0}", messageCount);
		        TotalItemUpdateCountTB.Text = String.Format("{0}", totalItemUpdateCount);
			    TimeSpan delta = (lastMessageTime - firstMessageTime);

		        if (delta.TotalMilliseconds > 0)
                {
                    LogTB.AppendText(Utils.Format("Checking Update Counts. Time={0}, Min={1}, Max={2}", DateTime.UtcNow.ToString("mm:ss.fff"), minItemUpdateCount, maxItemUpdateCount));
                    LogTB.AppendText(Environment.NewLine);

                    MessageRateTB.Text = String.Format("{0}", delta.TotalSeconds); 
			        TotalItemUpdateRateTB.Text = String.Format("{0}", ((double)totalItemUpdateCount)/delta.TotalSeconds); 
		        }
		        else
		        {	
			        MessageRateTB.Text = String.Empty;
                    TotalItemUpdateRateTB.Text = String.Empty;
		        }
            }
            catch (Exception)
            {
                // TBD
            }
        }

        private void StopBTN_Click(object sender, EventArgs e)
        {
            try
            {
                StopTest();
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException(this.Text, exception);
            }
        }
        #endregion
    }
}
