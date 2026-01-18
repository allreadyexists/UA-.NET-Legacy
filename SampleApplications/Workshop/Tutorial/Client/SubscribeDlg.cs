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
using Opc.Ua.Client.Controls;

namespace TutorialClient
{
    /// <summary>
    /// Prompts the user to select an area to use as an event filter.
    /// </summary>
    public partial class SubscribeDlg : Form
    {
        #region Constructors
        /// <summary>
        /// Creates an empty form.
        /// </summary>
        public SubscribeDlg()
        {
            InitializeComponent();
            CreateDataSet();
        }
        #endregion
        
        #region Private Fields
        private Session m_session;
        private NodeId m_nodeId;
        private DataSet m_dataset;
        private int m_nextId;
        #endregion
        
        #region Public Interface
        public void Show(Session session, NodeId nodeId)
        {
            m_session = session;
            m_nodeId = nodeId;

            Show();
        }
        #endregion
        
        #region Private Methods
        /// <summary>
        /// Creates the dataset and initializes the view.
        /// </summary>
        private void CreateDataSet()
        {
            DataSet dataset = new DataSet();
            dataset.Tables.Add("Results");

            DataColumn key = dataset.Tables[0].Columns.Add("Index", typeof(int));
            dataset.Tables[0].PrimaryKey = new DataColumn[] { key };

            dataset.Tables[0].Columns.Add("Timestamp", typeof(string));
            dataset.Tables[0].Columns.Add("Value", typeof(string));
            dataset.Tables[0].Columns.Add("StatusCode", typeof(string));
            dataset.Tables[0].Columns.Add("DataType", typeof(string));

            ResultsDV.Columns.Clear();
            ResultsDV.AutoGenerateColumns = false;

            for (int ii = 1; ii < dataset.Tables[0].Columns.Count; ii++)
            {
                string columnName = dataset.Tables[0].Columns[ii].ColumnName;
                ResultsDV.Columns.Add(columnName, columnName);
                ResultsDV.Columns[ResultsDV.Columns.Count - 1].DataPropertyName = columnName;
            }

            dataset.Tables[0].DefaultView.Sort = "Index";

            m_dataset = dataset;
            ResultsDV.DataSource = dataset.Tables[0];
        }

        /// <summary>
        /// Adds a value to the grid.
        /// </summary>
        private void AddValue(DataValue value, ModificationInfo modificationInfo)
        {
            DataRow row = m_dataset.Tables[0].NewRow();

            row[0] = m_nextId++;
            row[1] = value.SourceTimestamp.ToLocalTime().ToString("HH:mm:ss.fff");
            row[2] = value.WrappedValue;
            row[3] = new StatusCode(value.StatusCode.Code);

            if (value.WrappedValue.TypeInfo != null)
            {
                row[4] = value.WrappedValue.TypeInfo.BuiltInType.ToString();
            }
            else
            {
                row[4] = String.Empty;
            }

            m_dataset.Tables[0].Rows.Add(row);
        }
        #endregion

        #region Event Handlers
        private void SubscribeDlg_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException(this.Text, exception);
            }
        }
        #endregion
    }
}
