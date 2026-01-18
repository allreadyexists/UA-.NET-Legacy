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
using System.Drawing;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Data;
using Opc.Ua;
using Opc.Ua.Client;

namespace Opc.Ua.Client.Controls
{
    /// <summary>
    /// Prompts the user to edit a value.
    /// </summary>
    public partial class EditArrayDlg : Form
    {
        #region Constructors
        /// <summary>
        /// Creates an empty form.
        /// </summary>
        public EditArrayDlg()
        {
            InitializeComponent();
            this.Icon = ClientUtils.GetAppIcon();
            ArrayDV.AutoGenerateColumns = false;

            m_dataset = new DataSet();
            m_dataset.Tables.Add("Array");
            m_dataset.Tables[0].Columns.Add("Value", typeof(string));
            m_dataset.Tables[0].Columns.Add("Index", typeof(int));
            m_dataset.Tables[0].DefaultView.Sort = "Index";

            ArrayDV.DataSource = m_dataset.Tables[0];
        }
        #endregion

        #region Private Fields
        private DataSet m_dataset;
        private BuiltInType m_dataType;
        #endregion

        #region Public Interface
        /// <summary>
        /// Prompts the user to edit an array value.
        /// </summary>
        public Array ShowDialog(Array value, BuiltInType dataType, bool readOnly, string caption)
        {
            if (caption != null)
            {
                this.Text = caption;
            }

            // detect the data type.
            if (dataType == BuiltInType.Null)
            {
                dataType = TypeInfo.Construct(value).BuiltInType;
            }

            m_dataType = dataType;
            ArrayDV.AllowUserToAddRows = !readOnly;
            ArrayDV.AllowUserToDeleteRows = !readOnly;
            ArrayDV.RowHeadersVisible = !readOnly;
            m_dataset.Tables[0].Clear();

            if (value != null)
            {
                for (int ii = 0; ii < value.Length; ii++)
                {
                    DataRow row = m_dataset.Tables[0].NewRow();
                    row[0] = new Variant(value.GetValue(ii)).ToString();
                    row[1] = ii;
                    m_dataset.Tables[0].Rows.Add(row);
                }
            }

            m_dataset.AcceptChanges();

            if (ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            m_dataset.AcceptChanges();

            if (!readOnly)
            {
                value = TypeInfo.CreateArray(dataType, m_dataset.Tables[0].Rows.Count);

                for (int ii = 0; ii < m_dataset.Tables[0].DefaultView.Count; ii++)
                {
                    string oldValue = m_dataset.Tables[0].DefaultView[ii].Row[0] as string;
                    object newValue = TypeInfo.Cast(oldValue, m_dataType);
                    value.SetValue(newValue, ii);
                }
            }

            return value;
        }
        #endregion

        #region Event Handlers
        private void OkBTN_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.OK;
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException(this.Text, exception);
            }
        }

        private void ArrayDV_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                object newValue = TypeInfo.Cast(e.FormattedValue, m_dataType);
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException(this.Text, exception);
                e.Cancel = true;
            }
        }

        private void DeleteMI_Click(object sender, EventArgs e)
        {
            try
            {
                for (int ii = 0; ii < ArrayDV.SelectedRows.Count; ii++)
                {
                    DataGridViewRow row = ArrayDV.SelectedRows[ii];
                    DataRowView source = row.DataBoundItem as DataRowView;
                    source.Row.Delete();
                }

                m_dataset.AcceptChanges();
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException(this.Text, exception);
            }
        }

        private void InsertMI_Click(object sender, EventArgs e)
        {
            try
            {
                for (int ii = 0; ii < ArrayDV.SelectedRows.Count; ii++)
                {
                    DataGridViewRow currentRow = ArrayDV.SelectedRows[ii];
                    DataRowView source = currentRow.DataBoundItem as DataRowView;

                    int index = (int)source.Row[1];

                    for (int jj = 0; jj < m_dataset.Tables[0].Rows.Count; jj++)
                    {
                        int current = (int)m_dataset.Tables[0].Rows[jj][1];

                        if (current >= index)
                        {
                            m_dataset.Tables[0].Rows[jj][1] = current + 1;
                        }
                    }

                    DataRow row = m_dataset.Tables[0].NewRow();
                    row[0] = new Variant(TypeInfo.GetDefaultValue(m_dataType));
                    row[1] = index;
                    m_dataset.Tables[0].Rows.Add(row);
                }

                m_dataset.AcceptChanges();
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException(this.Text, exception);
            }
        }
        #endregion
    }
}
