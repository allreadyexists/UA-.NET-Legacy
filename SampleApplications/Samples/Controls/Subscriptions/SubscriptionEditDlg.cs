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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using Opc.Ua.Client;

namespace Opc.Ua.Sample.Controls
{
    public partial class SubscriptionEditDlg : Form
    {
        public SubscriptionEditDlg()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Prompts the user to specify the browse options.
        /// </summary>
        public bool ShowDialog(Subscription subscription)
        {
            if (subscription == null) throw new ArgumentNullException("subscription");

            DisplayNameTB.Text          = subscription.DisplayName;
            PublishingIntervalNC.Value  = subscription.Created ? (decimal)subscription.CurrentPublishingInterval : (decimal)subscription.PublishingInterval;
            KeepAliveCountNC.Value      = subscription.Created ? subscription.CurrentKeepAliveCount : subscription.KeepAliveCount;
            LifetimeCountCTRL.Value     = subscription.Created ? subscription.CurrentLifetimeCount: subscription.LifetimeCount;
            MaxNotificationsCTRL.Value  = subscription.MaxNotificationsPerPublish;
            PriorityNC.Value            = subscription.Created ? subscription.CurrentPriority : subscription.Priority;
            PublishingEnabledCK.Checked = subscription.Created ? subscription.CurrentPublishingEnabled : subscription.PublishingEnabled;
            
            if (ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            subscription.DisplayName                = DisplayNameTB.Text;
            subscription.PublishingInterval         = (int)PublishingIntervalNC.Value;
            subscription.KeepAliveCount             = (uint)KeepAliveCountNC.Value;
            subscription.LifetimeCount              = (uint)LifetimeCountCTRL.Value;
            subscription.MaxNotificationsPerPublish = (uint)MaxNotificationsCTRL.Value;
            subscription.Priority                   = (byte)PriorityNC.Value;
            if (subscription.Created)
            {
                subscription.SetPublishingMode(PublishingEnabledCK.Checked);            
            }
            else
            {
                subscription.PublishingEnabled = PublishingEnabledCK.Checked;
            }
            return true;
        }
    }
}
