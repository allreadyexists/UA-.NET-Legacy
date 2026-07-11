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
using Opc.Ua.Client.Controls;

namespace Opc.Ua.NetworkTester
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            
            ListenerUrlTB.Text = "opc.tcp://localhost:7001";
            ServerUrlTB.Text = "opc.tcp://localhost:61210";

            StopBTN.Enabled = false;
        }

        private Server m_server;

        private void StartBTN_Click(object sender, EventArgs e)
        {   
            try
            {
                if (m_server != null)
                {
                    m_server.Stop();
                    m_server = null;
                }

                m_server = new Server(ListenerUrlTB.Text, ServerUrlTB.Text);
                m_server.Start();

                StopBTN.Enabled = true;
            }
            catch (Exception exception)
            {
				GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void StopBTN_Click(object sender, EventArgs e)
        {   
            try
            {
                if (m_server != null)
                {
                    m_server.Stop();
                    m_server = null;
                }

                StopBTN.Enabled = false;
            }
            catch (Exception exception)
            {
				GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }
    }
}
