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

using Opc.Ua.Client;
using Opc.Ua.Client.Controls;

namespace Opc.Ua.Sample.Controls
{
    public partial class MonitoredItemEditDlg : Form
    {
        public MonitoredItemEditDlg()
        {
            InitializeComponent();
            this.Icon = ClientUtils.GetAppIcon();

            AttributeIdCB.Items.AddRange(Attributes.GetBrowseNames());
            
            foreach (MonitoringMode value in Enum.GetValues(typeof(MonitoringMode)))
            {
                MonitoringModeCB.Items.Add(value);
            }

            foreach (NodeClass value in Enum.GetValues(typeof(NodeClass)))
            {
                NodeClassCB.Items.Add(value);
            }
        }

        private Session m_session;

        /// <summary>
        /// Prompts the user to specify the browse options.
        /// </summary>
        public bool ShowDialog(Session session, MonitoredItem monitoredItem)
        {
            return ShowDialog(session, monitoredItem, false);
        }

        /// <summary>
        /// Prompts the user to specify the browse options.
        /// </summary>
        public bool ShowDialog(Session session, MonitoredItem monitoredItem, bool editMonitoredItem)
        {
            if (monitoredItem == null) throw new ArgumentNullException("monitoredItem");

            m_session = session;

            NodeIdCTRL.Browser = new Browser(session);

            if (editMonitoredItem)
            {
                // Disable the not changeable values            
                NodeIdCTRL.Enabled = false;
                RelativePathTB.Enabled = false;
                NodeClassCB.Enabled = false;
                AttributeIdCB.Enabled = false;
                IndexRangeTB.Enabled = false;
                EncodingCB.Enabled = false;
                MonitoringModeCB.Enabled = false;

                DisplayNameTB.Text = monitoredItem.DisplayName;
            }
            else
            {
                uint monitoredItemsCount = 0;

                if (session != null && session.SubscriptionCount >= 1)
                {
                    foreach (Subscription subscription in session.Subscriptions)
                    {
                        monitoredItemsCount += subscription.MonitoredItemCount;
                    }
                }

                DisplayNameTB.Text = String.Format("MonitoredItem {0}", monitoredItemsCount + 1);
            }

            NodeIdCTRL.Identifier = monitoredItem.StartNodeId;
            RelativePathTB.Text = monitoredItem.RelativePath;
            NodeClassCB.SelectedItem = monitoredItem.NodeClass;
            AttributeIdCB.SelectedItem = Attributes.GetBrowseName(monitoredItem.AttributeId);
            IndexRangeTB.Text = monitoredItem.IndexRange;
            EncodingCB.Text = (monitoredItem.Encoding != null) ? monitoredItem.Encoding.Name : null;
            MonitoringModeCB.SelectedItem = monitoredItem.MonitoringMode;
            SamplingIntervalNC.Value = 1000;
            DisableOldestCK.Checked = monitoredItem.DiscardOldest;

            if (monitoredItem.SamplingInterval >= 0)
            {
                SamplingIntervalNC.Value = (decimal)monitoredItem.SamplingInterval;
            }

            QueueSizeNC.Value = monitoredItem.QueueSize;

            if (ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            monitoredItem.DisplayName = DisplayNameTB.Text;
            monitoredItem.NodeClass = (NodeClass)NodeClassCB.SelectedItem;
            monitoredItem.StartNodeId = NodeIdCTRL.Identifier;
            monitoredItem.RelativePath = RelativePathTB.Text;
            monitoredItem.AttributeId = Attributes.GetIdentifier((string)AttributeIdCB.SelectedItem);
            monitoredItem.IndexRange = IndexRangeTB.Text;
            monitoredItem.MonitoringMode = (MonitoringMode)MonitoringModeCB.SelectedItem;
            monitoredItem.SamplingInterval = (int)SamplingIntervalNC.Value;
            monitoredItem.QueueSize = (uint)QueueSizeNC.Value;
            monitoredItem.DiscardOldest = DisableOldestCK.Checked;

            if (!String.IsNullOrEmpty(EncodingCB.Text))
            {
                monitoredItem.Encoding = new QualifiedName(EncodingCB.Text);
            }

            return true;
        }

        private void OkBTN_Click(object sender, EventArgs e)
        {              
            try
            {
                NodeId nodeId = NodeIdCTRL.Identifier;
            }
            catch (Exception)
            {
				MessageBox.Show("Please enter a valid node id.", this.Text);
            }
            
            try
            {
                if (!String.IsNullOrEmpty(RelativePathTB.Text))
                {
                    RelativePath relativePath = RelativePath.Parse(RelativePathTB.Text, m_session.TypeTree);
                }
            }
            catch (Exception)
            {
				MessageBox.Show("Please enter a valid relative path.", this.Text);
            }

            try
            {
                if (!String.IsNullOrEmpty(IndexRangeTB.Text))
                {
                    NumericRange indexRange = NumericRange.Parse(IndexRangeTB.Text);
                }
            }
            catch (Exception)
            {
				MessageBox.Show("Please enter a valid index range.", this.Text);
            }

            DialogResult = DialogResult.OK;
        }

        private void NodeIdCTRL_IdentifierChanged(object sender, EventArgs e)
        {
            if (NodeIdCTRL.Reference != null)
            {
                DisplayNameTB.Text = m_session.NodeCache.GetDisplayText(NodeIdCTRL.Reference);
                NodeClassCB.SelectedItem = (NodeClass)NodeIdCTRL.Reference.NodeClass;
            }
        }
    }
}
