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
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Opc.Ua.Configuration
{
    /// <summary>
    /// Manage a password.
    /// </summary>
    public partial class UserPasswordControl : UserControl
    {
        #region Constructors
        /// <summary>
        /// Creates an empty control.
        /// </summary>
        public UserPasswordControl()
        {
            InitializeComponent();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Returns the password.
        /// </summary>
        public string Password
        {
            get { return ((PasswordTextBox == null) ? string.Empty : PasswordTextBox.Text); }
            set
            {
                if (PasswordTextBox == null)
                {
                    return;
                }
                PasswordTextBox.Text = value;
            }
        }

        /// <summary>
        /// Returns the confirm password
        /// </summary>
        public string PasswordConfirm
        {
            get { return ((PasswordConfirmTextBox == null) ? string.Empty : PasswordConfirmTextBox.Text); }
            set
            {
                if (PasswordConfirmTextBox == null)
                {
                    return;
                }
                PasswordConfirmTextBox.Text = value;
            }
        }

        public bool PasswordAgrees { get; private set; }

        #endregion

        #region Event Handlers
        private void PassworConfirm_TextChanged(object sender, EventArgs e)
        {
            #region パラメータチェック

            if (sender == null)
            {
                return;
            }

            if (PasswordTextBox == null
                || PasswordConfirmTextBox == null)
            {
                return;
            }

            if(string.IsNullOrEmpty(PasswordTextBox.Text))
            {   // check a null.
                PasswordConfirmTextBox.BackColor = System.Drawing.Color.LightPink;
                PasswordAgrees = false;
                return;
            }

            if (string.IsNullOrEmpty(PasswordConfirmTextBox.Text))
            {   // check a null.
                PasswordConfirmTextBox.BackColor = System.Drawing.Color.LightPink;
                PasswordAgrees = false;
                return;
            }

            #endregion パラメータチェック

            if (PasswordTextBox.Text == PasswordConfirmTextBox.Text)
            {   // password matches confirm password.
                PasswordConfirmTextBox.BackColor = System.Drawing.Color.Lime;
                PasswordAgrees = true;
            }
            else
            {   //  password unmatched confirm password.
                PasswordConfirmTextBox.BackColor = System.Drawing.Color.LightPink;
                PasswordAgrees = false;
            }
        }
        #endregion メソッド定義
    }
}
