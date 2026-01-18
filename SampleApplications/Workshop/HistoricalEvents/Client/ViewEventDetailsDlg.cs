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
using System.Text;
using System.Windows.Forms;
using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Client.Controls;

namespace Quickstarts.HistoricalEvents.Client
{
    /// <summary>
    /// Displays all fields associated with an event notification.
    /// </summary>
    public partial class ViewEventDetailsDlg : Form
    {
        #region Constructors
        /// <summary>
        /// Creates an empty form.
        /// </summary>
        public ViewEventDetailsDlg()
        {
            InitializeComponent();
        }
        #endregion
        
        #region Private Fields
        #endregion
        
        #region Public Interface
        /// <summary>
        /// Shows all fields for the current condition.
        /// </summary>
        public bool ShowDialog(FilterDeclaration filter, VariantCollection fields)
        {
            // fill in dialog.
            for (int ii = 0; ii < filter.Fields.Count; ii++)
            {
                InstanceDeclaration instance = filter.Fields[ii].InstanceDeclaration;
                ListViewItem item = new ListViewItem(instance.DisplayPath);
                item.SubItems.Add(instance.DataTypeDisplayText);

                string text = null;

                // check for missing fields.
                if (fields.Count <= ii || fields[ii].Value == null)
                {
                    text = String.Empty;
                }

                // use default string format.
                else
                {
                    text = fields[ii].ToString();
                }

                item.SubItems.Add(text);
                item.Tag = filter.Fields[ii];
                FieldsLV.Items.Add(item);
            }

            // adjust columns.
            for (int ii = 0; ii < FieldsLV.Columns.Count; ii++)
            {
                FieldsLV.Columns[ii].Width = -2;
            }

            // display the dialog.
            if (ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
