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
using System.Windows.Forms;
using System.Text;
using Opc.Ua;
using Opc.Ua.Client;

namespace Opc.Ua.Client.Controls
{
    /// <summary>
    /// Prompts the user to edit a value.
    /// </summary>
    public partial class EditDataValueDlg : Form
    {
        #region Constructors
        /// <summary>
        /// Creates an empty form.
        /// </summary>
        public EditDataValueDlg()
        {
            InitializeComponent();
            this.Icon = ClientUtils.GetAppIcon();
        }
        #endregion
        
        #region Private Fields
        private DataValue m_value;
        #endregion

        #region Public Interface
        /// <summary>
        /// Prompts the user to edit a value.
        /// </summary>
        public Variant ShowDialog(Variant value, string caption)
        {
            if (caption != null)
            {
                this.Text = caption;
            }

            ValueCTRL.ShowStatusTimestamp = false;
            ValueCTRL.Value = value;

            if (ShowDialog() != DialogResult.OK)
            {
                return Variant.Null;
            }

            if (m_value != null)
            {
                return m_value.WrappedValue;
            }

            return Variant.Null;
        }

        /// <summary>
        /// Prompts the user to edit a data value.
        /// </summary>
        public DataValue ShowDialog(DataValue value, TypeInfo expectedType, string caption)
        {
            if (caption != null)
            {
                this.Text = caption;
            }

            ValueCTRL.SetDataValue(value, expectedType);
            ValueCTRL.ShowStatusTimestamp = true;

            if (ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            return m_value;
        }
        #endregion
        
        #region Event Handlers
        private void OkBTN_Click(object sender, EventArgs e)
        {
            try
            {
                m_value = ValueCTRL.GetDataValue();
                DialogResult = DialogResult.OK;
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException(this.Text, exception);
            }
        }
        #endregion
    }
}
