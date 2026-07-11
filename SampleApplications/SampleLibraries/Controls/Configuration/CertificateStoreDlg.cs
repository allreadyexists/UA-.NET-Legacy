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
using System.Security.Cryptography.X509Certificates;

namespace Opc.Ua.Client.Controls
{
    /// <summary>
    /// Prompts the user to choose a certificate store.
    /// </summary>
    public partial class CertificateStoreDlg : Form
    {
        /// <summary>
        /// Contructs the object.
        /// </summary>
        public CertificateStoreDlg()
        {
            InitializeComponent();
            this.Icon = ClientUtils.GetAppIcon();
        }

        /// <summary>
        /// Displays the dialog.
        /// </summary>
        public CertificateStoreIdentifier ShowDialog(CertificateStoreIdentifier store)
        {
            CertificateStoreCTRL.StoreType = CertificateStoreType.Directory;
            CertificateStoreCTRL.StorePath = null;

            if (store != null)
            {
                CertificateStoreCTRL.StoreType = store.StoreType;
                CertificateStoreCTRL.StorePath = store.StorePath;
            }

            if (ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            store = new CertificateStoreIdentifier();
            store.StoreType = CertificateStoreCTRL.StoreType;
            store.StorePath = CertificateStoreCTRL.StorePath;
            return store;
        }

        private void OkBTN_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.OK;
            }
            catch (Exception exception)
            {
                GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void ViewBTN_Click(object sender, EventArgs e)
        {
            try
            {
                CertificateStoreIdentifier store = new CertificateStoreIdentifier();
                store.StoreType = CertificateStoreCTRL.StoreType;
                store.StorePath = CertificateStoreCTRL.StorePath;
                new CertificateListDlg().ShowDialog(store, false);
            }
            catch (Exception exception)
            {
                GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }
    }
}
