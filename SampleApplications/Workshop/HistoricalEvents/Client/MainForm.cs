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

namespace Quickstarts.HistoricalEvents.Client
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
            this.Icon = ClientUtils.GetAppIcon();
        }
        
        /// <summary>
        /// Creates a form which uses the specified client configuration.
        /// </summary>
        /// <param name="configuration">The configuration to use.</param>
        public MainForm(ApplicationConfiguration configuration)
        {
            InitializeComponent();
            this.Icon = ClientUtils.GetAppIcon();

            ConnectServerCTRL.Configuration = m_configuration = configuration;
            ConnectServerCTRL.ServerUrl = "opc.tcp://localhost:62553/Quickstarts/HistoricalEventsServer";
            this.Text = m_configuration.ApplicationName;
        }
        #endregion
        
        #region Private Fields
        private ApplicationConfiguration m_configuration;
        private Session m_session;
        private bool m_connectedOnce;
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

                // set a suitable initial state.
                if (m_session != null && !m_connectedOnce)
                {
                    EventsLV.IsSubscribed = false;
                    EventsLV.ChangeArea(ExpandedNodeId.ToNodeId(ObjectIds.Plaforms, m_session.NamespaceUris), true);

                    TypeDeclaration type = new TypeDeclaration();
                    type.NodeId = ExpandedNodeId.ToNodeId(ObjectTypeIds.WellTestReportType, m_session.NamespaceUris);
                    type.Declarations = ModelUtils.CollectInstanceDeclarationsForType(m_session, type.NodeId);

                    EventsLV.ChangeFilter(new FilterDeclaration(type, null), true);
                    m_connectedOnce = true;
                }

                EventsLV.IsSubscribed = Events_EnableSubscriptionMI.Checked;
                EventsLV.ChangeSession(m_session, true);
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
                EventsLV.SessionReconnected(m_session);
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

        private void Events_SelectEventTypeMI_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_session == null)
                {
                    return;
                }

                TypeDeclaration type = new SelectTypeDlg().ShowDialog(m_session, Opc.Ua.ObjectTypeIds.BaseEventType, "Select Event Type");

                if (type == null)
                {
                    return;
                }

                EventsLV.ChangeFilter(new FilterDeclaration(type, EventsLV.Filter), true);
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException(this.Text, exception);
            }
        }

        private void Events_ModifyEventFilterMI_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_session == null)
                {
                    return;
                }

                if (!new ModifyFilterDlg().ShowDialog(EventsLV.Filter))
                {
                    return;
                }

                EventsLV.ChangeFilter(EventsLV.Filter, true);
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException(this.Text, exception);
            }
        }

        private void Events_SelectEventAreaMI_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_session == null)
                {
                    return;
                }

                NodeId areaId = new SelectNodeDlg().ShowDialog(m_session, Opc.Ua.ObjectIds.Server, "Select Event Area", Opc.Ua.ReferenceTypeIds.HasEventSource);

                if (areaId == null)
                {
                    return;
                }

                EventsLV.ChangeArea(areaId, true);
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException(this.Text, exception);
            }
        }

        private void Events_EnableSubscriptionMI_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                EventsLV.IsSubscribed = Events_EnableSubscriptionMI.Checked;
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException(this.Text, exception);
            }
        }

        private void Events_EditEventHistoryMI_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_session == null)
                {
                    return;
                }

                new ReadEventHistoryDlg().ShowDialog(m_session, EventsLV.AreaId, new FilterDeclaration(EventsLV.Filter));
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException(this.Text, exception);
            }
        }

        /// <summary>
        /// Sets the locale to use.
        /// </summary>
        private void Server_SetLocaleMI_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_session == null)
                {
                    return;
                }

                string locale = new SelectLocaleDlg().ShowDialog(m_session);

                if (locale == null)
                {
                    return;
                }

                ConnectServerCTRL.PreferredLocales = new string[] { locale };
                m_session.ChangePreferredLocales(new StringCollection(ConnectServerCTRL.PreferredLocales));
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException(this.Text, exception);
            }
        }
        #endregion

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit the application?", "UA Sample Client", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void Help_ContentsMI_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start( Path.GetDirectoryName(Application.ExecutablePath) + "\\WebHelp\\haeventsclientoverview.htm");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to launch help documentation. Error: " + ex.Message);
            }
        }
    }
}
