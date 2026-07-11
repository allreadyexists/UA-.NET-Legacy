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
using System.Text;
using System.Windows.Forms;
using Opc.Ua;
using Opc.Ua.Client;

namespace Opc.Ua.Client.Controls
{
    /// <summary>
    /// Allows the user to edit and issue call requests.
    /// </summary>
    public partial class CallRequestDlg : Form, ISessionForm
    {
        #region Constructors
        /// <summary>
        /// Creates an empty form.
        /// </summary>
        public CallRequestDlg()
        {
            InitializeComponent();
            this.Icon = ClientUtils.GetAppIcon();
        }
        #endregion
        
        #region Private Fields
        private Session m_session;
        #endregion
        
        #region Public Interface
        /// <summary>
        /// Changes the session used for the call request.
        /// </summary>
        public void ChangeSession(Session session)
        {
            m_session = session;
            CallRequestCTRL.ChangeSession(session);
        }

        /// <summary>
        /// Sets the method called by the control.
        /// </summary>
        public void SetMethod(NodeId objectId, NodeId methodId)
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append("Calling Method ");
            buffer.Append(m_session.NodeCache.GetDisplayText(methodId));
            buffer.Append(" on Object ");
            buffer.Append(m_session.NodeCache.GetDisplayText(objectId));
            this.Text = buffer.ToString();

            CallRequestCTRL.SetMethod(objectId, methodId);
        }
        #endregion

        #region Private Methods
        #endregion

        #region Event Handlers
        private void CallBTN_Click(object sender, EventArgs e)
        {
            try
            {
                CallRequestCTRL.Call();
                CallBTN.Visible = false;
                BackBTN.Visible = true;
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException(this.Text, exception);
            }
        }

        private void BackBTN_Click(object sender, EventArgs e)
        {
            try
            {
                CallRequestCTRL.Back();
                CallBTN.Visible = true;
                BackBTN.Visible = false;
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException(this.Text, exception);
            }
        }

        private void CloseBTN_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Modal)
                {
                    DialogResult = DialogResult.Cancel;
                }
                else
                {
                    this.Close();
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
