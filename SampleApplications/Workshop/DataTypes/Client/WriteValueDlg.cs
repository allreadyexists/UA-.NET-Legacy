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
    /// Prompts the user to edit a value.
    /// </summary>
    public partial class WriteValueDlg : Form
    {
        #region Constructors
        /// <summary>
        /// Creates an empty form.
        /// </summary>
        public WriteValueDlg()
        {
            InitializeComponent();
        }
        #endregion
        
        #region Private Fields
        private Session m_session;
        private NodeId m_nodeId;
        private TypeInfo m_sourceType;
        private Variant m_value;
        #endregion
        
        #region Public Interface
        /// <summary>
        /// Prompts the user to edit a value.
        /// </summary>
        public Variant ShowDialog(Session session, NodeId nodeId)
        {
            m_session = session;
            m_nodeId = nodeId;

            if (ShowDialog() != DialogResult.OK)
            {
                return Variant.Null;
            }

            return m_value;
        }
        #endregion

        #region Private Methods
        #endregion

        #region Event Handlers
        /// <summary>
        /// Handles the click event for the OK button.
        /// </summary>
        private void OkBTN_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.OK;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
