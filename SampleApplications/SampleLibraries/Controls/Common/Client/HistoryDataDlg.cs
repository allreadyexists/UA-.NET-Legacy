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
using System.Drawing;
using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Client.Controls;

namespace Opc.Ua.Client.Controls
{
    /// <summary>
    /// Allows the user to read and update history for a variable.
    /// </summary>
    public partial class HistoryDataDlg : Form, ISessionForm
    {
        #region Constructors
        /// <summary>
        /// Creates an empty form.
        /// </summary>
        public HistoryDataDlg()
        {
            InitializeComponent();
            this.Icon = ClientUtils.GetAppIcon();
        }
        #endregion
        
        #region Private Fields
        #endregion

        #region Public Interface
        /// <summary>
        /// Changes the session used.
        /// </summary>
        public void ChangeSession(Session session)
        {
            HistoryDataCTRL.ChangeSession(session);
        }

        /// <summary>
        /// Sets the variable shown in the dialog.
        /// </summary>
        public void SetVariable(NodeId variableId)
        {
            HistoryDataCTRL.ChangeNode(variableId);
        }
        #endregion

        #region Private Methods
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

        private void CancelBTN_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Modal)
                {
                    DialogResult = DialogResult.Cancel;
                }
                else
                {
                    Close();
                }
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException(this.Text, exception);
            }
        }
        #endregion
    }
}
