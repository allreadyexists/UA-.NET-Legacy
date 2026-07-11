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
using System.IO;

namespace Opc.Ua.Client.Controls
{
    /// <summary>
    /// A control with button that displays edit array dialog.
    /// </summary>
    public partial class EditValue2Ctrl : UserControl
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of the control.
        /// </summary>
        public EditValue2Ctrl()
        {
            InitializeComponent();
        }
        #endregion
        
        #region Private Fields
        private event EventHandler m_ValueChanged;
        private Variant m_value;
        #endregion
        
        #region Public Interface
        /// <summary>
        /// The value in the control.
        /// </summary>
        public Variant Value 
        {
            get 
            {
                return m_value; 
            }

            set
            {
                if (CurrentValueControl != null)
                {
                    CurrentValueControl.Text = value.ToString();
                }

                m_value = value;
            }
        }
        
        /// <summary>
        /// Gets or sets the control that shows the current value.
        /// </summary>
        public Control CurrentValueControl { get; set; }

        /// <summary>
        /// Raised when the value is changed.
        /// </summary>
        public event EventHandler ValueChanged
        {
            add { m_ValueChanged += value; }
            remove { m_ValueChanged -= value; }
        }
        #endregion

        #region Event Handlers
        private void BrowseBTN_Click(object sender, EventArgs e)
        {
            if (CurrentValueControl == null)
            {
                return;
            }

            object value = new EditComplexValueDlg().ShowDialog(
                m_value.TypeInfo,
                null,
                m_value.Value,
                "Edit Value");
            
            if (value == null)
            {
                return;
            }

            Value = new Variant(value);

            if (m_ValueChanged != null)
            {
                m_ValueChanged(this, e);
            }
        }
        #endregion
    }
}
