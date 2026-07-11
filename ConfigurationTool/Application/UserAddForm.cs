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
    /// Create a new user.
    /// </summary>
    public partial class UserAddForm : Form
    {
        #region Constructors
        /// <summary>
        /// Creates an empty form.
        /// </summary>
        public UserAddForm()
        {
            InitializeComponent();
            userAddControl1.Owner = this;
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Returns the user name.
        /// </summary>
        public string UserName
        {
            get { return ((userAddControl1 == null) ? null : userAddControl1.UserName); }
        }

        /// <summary>
        /// Returns the password.
        /// </summary>
        public string Password
        {
            get { return ((userAddControl1 == null) ? null : userAddControl1.Password); }
        }

        #endregion

    }
}
