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
using System.Security.Principal;
using CubicOrange.Windows.Forms.ActiveDirectory;

using Opc.Ua.Client.Controls;
using Opc.Ua.Configuration;

namespace Opc.Ua.Configuration
{
    /// <summary>
    /// Prompts the user to specify a new access rule for a file.
    /// </summary>
    public partial class AccessRuleDlg : Form
    {
        #region Constructors
        /// <summary>
        /// Initializes the dialog.
        /// </summary>
        public AccessRuleDlg()
        {
            InitializeComponent();
            
            AccessTypeCB.Items.Add(AccessControlType.Allow);
            AccessTypeCB.Items.Add(AccessControlType.Deny);
            
            AccessRightCB.Items.Add(ApplicationAccessRight.Run);
            AccessRightCB.Items.Add(ApplicationAccessRight.Update);
            AccessRightCB.Items.Add(ApplicationAccessRight.Configure);
        }
        #endregion
        
        #region Private Fields
        IdentityReference m_identity;
        #endregion
        
        #region Public Interface
        /// <summary>
        /// Displays the dialog.
        /// </summary>
        public bool ShowDialog(ApplicationAccessRule accessRule)
        {
            AccessTypeCB.SelectedItem = accessRule.RuleType;
            IdentityNameTB.Text = accessRule.IdentityName;
            m_identity = accessRule.IdentityReference;

            if (m_identity == null)
            {
                AccountInfo account = AccountInfo.Create(IdentityNameTB.Text);

                if (account != null)
                {
                    m_identity = account.GetIdentityReference();
                }
            }

            if (accessRule.Right != ApplicationAccessRight.None)
            {
                AccessRightCB.SelectedItem = accessRule.Right;
            }

            if (ShowDialog() != DialogResult.OK)
            {
                return false;
            }
                    
            accessRule.RuleType = (AccessControlType)AccessTypeCB.SelectedItem;
            accessRule.Right = (ApplicationAccessRight)AccessRightCB.SelectedItem;
            accessRule.IdentityReference = m_identity;

            return true;
        }
        #endregion

        #region Event Handlers
        private void OkBTN_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_identity == null)
                {
                    AccountInfo account = AccountInfo.Create(IdentityNameTB.Text);

                    if (account == null)
                    {
                        throw new ApplicationException("Please specified a valid identity.");
                    }

                    m_identity = account.GetIdentityReference();
                }

                // close the dialog.
                DialogResult = DialogResult.OK;
            }
            catch (Exception exception)
            {
                GuiUtils.HandleException(this.Text, System.Reflection.MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void BrowseBTN_Click(object sender, EventArgs e)
        {            
            try
            {
                DirectoryObjectPickerDialog picker = new DirectoryObjectPickerDialog();

                picker.AllowedObjectTypes = CubicOrange.Windows.Forms.ActiveDirectory.ObjectTypes.Computers | CubicOrange.Windows.Forms.ActiveDirectory.ObjectTypes.BuiltInGroups | CubicOrange.Windows.Forms.ActiveDirectory.ObjectTypes.Groups | CubicOrange.Windows.Forms.ActiveDirectory.ObjectTypes.Users | CubicOrange.Windows.Forms.ActiveDirectory.ObjectTypes.WellKnownPrincipals;
                picker.DefaultObjectTypes = picker.AllowedObjectTypes;
                picker.AllowedLocations = CubicOrange.Windows.Forms.ActiveDirectory.Locations.All;
                picker.DefaultLocations = CubicOrange.Windows.Forms.ActiveDirectory.Locations.All;
                picker.MultiSelect = false;
                picker.TargetComputer = null;
                
                if (picker.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                } 
                
                DirectoryObject[] results = picker.SelectedObjects;

                if (results == null || results.Length != 1)
                {
                    return;
                }

                if (!String.IsNullOrEmpty(results[0].Path))
                {
                    string path = results[0].Path;                    
                    string[] fields = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                    string domain  = fields[fields.Length-2];
                    string account = fields[fields.Length-1];

                    if (String.Compare(domain, System.Net.Dns.GetHostName(), StringComparison.InvariantCultureIgnoreCase) == 0)
                    {
                        m_identity = new NTAccount(account);
                    }
                    else
                    {
                        m_identity = new NTAccount(domain, account);
                    }
                }
                else
                {
                    m_identity = new NTAccount(results[0].Name);
                }

                if (m_identity != null)
                {
                    IdentityNameTB.Text = m_identity.ToString();
                }
            }
            catch (Exception exception)
            {
                GuiUtils.HandleException(this.Text, System.Reflection.MethodBase.GetCurrentMethod(), exception);
            }
        }
        #endregion
    }
}
