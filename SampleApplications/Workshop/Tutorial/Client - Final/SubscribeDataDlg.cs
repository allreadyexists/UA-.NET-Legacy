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
using System.Windows.Forms;
using System.Text;
using System.Data;
using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Client.Controls;

namespace TutorialClient
{
    /// <summary>
    /// Prompts the user to select an area to use as an event filter.
    /// </summary>
    public partial class SubscribeDataDlg : Form
    {
        #region Constructors
        /// <summary>
        /// Creates an empty form.
        /// </summary>
        public SubscribeDataDlg()
        {
            InitializeComponent();
            CreateDataSet();
        }
        #endregion
        
        #region Private Fields
        private Session m_session;
        private NodeId m_nodeId;
        private Subscription m_subscription;
        private DataSet m_dataset;
        private int m_nextId;
        #endregion
        
        #region Public Interface
        /// <summary>
        /// Displays the dialog.
        /// </summary>
        public void Show(Session session, NodeId nodeId)
        {
            m_session = session;
            m_nodeId = nodeId;

            #region Task #B3 - Subscribe Data
            CreateSubscription();
            #endregion

            Show();
        }

        /// <summary>
        /// Updates the dialog after a reconnect.
        /// </summary>
        public void ReconnectComplete(Session session)
        {
            m_session = session;

            // find the matching subscription.
            foreach (Subscription subscription in m_session.Subscriptions)
            {
                if (Object.ReferenceEquals(subscription.Handle, this))
                {
                    m_subscription = subscription;
                    break;
                }
            }
        }
        #endregion
        
        #region Private Methods
        /// <summary>
        /// Creates the dataset and initializes the view.
        /// </summary>
        private void CreateDataSet()
        {
            DataSet dataset = new DataSet();
            dataset.Tables.Add("Results");

            DataColumn key = dataset.Tables[0].Columns.Add("Index", typeof(int));
            dataset.Tables[0].PrimaryKey = new DataColumn[] { key };

            dataset.Tables[0].Columns.Add("Timestamp", typeof(string));
            dataset.Tables[0].Columns.Add("Value", typeof(string));
            dataset.Tables[0].Columns.Add("StatusCode", typeof(string));
            dataset.Tables[0].Columns.Add("DataType", typeof(string));

            ResultsDV.Columns.Clear();
            ResultsDV.AutoGenerateColumns = false;

            for (int ii = 1; ii < dataset.Tables[0].Columns.Count; ii++)
            {
                string columnName = dataset.Tables[0].Columns[ii].ColumnName;
                ResultsDV.Columns.Add(columnName, columnName);
                ResultsDV.Columns[ResultsDV.Columns.Count - 1].DataPropertyName = columnName;
            }

            dataset.Tables[0].DefaultView.Sort = "Index";

            m_dataset = dataset;
            ResultsDV.DataSource = dataset.Tables[0];
        }

        /// <summary>
        /// Adds a value to the grid.
        /// </summary>
        private void AddValue(DataValue value, ModificationInfo modificationInfo)
        {
            DataRow row = m_dataset.Tables[0].NewRow();

            row[0] = m_nextId++;
            row[1] = value.SourceTimestamp.ToLocalTime().ToString("HH:mm:ss.fff");
            row[2] = value.WrappedValue;
            row[3] = new StatusCode(value.StatusCode.Code);

            if (value.WrappedValue.TypeInfo != null)
            {
                row[4] = value.WrappedValue.TypeInfo.BuiltInType.ToString();
            }
            else
            {
                row[4] = String.Empty;
            }

            m_dataset.Tables[0].Rows.Add(row);
        }

        #endregion

        #region Task #B3 - Subscribe Data
        /// <summary>
        /// Creates the subscription.
        /// </summary>
        private void CreateSubscription()
        {
            if (m_session == null)
            {
                return;
            }

            m_subscription = new Subscription();
            m_subscription.Handle = this;
            m_subscription.DisplayName = null;
            m_subscription.PublishingInterval = 500;
            m_subscription.KeepAliveCount = 10;
            m_subscription.LifetimeCount = 100;
            m_subscription.MaxNotificationsPerPublish = 1000;
            m_subscription.PublishingEnabled = true;
            m_subscription.TimestampsToReturn = TimestampsToReturn.Both;

            m_session.AddSubscription(m_subscription);
            m_subscription.Create();

            MonitoredItem monitoredItem = new MonitoredItem();
            monitoredItem.StartNodeId = m_nodeId;
            monitoredItem.AttributeId = Attributes.Value;
            monitoredItem.SamplingInterval = 1000;
            monitoredItem.QueueSize = 1000;
            monitoredItem.DiscardOldest = true;

            monitoredItem.Notification += new MonitoredItemNotificationEventHandler(MonitoredItem_Notification);

            m_subscription.AddItem(monitoredItem);
            m_subscription.ApplyChanges();

            // verify that the item was created successfully.
            if (ServiceResult.IsBad(monitoredItem.Status.Error))
            {
                throw new ServiceResultException(monitoredItem.Status.Error);
            }
        }

        /// <summary>
        /// Deletes the subscription.
        /// </summary>
        private void DeleteSubscription()
        {
            if (m_subscription != null)
            {
                m_subscription.Delete(true);
                m_session.RemoveSubscription(m_subscription);
                m_subscription = null;
            }
        }

        /// <summary>
        /// Updates the display with a new value for a monitored variable. 
        /// </summary>
        private void MonitoredItem_Notification(MonitoredItem monitoredItem, MonitoredItemNotificationEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MonitoredItemNotificationEventHandler(MonitoredItem_Notification), monitoredItem, e);
                return;
            }

            try
            {
                if (!Object.ReferenceEquals(monitoredItem.Subscription, m_subscription))
                {
                    return;
                }

                MonitoredItemNotification notification = e.NotificationValue as MonitoredItemNotification;

                if (notification == null)
                {
                    return;
                }

                AddValue(notification.Value, null);
                m_dataset.AcceptChanges();
                ResultsDV.FirstDisplayedCell = ResultsDV.Rows[ResultsDV.Rows.Count - 1].Cells[0];
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException(this.Text, exception);
            }
        }

        private void SubscribeDlg_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (m_session != null)
                {
                    DeleteSubscription();
                }
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException(this.Text, exception);
            }
        }
        #endregion

        #region Event Handlers
        #endregion
    }
}
