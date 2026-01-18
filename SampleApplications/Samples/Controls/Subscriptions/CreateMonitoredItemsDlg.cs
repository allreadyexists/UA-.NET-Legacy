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
using Opc.Ua.Client.Controls;

namespace Opc.Ua.Sample.Controls
{
    public partial class CreateMonitoredItemsDlg : Form
    {
        #region Constructors
        public CreateMonitoredItemsDlg()
        {
            InitializeComponent();
            this.Icon = ClientUtils.GetAppIcon();
            m_SubscriptionStateChanged = new SubscriptionStateChangedEventHandler(Subscription_StateChanged);
        }
        #endregion

        #region Private Fields
        private Subscription m_subscription;
        private SubscriptionStateChangedEventHandler m_SubscriptionStateChanged;
        #endregion
        
        #region Public Interface
        /// <summary>
        /// Displays the dialog.
        /// </summary>
        public void Show(Subscription subscription, bool useTypeModel)
        {
            if (subscription == null) throw new ArgumentNullException("subscription");
            
            Show();
            BringToFront();

            // remove previous subscription.
            if (m_subscription != null)
            {
                m_subscription.StateChanged -= m_SubscriptionStateChanged;
            }
            
            // start receiving notifications from the new subscription.
            m_subscription = subscription;
  
            if (subscription != null)
            {
                m_subscription.StateChanged += m_SubscriptionStateChanged;
            }                    

            m_subscription = subscription;
            
            BrowseCTRL.AllowPick = true;
            BrowseCTRL.SetView(subscription.Session, (useTypeModel)?BrowseViewType.ObjectTypes:BrowseViewType.Objects, null);

            MonitoredItemsCTRL.Initialize(subscription);
        }
        #endregion
        
        #region Event Handlers
        /// <summary>
        /// Handles a change to the state of the subscription.
        /// </summary>
        void Subscription_StateChanged(Subscription subscription, SubscriptionStateChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(m_SubscriptionStateChanged, subscription, e);
                return;
            }
            else if (!IsHandleCreated)
            {
                return;
            }

            try
            {
                // ignore notifications for other subscriptions.
                if (!Object.ReferenceEquals(m_subscription,  subscription))
                {
                    return;
                }

                // notify controls of the change.
                MonitoredItemsCTRL.SubscriptionChanged(e);
            }
            catch (Exception exception)
            {
				GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void BrowseCTRL_ItemsSelected(object sender, NodesSelectedEventArgs e)
        {
            try
            {
                foreach (ReferenceDescription reference in e.References)
                {
                    if (reference.ReferenceTypeId == ReferenceTypeIds.HasProperty || reference.IsForward)
                    {
                        MonitoredItemsCTRL.AddItem(reference);
                    }
                }
            }
            catch (Exception exception)
            {
				GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void ApplyBTN_Click(object sender, EventArgs e)
        {
            try
            {
                MonitoredItemsCTRL.ApplyChanges(true);
            }
            catch (Exception exception)
            {
				GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
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
				GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }
        #endregion
    }
}
