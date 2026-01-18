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

namespace Opc.Ua.Client.Controls
{
    /// <summary>
    /// Prompts the user to enter a user name/password.
    /// </summary>
    public partial class UserNamePasswordDlg : Form
    {
        /// <summary>
        /// Contructs the object.
        /// </summary>
        public UserNamePasswordDlg()
        {
            InitializeComponent();
            this.Icon = ClientUtils.GetAppIcon();
        }

        /// <summary>
        /// Displays the dialog.
        /// </summary>
        public UserIdentity ShowDialog(IUserIdentity identity, string caption)
        {
            if (!String.IsNullOrEmpty(caption))
            {
                this.Text = caption;
            }

            if (identity != null)
            {
                UserNameIdentityToken token = identity.GetIdentityToken() as UserNameIdentityToken;

                if (token != null)
                {
                    UserNameTB.Text = token.UserName;
                    PasswordTB.Text = token.DecryptedPassword;
                }
            }

            if (ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            return new UserIdentity(UserNameTB.Text, PasswordTB.Text);
        }
    }
}
