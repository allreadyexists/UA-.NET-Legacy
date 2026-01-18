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
using Opc.Ua;
using Opc.Ua.Client;

namespace Quickstarts.HistoricalEvents.Client
{
    /// <summary>
    /// Prompts the user to specify a new value and then writes it to the server.
    /// </summary>
    public partial class SetValueDlg : Form
    {
        #region Constructors
        /// <summary>
        /// Creates an empty form.
        /// </summary>
        public SetValueDlg()
        {
            InitializeComponent();
        }
        #endregion
        
        #region Private Fields
        #endregion
        
        #region Public Interface
        public Variant? ShowDialog(Variant value, BuiltInType builtInType)
        {
            if (value != Variant.Null)
            {
                ValueTB.Text = value.ToString();
            }
            
            if (ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            if (String.IsNullOrEmpty(ValueTB.Text))
            {
                return Variant.Null;
            }

            return new Variant(TypeInfo.Cast(ValueTB.Text, builtInType));
        }
        #endregion
                
        #region Event Handlers
        private void OkBTN_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
        #endregion
    }
}
