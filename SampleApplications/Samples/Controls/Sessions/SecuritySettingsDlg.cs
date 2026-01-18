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
using System.ServiceModel;
using System.Reflection;
using System.IdentityModel.Claims;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;
using System.ServiceModel.Channels;

using Opc.Ua.Bindings;
using Opc.Ua.Client.Controls;

namespace Opc.Ua.Sample.Controls
{
    /// <summary>
    /// Prompts the user to create a new secure channel.
    /// </summary>
    public partial class SecuritySettingsDlg : Form
    {
        public SecuritySettingsDlg()
        {
            InitializeComponent();
            this.Icon = ClientUtils.GetAppIcon();

            foreach (MessageSecurityMode value in Enum.GetValues(typeof(MessageSecurityMode)))
            {
                SecurityModeCB.Items.Add(value);
            }

            foreach (string value in SecurityPolicies.GetDisplayNames())
            {
                SecurityPolicyUriCB.Items.Add(value);
            }
        }

        /// <summary>
        /// Displays the dialog.
        /// </summary>
        public bool ShowDialog(ref MessageSecurityMode securityMode, ref string securityPolicyUri, ref bool useNativeStack)
        {
            // set security mode.
            SecurityModeCB.SelectedItem = securityMode;
            
            // set security policy uri
            SecurityPolicyUriCB.SelectedIndex = -1;

            // set native stack flag.
            UseNativeStackCK.Checked = useNativeStack;

            if (!String.IsNullOrEmpty(securityPolicyUri))
            {
                SecurityPolicyUriCB.SelectedItem = SecurityPolicies.GetDisplayName(securityPolicyUri);
            }

            // show dialog.
            if (ShowDialog() != DialogResult.OK)
            {
                return false;
            }
                
            securityMode      = (MessageSecurityMode)SecurityModeCB.SelectedItem;
            securityPolicyUri = SecurityPolicies.GetUri((string)SecurityPolicyUriCB.SelectedItem);
            useNativeStack    = UseNativeStackCK.Checked;
                       
            return true;
        }

        private void OkBTN_Click(object sender, EventArgs e)
        {
            try
            {
                // close the dialog.
                DialogResult = DialogResult.OK;
            }
            catch (Exception exception)
            {
                GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }
    }
}
