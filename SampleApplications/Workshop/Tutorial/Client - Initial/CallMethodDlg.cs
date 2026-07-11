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

namespace TutorialClient
{
    /// <summary>
    /// Prompts the user to select an area to use as an event filter.
    /// </summary>
    public partial class CallMethodDlg : Form
    {
        #region Constructors
        /// <summary>
        /// Creates an empty form.
        /// </summary>
        public CallMethodDlg()
        {
            InitializeComponent();
        }
        #endregion
        
        #region Private Fields
        private Session m_session;
        private NodeId m_objectId;
        private NodeId m_methodId;
        private int m_firstOutputArgument;
        #endregion
        
        #region Public Interface
        public void Show(Session session, NodeId objectId, NodeId methodId)
        {
            m_session = session;
            m_objectId = objectId;
            m_methodId = methodId;

            Show();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Updates the list control.
        /// </summary>
        private void UpdateList(Session session, Argument[] arguments, string browseName)
        {
            for (int ii = 0; ii < arguments.Length; ii++)
            {
                Argument argument = arguments[ii];
                Variant defaultValue = new Variant(TypeInfo.GetDefaultValue(argument.DataType, argument.ValueRank));

                ListViewItem item = new ListViewItem(arguments[ii].Name);

                if (browseName == BrowseNames.InputArguments)
                {
                    item.SubItems.Add("IN");
                    m_firstOutputArgument++;
                }
                else
                {
                    item.SubItems.Add("OUT");
                }

                string dataType = session.NodeCache.GetDisplayText(arguments[ii].DataType);

                if (arguments[ii].ValueRank >= 0)
                {
                    dataType += "[]";
                }

                item.SubItems.Add(defaultValue.ToString());
                item.SubItems.Add(dataType);
                item.SubItems.Add(Utils.Format("{0}", arguments[ii].Description));
                item.Tag = defaultValue;

                ArgumentsLV.Items.Add(item);
            }
        }
        #endregion

        #region Event Handlers
        private void OkBTN_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelBTN_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

    }
}
