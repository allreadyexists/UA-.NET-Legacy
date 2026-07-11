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
    /// Manage a user name.
    /// </summary>
    public partial class UserNameControl : UserControl
    {
        #region Constructors
        /// <summary>
        /// Creates an empty control.
        /// </summary>
        public UserNameControl()
        {
            InitializeComponent();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Returns the user name.
        /// </summary>
        public string UserName
        {
            get { return ((UserNameTextBox == null) ? null : UserNameTextBox.Text); }
            set
            {
                if (UserNameTextBox == null)
                {
                    return;
                }
                UserNameTextBox.Text = value;
            }
        }
        #endregion
    }
}
