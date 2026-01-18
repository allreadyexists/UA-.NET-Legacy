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
    public partial class DataChangeFilterEditDlg : Form
    {
        public DataChangeFilterEditDlg()
        {
            InitializeComponent();
            this.Icon = ClientUtils.GetAppIcon();
            
            Array values = Enum.GetValues(typeof(DataChangeTrigger));

            foreach (object value in values)
            {
                TriggerCB.Items.Add(value);
            }
                        
            values = Enum.GetValues(typeof(DeadbandType));

            foreach (object value in values)
            {
                DeadbandTypeCB.Items.Add(value);
            }
        }

        /// <summary>
        /// Prompts the user to specify the browse options.
        /// </summary>
        public bool ShowDialog(Session session, MonitoredItem monitoredItem)
        {
            if (monitoredItem == null) throw new ArgumentNullException("monitoredItem");

            DataChangeFilter filter = monitoredItem.Filter as DataChangeFilter;

            if (filter == null)
            {
                filter = new DataChangeFilter();

                filter.Trigger       = DataChangeTrigger.StatusValue;
                filter.DeadbandValue = 0;
                filter.DeadbandType  = (uint)(int)DeadbandType.None;
            }

            TriggerCB.SelectedItem      = filter.Trigger;
            DeadbandTypeCB.SelectedItem = (DeadbandType)(int)filter.DeadbandType;
            DeadbandNC.Value            = (decimal)filter.DeadbandValue;
            
            if (ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            filter.Trigger       = (DataChangeTrigger)TriggerCB.SelectedItem;
            filter.DeadbandType  = Convert.ToUInt32(DeadbandTypeCB.SelectedItem);
            filter.DeadbandValue = (double)DeadbandNC.Value;

            monitoredItem.Filter = filter;

            return true;
        }

        private void OkBTN_Click(object sender, EventArgs e)
        {              
            DialogResult = DialogResult.OK;
        }

        private void DeadbandTypeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeadbandType deadbandType = (DeadbandType)DeadbandTypeCB.SelectedItem;

            DeadbandNC.Enabled = deadbandType != DeadbandType.None;

            if (deadbandType == DeadbandType.Percent)
            {
                DeadbandNC.Minimum = 0;
                DeadbandNC.Maximum = 100;
            }
            else
            {
                DeadbandNC.Minimum = Decimal.MinValue;
                DeadbandNC.Maximum = Decimal.MaxValue;
            }
        }
    }
}
