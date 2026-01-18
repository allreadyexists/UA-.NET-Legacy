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
using Opc.Ua.Client.Controls;

namespace TutorialClient
{
    /// <summary>
    /// Prompts the user to select an area to use as an event filter.
    /// </summary>
    public partial class SetUserAndLocaleDlg: Form
    {
        #region Constructors
        /// <summary>
        /// Creates an empty form.
        /// </summary>
        public SetUserAndLocaleDlg()
        {
            InitializeComponent();
        }
        #endregion
        
        #region Private Fields
        private Session m_session;
        #endregion
        
        #region Public Interface
        /// <summary>
        /// Prompts the user to specify the user name and locale.
        /// </summary>
        public bool ShowDialog(Session session)
        {
            m_session = session;

            #region Task #D3 - Change Locale and User Identity
            UpdateUserIdentity(session);
            UpdateLocale(session);
            #endregion
            
            // display the dialog.
            if (ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            return true;
        }
        #endregion

        #region Task #D3 - Change Locale and User Identity
        /// <summary>
        /// Updates the local displayed in the control.
        /// </summary>
        private void UpdateUserIdentity(Session session)
        {
            UserNameTB.Text = null;
            PasswordTB.Text = null;

            // get the current identity.
            IUserIdentity identity = session.Identity;

            if (identity != null && identity.TokenType == UserTokenType.UserName)
            {
                UserNameIdentityToken token = identity.GetIdentityToken() as UserNameIdentityToken;

                if (token != null)
                {
                    UserNameTB.Text = token.UserName;
                    PasswordTB.Text = token.DecryptedPassword;
                }
            }
        }

        /// <summary>
        /// Updates the local displayed in the control.
        /// </summary>
        private void UpdateLocale(Session session)
        {
            LocaleCB.Items.Clear();

            // get the locales from the server.
            DataValue value = m_session.ReadValue(VariableIds.Server_ServerCapabilities_LocaleIdArray);

            if (value != null)
            {
                string[] availableLocales = value.GetValue<string[]>(null);

                if (availableLocales != null)
                {
                    for (int ii = 0; ii < availableLocales.Length; ii++)
                    {
                        LocaleCB.Items.Add(availableLocales[ii]);
                    }
                }
            }

            // select the default locale.
            if (LocaleCB.Items.Count > 0)
            {
                LocaleCB.SelectedIndex = 0;
            }

            // select the cutrren locale for the session.
            if (session.PreferredLocales != null)
            {
                for (int ii = 0; ii < session.PreferredLocales.Count; ii++)
                {
                    int index = LocaleCB.FindStringExact(session.PreferredLocales[ii]);

                    if (index >= 0)
                    {
                        LocaleCB.SelectedIndex = index;
                        break;
                    }
                }
            }
        }
        #endregion

        #region Private Methods
        #endregion

        #region Event Handlers
        private void OkBTN_Click(object sender, EventArgs e)
        {
            try
            {
                #region Task #D3 - Change Locale and User Identity
                UserIdentity identity = null;

                // use the anonymous identity of the user name is not provided.
                if (String.IsNullOrEmpty(UserNameTB.Text))
                {
                    identity = new UserIdentity();
                }

                // could add check for domain name in user name and use a kerberos token instead.
                else
                {
                    identity = new UserIdentity(UserNameTB.Text, PasswordTB.Text);
                }

                // can specify multiple locales but just use one here to keep the UI simple.
                StringCollection preferredLocales = new StringCollection();
                preferredLocales.Add(LocaleCB.SelectedItem as string);

                // override the default diagnostics to get error messages.
                DiagnosticsMasks returnDiagnostics = m_session.ReturnDiagnostics;

                try
                {
                    // update the session.
                    m_session.ReturnDiagnostics = DiagnosticsMasks.ServiceSymbolicIdAndText;
                    m_session.UpdateSession(identity, preferredLocales);
                }
                finally
                {
                    m_session.ReturnDiagnostics = returnDiagnostics;
                }
                #endregion

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
