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
using Opc.Ua;

namespace Quickstarts.ComDataAccessServer
{
    /// <summary>
    /// Prompts the user to accept or reject an untrusted certificate.
    /// </summary>
    public partial class AcceptCertificateDlg : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AcceptCertificateDlg"/> class.
        /// </summary>
        public AcceptCertificateDlg()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Shows the dialog.
        /// </summary>
        /// <param name="e">The <see cref="Opc.Ua.CertificateValidationEventArgs"/> instance containing the event data.</param>
        /// <returns>True if the certificate was accepted.</returns>
        public AcceptCertificateOptions ShowDialog(CertificateValidationEventArgs e)
        {
            SubjectTB.Text = Utils.Format("{0}", e.Certificate.Subject);
            IssuerTB.Text = Utils.Format("{0}", (e.Certificate.Subject == e.Certificate.Issuer)?"Self-signed":e.Certificate.Issuer);
            ValidFromTB.Text = Utils.Format("{0:yyyy-MM-dd}", e.Certificate.NotBefore);
            ValidToTB.Text = Utils.Format("{0:yyyy-MM-dd}", e.Certificate.NotAfter);
            ThumbprintTB.Text = Utils.Format("{0}", e.Certificate.Thumbprint);

            AlwaysAcceptAllRB.Enabled = false;
            AlwaysAcceptSingleRB.Enabled = false;
            AlwaysRejectRB.Enabled = false;
            OneTimeAcceptRB.Checked = true;

            if (ShowDialog() != DialogResult.OK)
            {
                return AcceptCertificateOptions.RejectOnce;
            }

            if (OneTimeAcceptRB.Checked)
            {
                return AcceptCertificateOptions.AcceptOnceForCurrent;
            }

            return AcceptCertificateOptions.RejectOnce;
        }
    }

    /// <summary>
    /// The available choices when accepting a certificate.
    /// </summary>
    public enum AcceptCertificateOptions
    {
        /// <summary>
        /// The certificate should be rejected this time.
        /// </summary>
        RejectOnce,

        /// <summary>
        /// The certificate should be accepted once for the current server.
        /// </summary>
        AcceptOnceForCurrent,

        /// <summary>
        /// The certificate should be accepted always for the current server.
        /// </summary>
        AcceptAlwaysForCurrent,

        /// <summary>
        /// The certificate should be accepted always for all servers on the machine.
        /// </summary>
        AcceptAlwaysForAll,

        /// <summary>
        /// The certificate should be rejected always.
        /// </summary>
        RejectAlways
    }
}
