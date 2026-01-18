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
using System.Reflection;
using Opc.Ua.Configuration;

namespace Opc.Ua.Client.Controls
{
    /// <summary>
    /// Prompts the user to edit a ApplicationDescription.
    /// </summary>
    public partial class CertificateStoreTreeDlg : Form
    {
        /// <summary>
        /// Contructs the object.
        /// </summary>
        public CertificateStoreTreeDlg()
        {
            InitializeComponent();
            this.Icon = ClientUtils.GetAppIcon();
        }
        
        /// <summary>
        /// Displays the dialog.
        /// </summary>
        public CertificateStoreIdentifier ShowDialog(CertificateStoreIdentifier store)
        {
            ContainersCTRL.Initialize();

            if (ShowDialog() != DialogResult.OK)
            {
                return null;
            }
  
            return ContainersCTRL.SelectedStore;
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

        private void ContainersCTRL_NodeSelected(object sender, TreeNodeActionEventArgs e)
        {
            try
            {
                OkBTN.Enabled = ContainersCTRL.SelectedStore != null;
            }
            catch (Exception exception)
            {
                GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }
    }
}
