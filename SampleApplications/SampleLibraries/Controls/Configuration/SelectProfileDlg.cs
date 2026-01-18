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
using System.Drawing;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Data;
using Opc.Ua;
using Opc.Ua.Client;

namespace Opc.Ua.Client.Controls
{
    /// <summary>
    /// Prompts the user to edit a value.
    /// </summary>
    public partial class SelectProfileDlg : Form
    {
        #region Constructors
        /// <summary>
        /// Creates an empty form.
        /// </summary>
        public SelectProfileDlg()
        {
            InitializeComponent();
            this.Icon = ClientUtils.GetAppIcon();
        }
        #endregion

        #region Private Fields
        #endregion

        #region Public Interface
        /// <summary>
        /// Prompts the user to edit an array value.
        /// </summary>
        public Opc.Ua.Security.ListOfSecurityProfiles ShowDialog(Opc.Ua.Security.ListOfSecurityProfiles profiles, string caption)
        {
            if (caption != null)
            {
                this.Text = caption;
            }

            ProfilesLV.Items.Clear();

            if (profiles != null)
            {
                for (int ii = 0; ii < profiles.Count; ii++)
                {
                    ProfilesLV.Items.Add(profiles[ii].ProfileUri, profiles[ii].Enabled);
                }
            }

            if (ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            Opc.Ua.Security.ListOfSecurityProfiles results = new Opc.Ua.Security.ListOfSecurityProfiles();

            for (int ii = 0; ii < ProfilesLV.Items.Count; ii++)
            {
                Opc.Ua.Security.SecurityProfile profile = new Opc.Ua.Security.SecurityProfile();
                profile.ProfileUri = ProfilesLV.Items[ii] as string;
                profile.Enabled = ProfilesLV.CheckedIndices.Contains(ii);
                results.Add(profile);
            }

            return results;
        }
        #endregion
    }
}
