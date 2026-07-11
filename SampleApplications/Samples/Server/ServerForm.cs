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
using System.Runtime.InteropServices;
using Opc.Ua;
using Opc.Ua.Server;
using Opc.Ua.Configuration;
using System.IO;

namespace Opc.Ua.Sample
{
    /// <summary>
    /// The primary form displayed by the application.
    /// </summary>
    public partial class ServerForm : Form
    {
        #region Constructors
        /// <summary>
        /// Creates an empty form.
        /// </summary>
        public ServerForm()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Creates a form which displays the status for a UA server.
        /// </summary>
        public ServerForm(ApplicationInstance application)
        {
            InitializeComponent();

            m_application = application;

            if (application.Server is StandardServer)
            {
                this.ServerDiagnosticsCTRL.Initialize((StandardServer)application.Server, application.ApplicationConfiguration);
            }

            if (!application.ApplicationConfiguration.SecurityConfiguration.AutoAcceptUntrustedCertificates)
            {
                application.ApplicationConfiguration.CertificateValidator.CertificateValidation += new CertificateValidationEventHandler(CertificateValidator_CertificateValidation);
            }

            TrayIcon.Text = this.Text = application.ApplicationName;
            TrayIcon.Icon = GetAppIcon();
        }
        #endregion

        #region Private Fields
        private ApplicationInstance m_application;
        #endregion

        #region Private Methods
        private static class NativeMethods 
        { 
            [DllImport("user32.dll", CharSet = CharSet.Unicode)] 
            static extern internal IntPtr LoadIcon(IntPtr hInstance, string lpIconName); 

            [DllImport("kernel32.dll", CharSet = CharSet.Unicode)] 
            static extern internal IntPtr LoadLibrary(string lpFileName); 
        }

        /// <summary>
        /// Gets the application icon.
        /// </summary>
        static Icon GetAppIcon() 
        { 
            string fileName = Assembly.GetEntryAssembly().Location;
            IntPtr hLibrary = NativeMethods.LoadLibrary(fileName);

            if (hLibrary != IntPtr.Zero) 
            { 
                IntPtr hIcon = NativeMethods.LoadIcon(hLibrary, "#32512");

                if (hIcon != IntPtr.Zero) 
                { 
                    return Icon.FromHandle(hIcon); 
                }
            }

            return null;
        } 
        #endregion

        #region Event Handlers
        /// <summary>
        /// Handles a certificate validation error.
        /// </summary>
        void CertificateValidator_CertificateValidation(CertificateValidator validator, CertificateValidationEventArgs e)
        {
            try
            {
                Opc.Ua.Client.Controls.GuiUtils.HandleCertificateValidationError(this, validator, e);
            }
            catch (Exception exception)
            {
                Opc.Ua.Client.Controls.GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void ServerForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                Hide();
            }
        }

        private void TrayIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void Server_ExitMI_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ServerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                m_application.Stop();
            }
            catch (Exception exception)
            {
                Utils.Trace(exception, "Error stopping server.");
            }
        }

        private void TrayIcon_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                StandardServer server = m_application.Server as StandardServer;

                if (server != null)
                {
                    TrayIcon.Text = String.Format(
                        "{0} [{1} {2:HH:mm:ss}]", 
                        m_application.ApplicationName,
                        server.CurrentInstance.CurrentState,
                        DateTime.Now);
                }
            }
            catch (Exception exception)
            {
                Utils.Trace(exception, "Error getting server status.");
            }
        }
        #endregion

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Quit this application", "Generic Server", MessageBoxButtons.YesNoCancel)== DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start( Path.GetDirectoryName(Application.ExecutablePath) + "\\WebHelp\\ua_sample_server.htm");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to launch help documentation. Error: " + ex.Message);
            }
        }
    }
}
