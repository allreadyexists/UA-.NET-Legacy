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

namespace Opc.Ua.Sample.Controls
{
    public partial class SetMonitoringModeDlg : Form
    {
        public SetMonitoringModeDlg()
        {
            InitializeComponent();

            foreach (MonitoringMode value in Enum.GetValues(typeof(MonitoringMode)))
            {
                MonitoringModeCB.Items.Add(value);
            }
        }

        /// <summary>
        /// Displays the dialog.
        /// </summary>
        public bool ShowDialog(ref MonitoringMode monitoringMode)
        {
            MonitoringModeCB.SelectedItem = monitoringMode;

            if (ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            monitoringMode = (MonitoringMode)MonitoringModeCB.SelectedItem;

            return true;
        }
    }
}
