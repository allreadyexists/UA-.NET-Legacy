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
    public partial class RepublishNotificationMessageDlg : Form
    {
        #region Constructors
        public RepublishNotificationMessageDlg()
        {
            InitializeComponent();
            this.Icon = ClientUtils.GetAppIcon();
        }
        #endregion
        
        #region Private Fields
        private Subscription m_subscription;
        private NotificationMessage m_message;
        #endregion
        
        #region Public Interface
        /// <summary>
        /// Displays the dialog.
        /// </summary>
        public NotificationMessage ShowDialog(Subscription subscription)
        {
            if (subscription == null) throw new ArgumentNullException("subscription");

            m_subscription = subscription;

            SequenceNumberNC.Value = 0;

            foreach (uint sequenceNumber in m_subscription.AvailableSequenceNumbers)
            {
                if (SequenceNumberNC.Value < (decimal)sequenceNumber)
                {
                    SequenceNumberNC.Value = sequenceNumber;
                }
            }

            if (ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            return m_message;
        }
        #endregion

        #region Event Handlers
        private void OkBTN_Click(object sender, EventArgs e)
        {        
            try
            {
                m_message = m_subscription.Republish((uint)SequenceNumberNC.Value);
                DialogResult = DialogResult.OK;
            }
            catch (Exception exception)
            {
				GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }
        #endregion
    }
}
