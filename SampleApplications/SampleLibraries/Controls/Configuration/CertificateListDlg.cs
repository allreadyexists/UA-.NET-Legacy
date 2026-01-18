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
using System.IO;

namespace Opc.Ua.Client.Controls
{
    /// <summary>
    /// Displays a list of certificates.
    /// </summary>
    public partial class CertificateListDlg : Form
    {
        /// <summary>
        /// Contructs the object.
        /// </summary>
        public CertificateListDlg()
        {
            InitializeComponent();
            this.Icon = ClientUtils.GetAppIcon();
        }

        /// <summary>
        /// Displays the dialog.
        /// </summary>
        public CertificateIdentifier ShowDialog(CertificateStoreIdentifier store, bool allowStoreChange)
        {
            CertificateStoreCTRL.StoreType = CertificateStoreType.Directory;
            CertificateStoreCTRL.StorePath = String.Empty;
            CertificateStoreCTRL.ReadOnly = !allowStoreChange;
            CertificatesCTRL.Initialize(null);
            OkBTN.Enabled = false;

            if (store != null)
            {
                CertificateStoreCTRL.StoreType = store.StoreType;
                CertificateStoreCTRL.StorePath = store.StorePath;
            }

            if (ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            CertificateIdentifier id = new CertificateIdentifier();
            id.StoreType = CertificateStoreCTRL.StoreType;
            id.StorePath = CertificateStoreCTRL.StorePath;
            id.Certificate = CertificatesCTRL.SelectedCertificate;
            return id;
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

        private void FilterBTN_Click(object sender, EventArgs e)
        {
            try
            {
                CertificateListFilter filter = new CertificateListFilter();
                filter.SubjectName = SubjectNameTB.Text.Trim();
                filter.IssuerName = IssuerNameTB.Text.Trim();
                filter.Domain = DomainTB.Text.Trim();
                filter.PrivateKey = PrivateKeyCK.Checked;

                List<CertificateListFilterType> types = new List<CertificateListFilterType>();

                if (ApplicationCK.Checked)
                {
                    types.Add(CertificateListFilterType.Application);
                }

                if (CaCK.Checked)
                {
                    types.Add(CertificateListFilterType.CA);
                }

                if (SelfSignedCK.Checked)
                {
                    types.Add(CertificateListFilterType.SelfSigned);
                }

                if (IssuedCK.Checked)
                {
                    types.Add(CertificateListFilterType.Issued);
                }

                if (types.Count > 0)
                {
                    filter.CertificateTypes = types.ToArray();
                }

                CertificatesCTRL.SetFilter(filter);
            }
            catch (Exception exception)
            {
                GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void CertificatesCTRL_ItemsSelected(object sender, ListItemActionEventArgs e)
        {
            try
            {
                OkBTN.Enabled = e.Items.Count == 1;
            }
            catch (Exception exception)
            {
                GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void CertificateStoreCTRL_StoreChanged(object sender, EventArgs e)
        {
            try
            {
                CertificateStoreIdentifier store = new CertificateStoreIdentifier();
                store.StoreType = CertificateStoreCTRL.StoreType;
                store.StorePath = CertificateStoreCTRL.StorePath;
                CertificatesCTRL.Initialize(store, null);

                if (!CertificatesCTRL.IsEmptyStore)
                {
                    Utils.UpdateRecentFileList("CertificateStores:" + store.StoreType, store.StorePath, 16);
                }

                FilterBTN_Click(sender, e);
            }
            catch (Exception exception)
            {
                GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }
    }
}
