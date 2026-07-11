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
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Opc.Ua.Client.Controls
{
    /// <summary>
    /// A control with button that displays edit array dialog.
    /// </summary>
    public partial class SelectProfileCtrl : UserControl
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of the control.
        /// </summary>
        public SelectProfileCtrl()
        {
            InitializeComponent();
        }
        #endregion
        
        #region Private Fields
        private event EventHandler m_ProfilesChanged;
        private Opc.Ua.Security.ListOfSecurityProfiles m_profiles;
        #endregion
        
        #region Public Interface
        /// <summary>
        /// The list of available security profiles.
        /// </summary>
        public Opc.Ua.Security.ListOfSecurityProfiles Profiles 
        {
            get 
            { 
                return m_profiles; 
            }

            set
            {
                if (CurrentProfilesControl != null)
                {
                    StringBuilder builder = new StringBuilder();

                    if (value != null)
                    {
                        for (int ii = 0; ii < value.Count; ii++)
                        {
                            if (value[ii].Enabled)
                            {
                                if (builder.Length > 0)
                                {
                                    builder.Append(", ");
                                }

                                builder.Append(SecurityPolicies.GetDisplayName(value[ii].ProfileUri));
                            }
                        }
                    }

                    CurrentProfilesControl.Text = builder.ToString();
                }

                m_profiles = value;

            }
        }
        
        /// <summary>
        /// Gets or sets the control that is stores with the current file path.
        /// </summary>
        public Control CurrentProfilesControl { get; set; }

        /// <summary>
        /// Raised when the profiles are changed.
        /// </summary>
        public event EventHandler ProfilesChanged
        {
            add { m_ProfilesChanged += value; }
            remove { m_ProfilesChanged -= value; }
        }
        #endregion

        #region Event Handlers
        private void BrowseBTN_Click(object sender, EventArgs e)
        {
            if (CurrentProfilesControl == null)
            {
                return;
            }

            Opc.Ua.Security.ListOfSecurityProfiles profiles = new SelectProfileDlg().ShowDialog(Profiles, null);

            if (profiles == null)
            {
                return;
            }

            Profiles = profiles;

            if (m_ProfilesChanged != null)
            {
                m_ProfilesChanged(this, e);
            }
        }
        #endregion
    }
}
