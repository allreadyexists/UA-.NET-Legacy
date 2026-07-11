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
    /// Create a new user.
    /// </summary>
    public partial class UserAddControl : UserControl
    {
        #region Constructors
        /// <summary>
        /// Creates an empty control.
        /// </summary>
        public UserAddControl()
            : this(null)
        { 
        }

        /// <summary>
        /// Creates a control with owner as input.
        /// </summary>
        public UserAddControl(Form owner)
        {
            InitializeComponent();

            Owner = owner;
            if (Owner != null)
            {
                Owner.DialogResult = DialogResult.Cancel;
            }
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Returns the owner Form.
        /// </summary>
        public Form Owner { get; set; }

        /// <summary>
        /// Returns the user name.
        /// </summary>
        public string UserName
        {
            get { return ((userNameControl1 == null) ? null : userNameControl1.UserName); }
        }

        /// <summary>
        /// Returns the password.
        /// </summary>
        public string Password
        {
            get { return ((userPasswordControl1 == null) ? null : userPasswordControl1.Password); }
        }

        #endregion

        #region Event Handlers
        private void CreateButton_Click(object sender, EventArgs e)
        {
            // check date.
            if (string.IsNullOrEmpty(UserName))
            {   
                MessageBox.Show(this
                                , "A user name is not input. Please input a user name again."
                                , "Exclamation"
                                , MessageBoxButtons.OK
                                , MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrEmpty(Password))
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

            // closer a From.
            if (Owner != null)
            {
                Owner.DialogResult = DialogResult.OK;
                Owner.Close();
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            if (Owner == null)
            {
                return;
            }

            Owner.DialogResult = DialogResult.Cancel;
            Owner.Close();

            return;
        }
        #endregion
    }
}
