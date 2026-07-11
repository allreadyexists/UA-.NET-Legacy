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
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Opc.Ua.Sample
{
    public partial class ServerForm : Form
    {
        public ServerForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Logs the message to the display.
        /// </summary>
        public void LogMessage(object message)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new System.Threading.WaitCallback(LogMessage), message);
                return;
            }

            MessagesTB.AppendText(String.Format("{0:HH:mm:ss} {1}", DateTime.Now, message));
        }
    }
}
