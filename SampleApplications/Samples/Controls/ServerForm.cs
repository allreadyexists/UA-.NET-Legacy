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
using System.Reflection;

using Opc.Ua.Server;
using Opc.Ua.Client.Controls;

namespace Opc.Ua.Sample.Controls
{
    public partial class ServerForm : Form
    {
        #region Constructors
        public ServerForm(StandardServer server, ApplicationConfiguration configuration)
        {
            InitializeComponent();
            this.Icon = this.TrayIcon.Icon = ClientUtils.GetAppIcon();
                        
            GuiUtils.DisplayUaTcpImplementation(this, configuration);

            m_server = server;

            if (!configuration.SecurityConfiguration.AutoAcceptUntrustedCertificates)
            {
                m_server.CertificateValidator.CertificateValidation += new CertificateValidationEventHandler(CertificateValidator_CertificateValidation);
            }
        }
        #endregion
        
        #region Private Fields
        private bool m_exit;
        private StandardServer m_server;
        #endregion
        
        #region Private Methods
        /// <summary>
        /// Shows the diagnostics window and starts the update timer.
        /// </summary>
        private void ShowStatus()
        {            
            this.WindowState = FormWindowState.Normal;
            this.BringToFront();
            Timer.Enabled = true;
        }
        
        /// <summary>
        /// Hides the diagnostics window and starts the update timer.
        /// </summary>
        private void HideStatus()
        {            
            this.WindowState = FormWindowState.Minimized;
            Timer.Enabled = false;
        }
        
		/// <summary>
		/// Displays an unhandled exception.
		/// </summary>
		public static void HandleException(string caption, MethodBase method, Exception e)
		{
            if (String.IsNullOrEmpty(caption))
            {
                caption = method.Name;
            }

			MessageBox.Show(e.Message, caption);
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

        private void ExitMI_Click(object sender, EventArgs e)
        {
            m_exit = true;
            Close();
        }

        private void TrayIcon_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                ShowStatus();          
            }
            catch (Exception exception)
            {
				HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (e.CloseReason == CloseReason.UserClosing && !m_exit)
                {
                    e.Cancel = true;
                    HideStatus();
                }       
            }
            catch (Exception exception)
            {
				HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void ShowMI_Click(object sender, EventArgs e)
        {
            try
            {
                ShowStatus();          
            }
            catch (Exception exception)
            {
				HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                ServerStatusDataType status = m_server.GetStatus();

                StartTimeTB.Text   = Utils.Format("{0:HH:mm:ss.ff}", status.StartTime.ToLocalTime());
                CurrentTimeTB.Text = Utils.Format("{0:HH:mm:ss.ff}", status.CurrentTime.ToLocalTime());
                ServerStateTB.Text = Utils.Format("{0}", status.State);
            }
            catch (Exception exception)
            {
				HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }
        #endregion
    }
}
