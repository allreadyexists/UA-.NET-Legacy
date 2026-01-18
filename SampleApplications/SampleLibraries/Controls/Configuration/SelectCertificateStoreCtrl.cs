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
    /// A control with button that displays a select certificate store dialog.
    /// </summary>
    public partial class SelectCertificateStoreCtrl : UserControl
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of the control.
        /// </summary>
        public SelectCertificateStoreCtrl()
        {
            InitializeComponent();
        }
        #endregion
        
        #region Private Fields
        private event EventHandler m_CertificateStoreSelected;
        #endregion
        
        #region Public Interface
        /// <summary>
        /// Gets or sets the control that is stores with the current certificate store.
        /// </summary>
        public Control CertificateStoreControl { get; set; }

        /// <summary>
        /// Raised when a new file is selected.
        /// </summary>
        public event EventHandler CertificateStoreSelected
        {
            add { m_CertificateStoreSelected += value; }
            remove { m_CertificateStoreSelected -= value; }
        }
        #endregion

        #region Event Handlers
        private void BrowseBTN_Click(object sender, EventArgs e)
        {
            CertificateStoreIdentifier store = new CertificateStoreIdentifier();
            store.StoreType = CertificateStoreIdentifier.DetermineStoreType(CertificateStoreControl.Text);
            store.StorePath = CertificateStoreControl.Text;

            store = new CertificateStoreDlg().ShowDialog(store);

            if (store == null)
            {
                return;
            }

            CertificateStoreControl.Text = store.StorePath;

            if (m_CertificateStoreSelected != null)
            {
                m_CertificateStoreSelected(this, new EventArgs());
            }
        }
        #endregion
    }
}
