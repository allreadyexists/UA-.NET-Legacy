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

namespace Opc.Ua.Configuration
{
    /// <summary>
    /// Change the password for form.
    /// </summary>
    public partial class UserPasswordChangeForm : Form
    {
        #region Constructors
        /// <summary>
        /// Creates an empty form.
        /// </summary>
        public UserPasswordChangeForm()
        {
            InitializeComponent();

            DialogResult = DialogResult.Cancel;
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Returns the user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Returns the old password.
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// Returns the new password.
        /// </summary>
        public string NewPassword
        {
            get { return ((userPasswordControl1 == null) ? null : userPasswordControl1.Password); }
        }

        #endregion

        #region Event Handlers
        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
            return;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            //  check data.
            if (string.IsNullOrEmpty(NewPassword))
            {   
                MessageBox.Show(this
                                , "A password is not input. Please input a password again."
                                , "Exclamation"
                                , MessageBoxButtons.OK
                                , MessageBoxIcon.Exclamation);

                userPasswordControl1.PasswordConfirm = string.Empty;
                return;
            }

            if (!userPasswordControl1.PasswordAgrees)
            {   
                MessageBox.Show(this
                                , "A password does not accord. Please input a password for confirmation again."
                                , "Exclamation"
                                , MessageBoxButtons.OK
                                , MessageBoxIcon.Exclamation);

                userPasswordControl1.PasswordConfirm = string.Empty;
                return;
            }

            DialogResult = DialogResult.OK;
            return;
        }

        private void UserPasswordChangeForm_Shown(object sender, EventArgs e)
        {
            Text += "(" + UserName + ")";
        }
        #endregion

    }
}
