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
using System.Data;
using Opc.Ua;
using Opc.Ua.Client;

namespace Opc.Ua.Client.Controls
{
    /// <summary>
    /// Prompts the user to edit a value.
    /// </summary>
    public partial class ViewNodeStateDlg : Form
    {
        #region Constructors
        /// <summary>
        /// Creates an empty form.
        /// </summary>
        public ViewNodeStateDlg()
        {
            InitializeComponent();
            this.Icon = ClientUtils.GetAppIcon();
            ResultsDV.AutoGenerateColumns = false;

            m_dataset = new DataSet();
            m_dataset.Tables.Add("Results");

            m_dataset.Tables[0].Columns.Add("Index", typeof(int));
            m_dataset.Tables[0].Columns.Add("BrowsePath", typeof(string));
            m_dataset.Tables[0].Columns.Add("DataType", typeof(string));
            m_dataset.Tables[0].Columns.Add("Value", typeof(Variant));

            m_dataset.Tables[0].DefaultView.Sort = "Index";

            ResultsDV.DataSource = m_dataset.Tables[0];
        }
        #endregion
        
        #region Private Fields
        private Session m_session;
        private DataSet m_dataset;
        #endregion

        #region Public Interface
        /// <summary>
        /// Prompts the user to edit a value.
        /// </summary>
        public bool ShowDialog(Session session, NodeState node, string caption)
        {
            m_session = session;

            if (caption != null)
            {
                this.Text = caption;
            }

            PopulateDataView(m_session.SystemContext, node, String.Empty);
            m_dataset.AcceptChanges();

            if (ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Recursively populates the data view.
        /// </summary>
        private void PopulateDataView(
            ISystemContext context,
            NodeState parent,
            string parentPath)
        {
            List<BaseInstanceState> children = new List<BaseInstanceState>();
            parent.GetChildren(context, children);

            for (int ii = 0; ii < children.Count; ii++)
            {
                BaseInstanceState child = children[ii];

                StringBuilder childPath = new StringBuilder();

                if (!String.IsNullOrEmpty(parentPath))
                {
                    childPath.Append(parentPath);
                    childPath.Append("/");
                }

                childPath.Append(child.GetDisplayText());

                if (child.NodeClass == NodeClass.Variable)
                {
                    BaseVariableState variable = (BaseVariableState)child;

                    if (StatusCode.IsGood(variable.StatusCode))
                    {
                        string dataType = m_session.NodeCache.GetDisplayText(variable.DataType);

                        if (variable.ValueRank >= 0)
                        {
                            dataType += "[]"; 
                        }

                        DataRow row = m_dataset.Tables[0].NewRow();
                        row[0] = m_dataset.Tables[0].Rows.Count;
                        row[1] = childPath.ToString();
                        row[2] = dataType;
                        row[3] = variable.WrappedValue;
                        m_dataset.Tables[0].Rows.Add(row);
                    }
                }

                PopulateDataView(context, child, childPath.ToString());
            }
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
        #endregion
    }
}
