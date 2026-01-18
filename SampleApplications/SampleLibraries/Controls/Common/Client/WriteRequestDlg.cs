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
    /// Allows the user to edit and issue read requests.
    /// </summary>
    public partial class WriteRequestDlg : Form, ISessionForm
    {
        #region Constructors
        /// <summary>
        /// Creates an empty form.
        /// </summary>
        public WriteRequestDlg()
        {
            InitializeComponent();
            this.Icon = ClientUtils.GetAppIcon();
        }
        #endregion
        
        #region Private Fields
        #endregion
        
        #region Public Interface
        /// <summary>
        /// Changes the session used for the read request.
        /// </summary>
        public void ChangeSession(Session session)
        {
            WriteRequestCTRL.ChangeSession(session);
        }

        /// <summary>
        /// Adds a node to the read request.
        /// </summary>
        public void AddNodes(params WriteValue[] nodesToWrite)
        {
            WriteRequestCTRL.AddNodes(nodesToWrite);
        }
        #endregion

        #region Private Methods
        #endregion

        #region Event Handlers
        private void ReadBTN_Click(object sender, EventArgs e)
        {
            try
            {
                WriteRequestCTRL.Read();
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException(this.Text, exception);
            }
        }

        private void WriteBTN_Click(object sender, EventArgs e)
        {
            try
            {
                WriteRequestCTRL.Write();
                ReadBTN.Visible = false;
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
                WriteRequestCTRL.Back();
                ReadBTN.Visible = true;
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
