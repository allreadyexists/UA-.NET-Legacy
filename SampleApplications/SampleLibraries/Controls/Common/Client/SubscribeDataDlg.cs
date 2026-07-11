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
    public partial class SubscribeDataDlg : Form, ISessionForm
    {
        #region Constructors
        /// <summary>
        /// Creates an empty form.
        /// </summary>
        public SubscribeDataDlg()
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
        /// Changes the session used for the subscription.
        /// </summary>
        public void ChangeSession(Session session)
        {
            SubscribeRequestCTRL.ChangeSession(session);
            m_session = session;
        }

        /// <summary>
        /// Returns true if the dialog has an active subscription assigned.
        /// </summary>
        public bool HasSubscription
        {
            get
            {
                return SubscribeRequestCTRL.HasSubscription;
            }
        }

        /// <summary>
        /// Set the subscription managed by the control.
        /// </summary>
        public void SetSubscription(Subscription subscription)
        {
            SubscribeRequestCTRL.SetSubscription(subscription);
            NextBTN.Visible = SubscribeRequestCTRL.CanCallNext;
            BackBTN.Visible = SubscribeRequestCTRL.CanCallBack;
        }

        /// <summary>
        /// Adds the items to monitor.
        /// </summary>
        public void AddItems(params ReadValueId[] nodesToRead)
        {
            SubscribeRequestCTRL.AddItems(nodesToRead);
            NextBTN.Visible = SubscribeRequestCTRL.CanCallNext;
            BackBTN.Visible = SubscribeRequestCTRL.CanCallBack;
        }
        #endregion

        #region Private Methods
        #endregion

        #region Event Handlers
        private void NextBTN_Click(object sender, EventArgs e)
        {
            try
            {
                SubscribeRequestCTRL.Next();
                NextBTN.Visible = SubscribeRequestCTRL.CanCallNext;
                BackBTN.Visible = SubscribeRequestCTRL.CanCallBack;
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
                SubscribeRequestCTRL.Back();
                NextBTN.Visible = SubscribeRequestCTRL.CanCallNext;
                BackBTN.Visible = SubscribeRequestCTRL.CanCallBack;
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
