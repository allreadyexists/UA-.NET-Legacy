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
using Opc.Ua.Configuration;

namespace Quickstarts.ReferenceServer
{
    public partial class Form2 : Quickstarts.ServerForm
    {
        public Form2(ApplicationInstance application) : base(application)
        {
            InitializeComponent();
            new MonitoredItemEventLogDlg().Display();
        }

        // launch the Website URL (should invoke the default browser)
        private void linkLabel1_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
        {
            System.Diagnostics.Process.Start( linkLabel1.Text );
        }

        private void Form2_Load( object sender, EventArgs e )
        {
            panel1.SendToBack();
        }
    }
}
