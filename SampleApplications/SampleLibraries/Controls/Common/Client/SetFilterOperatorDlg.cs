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
    /// Prompts the user to select an area to use as an event filter.
    /// </summary>
    public partial class SetFilterOperatorDlg : Form
    {
        #region Constructors
        /// <summary>
        /// Creates an empty form.
        /// </summary>
        public SetFilterOperatorDlg()
        {
            InitializeComponent();
            this.Icon = ClientUtils.GetAppIcon();

            FilterOperatorCB.Items.Add(FilterOperator.IsNull);
            FilterOperatorCB.Items.Add(FilterOperator.Equals);
            FilterOperatorCB.Items.Add(FilterOperator.GreaterThan);
            FilterOperatorCB.Items.Add(FilterOperator.LessThan);
            FilterOperatorCB.Items.Add(FilterOperator.GreaterThanOrEqual);
            FilterOperatorCB.Items.Add(FilterOperator.LessThanOrEqual);
            FilterOperatorCB.Items.Add(FilterOperator.Like);
            FilterOperatorCB.Items.Add(FilterOperator.Not);
            FilterOperatorCB.Items.Add(FilterOperator.OfType);
            FilterOperatorCB.Items.Add(FilterOperator.BitwiseAnd);
            FilterOperatorCB.Items.Add(FilterOperator.BitwiseOr);
        }
        #endregion
        
        #region Private Fields
        #endregion

        #region Public Interface
        /// <summary>
        /// Displays the available areas in a tree view.
        /// </summary>
        public bool ShowDialog(ref FilterOperator filterOperator)
        {
            FilterOperatorCB.SelectedItem = filterOperator;
 
            // display the dialog.
            if (base.ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            filterOperator = (FilterOperator)FilterOperatorCB.SelectedItem;
            return true;
        }
        #endregion
        
        #region Private Methods
        #endregion

        #region Event Handlers
        #endregion
    }
}
