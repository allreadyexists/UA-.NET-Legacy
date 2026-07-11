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
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

using Opc.Ua.Client.Controls;

namespace Opc.Ua.Configuration
{
    /// <summary>
    /// Prompts the user to edit a ApplicationDescription.
    /// </summary>
    public partial class PasswordDlg : Form
    {
        public PasswordDlg()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Displays the dialog.
        /// </summary>
        public string ShowDialog(string password, string message)
        {
            this.PasswordTB.Text = password;
            this.MessageTB.Text = message;
            
            if (ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            return PasswordTB.Text;
        }
    }
}
