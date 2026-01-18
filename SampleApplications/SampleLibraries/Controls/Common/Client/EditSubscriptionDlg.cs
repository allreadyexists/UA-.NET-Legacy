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

namespace Opc.Ua.Client.Controls
{
    /// <summary>
    /// Prompts the user to edit a value.
    /// </summary>
    public partial class EditSubscriptionDlg : Form
    {
        #region Constructors
        /// <summary>
        /// Creates an empty form.
        /// </summary>
        public EditSubscriptionDlg()
        {
            InitializeComponent();
            this.Icon = ClientUtils.GetAppIcon();
        }
        #endregion
      
        #region Private Fields
        #endregion

        #region Public Interface
        /// <summary>
        /// Prompts the user to edit the monitored item.
        /// </summary>
        public bool ShowDialog(Subscription subscription)
        {
            PublishingIntervalUP.Value = subscription.PublishingInterval;
            KeepAliveCountUP.Value = subscription.KeepAliveCount;
            LifetimeCountUP.Value = subscription.LifetimeCount;
            MaxNotificationsPerPublishUP.Value = subscription.MaxNotificationsPerPublish;
            PriorityTB.Value = subscription.Priority;
            PublishingEnabledCK.Checked = subscription.PublishingEnabled;

            if (base.ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            subscription.PublishingInterval = (int)PublishingIntervalUP.Value;
            subscription.KeepAliveCount = (uint)KeepAliveCountUP.Value;
            subscription.LifetimeCount = (uint)LifetimeCountUP.Value;
            subscription.MaxNotificationsPerPublish = (uint)MaxNotificationsPerPublishUP.Value;
            subscription.Priority = (byte)PriorityTB.Value;
            subscription.PublishingEnabled  = PublishingEnabledCK.Checked;

            return true;
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
