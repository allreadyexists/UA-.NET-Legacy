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
    /// Prompts the user to edit a ApplicationDescription.
    /// </summary>
    public partial class CertificateDlg : Form
    {
        /// <summary>
        /// Contructs the object.
        /// </summary>
        public CertificateDlg()
        {
            InitializeComponent();
            this.Icon = ClientUtils.GetAppIcon();

            PrivateKeyCB.Items.Add("No");
            PrivateKeyCB.Items.Add("Yes");
            PrivateKeyCB.SelectedIndex = 0;
        }

        /// <summary>
        /// Displays the dialog.
        /// </summary>
        public bool ShowDialog(CertificateIdentifier certificateIdentifier)
        {
            CertificateStoreCTRL.StoreType = null;
            CertificateStoreCTRL.StorePath = null;
            PrivateKeyCB.SelectedIndex = 0;
            PropertiesCTRL.Initialize((X509Certificate2)null);

            if (certificateIdentifier != null)
            {
                X509Certificate2 certificate = certificateIdentifier.Find();

                CertificateStoreCTRL.StoreType = certificateIdentifier.StoreType;
                CertificateStoreCTRL.StorePath = certificateIdentifier.StorePath;

                if (certificate != null && certificateIdentifier.Find(true) != null)
                {
                    PrivateKeyCB.SelectedIndex = 1;
                }
                else
                {
                    PrivateKeyCB.SelectedIndex = 0;
                }

                PropertiesCTRL.Initialize(certificate);
            }

            if (ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// Displays the dialog.
        /// </summary>
        public bool ShowDialog(X509Certificate2 certificate)
        {
            CertificateStoreCTRL.StoreType = null;
            CertificateStoreCTRL.StorePath = null;
            PrivateKeyCB.SelectedIndex = 0;
            PropertiesCTRL.Initialize(certificate);

            if (ShowDialog() != DialogResult.OK)
            {
                return false;
            }
  
            return true;
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
    }
}
