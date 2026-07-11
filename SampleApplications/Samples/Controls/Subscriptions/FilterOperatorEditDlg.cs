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

namespace Opc.Ua.Sample.Controls
{
    public partial class FilterOperatorEditDlg : Form
    {
        #region Constructors
        public FilterOperatorEditDlg()
        {
            InitializeComponent();

            OperatorCB.Items.Clear();

            foreach (FilterOperator op in Enum.GetValues(typeof(FilterOperator)))
            {
                OperatorCB.Items.Add(op);
            }
        }
        #endregion
                
        #region Public Interface
        /// <summary>
        /// Displays the dialog.
        /// </summary>
        public bool ShowDialog(ref FilterOperator value)
        {
            OperatorCB.SelectedItem = value;

            if (ShowDialog() != DialogResult.OK)
            {
                return false;
            }
            
            value = (FilterOperator)OperatorCB.SelectedItem;
            
            return true;
        }
        #endregion
    }
}
