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

namespace Opc.Ua.Client.Controls
{
    /// <summary>
    /// A dialog to edit a string value.
    /// </summary>
    public partial class StringValueEditDlg : Form
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="StringValueEditDlg"/> class.
        /// </summary>
        public StringValueEditDlg()
        {
            InitializeComponent();
        }
        #endregion
        
        #region Public Interface
        /// <summary>
        /// Displays the dialog.
        /// </summary>
        public string ShowDialog(string value)
        {
            ValueTB.Text = value;

            if (value != null)
            {
                int length = value.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None).Length;

                if (length > 20)
                {
                    length = 20;
                }

                this.Height += (length-1)*16;
            }

            if (ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            return ValueTB.Text;
        }
        #endregion
    }
}
