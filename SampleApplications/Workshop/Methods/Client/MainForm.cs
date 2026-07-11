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

namespace Quickstarts.MethodsClient
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
            ConnectServerCTRL.ServerUrl = "opc.tcp://localhost:62541/Quickstarts/MethodsServer";
            this.Text = m_configuration.ApplicationName;
        }
        #endregion

        #region Private Fields
        private ApplicationConfiguration m_configuration;
        private Session m_session;
        private Subscription m_subscription;
        private NodeId m_objectNode;
        private NodeId m_methodNode;
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

                if (m_session == null)
                {
                    StartBTN.Enabled = false;
                    return;
                }

                // set a suitable initial state.
                if (m_session != null && !m_connectedOnce)
                {
                    m_connectedOnce = true;
                }

                // this client has built-in knowledge of the information model used by the server.
                NamespaceTable wellKnownNamespaceUris = new NamespaceTable();
                wellKnownNamespaceUris.Append(Namespaces.Methods);

                string[] browsePaths = new string[] 
                {
                    "1:My Process/1:State",
                    "1:My Process",
                    "1:My Process/1:Start"
                };

                List<NodeId> nodes = ClientUtils.TranslateBrowsePaths(
                    m_session,
                    ObjectIds.ObjectsFolder,
                    wellKnownNamespaceUris,
                    browsePaths);

                // subscribe to the state if available.
                if (nodes.Count > 0 && !NodeId.IsNull(nodes[0]))
                {
                    m_subscription = new Subscription();

                    m_subscription.PublishingEnabled = true;
                    m_subscription.PublishingInterval = 1000;
                    m_subscription.Priority = 1;
                    m_subscription.KeepAliveCount = 10;
                    m_subscription.LifetimeCount = 20;
                    m_subscription.MaxNotificationsPerPublish = 1000;

                    m_session.AddSubscription(m_subscription);
                    m_subscription.Create();

                    MonitoredItem monitoredItem = new MonitoredItem();
                    monitoredItem.StartNodeId = nodes[0];
                    monitoredItem.AttributeId = Attributes.Value;
                    monitoredItem.Notification += new MonitoredItemNotificationEventHandler(MonitoredItem_Notification);
                    m_subscription.AddItem(monitoredItem);

                    m_subscription.ApplyChanges();
                }

                // save the object/method
                if (nodes.Count > 2)
                {
                    m_objectNode = nodes[1];
                    m_methodNode = nodes[2];
                }

                InitialStateTB.Text = "1";
                FinalStateTB.Text = "100";
                StartBTN.Enabled = true;
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
                StartBTN.Enabled = false;
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

                foreach (Subscription subscription in m_session.Subscriptions)
                {
                    m_subscription = subscription;
                    break;
                }

                StartBTN.Enabled = true;
                StartBTN_Click(this, null);
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

        #region Event Handlers
        private void StartBTN_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_session == null)
                {
                    return;
                }

                uint initialState = Convert.ToUInt32(InitialStateTB.Text);
                uint finalState = Convert.ToUInt32(FinalStateTB.Text);

                RevisedInitialStateTB.Text = String.Empty;
                RevisedFinalStateTB.Text = String.Empty;

                IList<object> outputArguments = m_session.Call(
                    m_objectNode,
                    m_methodNode,
                    initialState,
                    finalState);

                if (outputArguments != null && outputArguments.Count > 1)
                {
                    RevisedInitialStateTB.Text = String.Format("{0}", outputArguments[0]);
                    RevisedFinalStateTB.Text = String.Format("{0}", outputArguments[1]);
                }
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException(this.Text, exception);
            }
        }

        void MonitoredItem_Notification(MonitoredItem monitoredItem, MonitoredItemNotificationEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MonitoredItemNotificationEventHandler(MonitoredItem_Notification), monitoredItem, e);
                return;
            }

            try
            {
                MonitoredItemNotification datachange = e.NotificationValue as MonitoredItemNotification;

                if (datachange == null)
                {
                    return;
                }

                CurrentStateTB.Text = datachange.Value.WrappedValue.ToString();
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException(this.Text, exception);
            }
        }
        #endregion
    }
}
