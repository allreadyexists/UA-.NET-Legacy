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
using Opc.Ua;
using Opc.Ua.Server;

namespace Quickstarts.ComDataAccessServer
{
    /// <summary>
    /// The primary form displayed by the application.
    /// </summary>
    public partial class MainForm : Form
    {
        #region Constructors
        /// <summary>
        /// Creates an empty form.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Creates a form which displays the status for a UA server.
        /// </summary>
        /// <param name="server">The server displayed in the form.</param>
        /// <param name="configuration">The configuration used to initialize the server.</param>
        public MainForm(ComDaServerWrapper server, ApplicationConfiguration configuration)
        {
            InitializeComponent();
            this.Text = configuration.ApplicationName;
            ServerDiagnosticsCTRL.Initialize(server, configuration);

            m_server = server;
            m_configuration = configuration;
            m_configuration.CertificateValidator.CertificateValidation += new CertificateValidationEventHandler(CertificateValidator_CertificateValidation);
        }

        /// <summary>
        /// Called when a certificate cannot be validated.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Opc.Ua.CertificateValidationEventArgs"/> instance containing the event data.</param>
        void CertificateValidator_CertificateValidation(CertificateValidator sender, CertificateValidationEventArgs e)
        {
            // need to dispatch to the main UI thread.
            if (InvokeRequired)
            {
                Invoke(new CertificateValidationEventHandler(CertificateValidator_CertificateValidation), sender, e);
                return;
            }

            try
            {
                e.Accept = true;

                /*
                // prompt user.
                switch (new AcceptCertificateDlg().ShowDialog(e))
                {
                    case AcceptCertificateOptions.AcceptAlwaysForAll:
                    case AcceptCertificateOptions.AcceptAlwaysForCurrent:
                    case AcceptCertificateOptions.AcceptOnceForCurrent:
                    {
                        // TBD - update configuration to make the acceptance permenent.
                        e.Accept = true;
                        break;
                    }

                    case AcceptCertificateOptions.RejectAlways:
                    {
                        // TBD - update configuration to make the rejection permenent.
                        e.Accept = false;
                        break;
                    }
                }
                */
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
        #endregion
        
        #region Private Fields
        private ComDaServerWrapper m_server;
        private ApplicationConfiguration m_configuration;
        #endregion
        
        #region Event Handlers

        private void Config_SelectServerMI_Click(object sender, EventArgs e)
        {
            try
            {
                if (!new SelectComServerDlg().ShowDialogForUpdate(m_configuration))
                {
                    return;
                }

                m_configuration.SaveToFile(m_configuration.SourceFilePath);

                m_server.Stop();
                m_server = new ComDaServerWrapper();
                m_server.Start(m_configuration);

                ServerDiagnosticsCTRL.Initialize(m_server, m_configuration);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void Config_DeleteServerMI_Click(object sender, EventArgs e)
        {
            try
            {
                if (!new SelectComServerDlg().ShowDialogForDelete(m_configuration))
                {
                    return;
                }

                m_configuration.SaveToFile(m_configuration.SourceFilePath);

                m_server.Stop();
                m_server = new ComDaServerWrapper();
                m_server.Start(m_configuration);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (m_server != null)
                {
                    m_server.Stop();
                    m_server = null;
                }
            }
            catch (Exception exception)
            {
                Utils.Trace("Error stopping server application. '{0}'.", exception.Message);
            }
        }
        #endregion
    }
}
