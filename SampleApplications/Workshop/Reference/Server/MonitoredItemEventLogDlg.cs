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
using Opc.Ua.Server;

namespace Quickstarts.ReferenceServer
{
    public partial class MonitoredItemEventLogDlg : Form
    {
        public MonitoredItemEventLogDlg()
        {
            InitializeComponent();
            DataGridCTRL.AutoGenerateColumns = false;
        }
        
        public void Display()
        {
            ServerUtils.EventsEnabled = true;
            RefreshTimer.Enabled = true;
            Show();
        }

        DataSet m_dataset;

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            int count = 0;

            if (m_dataset != null)
            {
                count = m_dataset.Tables[0].Rows.Count;
            }

            m_dataset = ServerUtils.EmptyQueue(m_dataset);
            
            if (count != m_dataset.Tables[0].Rows.Count)
            {
                DataGridCTRL.DataSource = m_dataset.Tables[0];
            }
        }

        private void Events_ClearMI_Click(object sender, EventArgs e)
        {
            if (m_dataset != null)
            {
                m_dataset.Tables[0].Rows.Clear();
                m_dataset.AcceptChanges();
                DataGridCTRL.DataSource = m_dataset.Tables[0];
            }
        }
    }
}
