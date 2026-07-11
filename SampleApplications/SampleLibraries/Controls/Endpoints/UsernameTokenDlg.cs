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
using System.Threading;
using System.Security.Cryptography.X509Certificates;

using Opc.Ua.Client;

namespace Opc.Ua.Client.Controls
{
    /// <summary>
    /// Prompts the user to provide a user name and password.
    /// </summary>
    public partial class UsernameTokenDlg : Form
    {
        #region Constructors
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public UsernameTokenDlg()
        {
            InitializeComponent();
            this.Icon = ClientUtils.GetAppIcon();
        }
        #endregion

        #region Private Fields
        #endregion
        
        #region Public Interface
        /// <summary>
        /// Displays the dialog.
        /// </summary>
        public bool ShowDialog(UserNameIdentityToken token)
        {
            if (token != null)
            {
                UserNameCB.Text = token.UserName;

                if (token.Password != null && token.Password.Length > 0)
                {
                    PasswordTB.Text = new UTF8Encoding().GetString(token.Password);
                }
            }

            if (ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            token.UserName = UserNameCB.Text;

            if (!String.IsNullOrEmpty(PasswordTB.Text))
            {
                token.Password = new UTF8Encoding().GetBytes(PasswordTB.Text);
            }
            else
            {
                token.Password = null;
            }

            return true;
        }
        #endregion
    }
}
