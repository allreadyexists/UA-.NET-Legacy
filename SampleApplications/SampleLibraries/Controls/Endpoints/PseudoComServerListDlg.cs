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
using System.IO;

using Opc.Ua.Configuration;

namespace Opc.Ua.Client.Controls
{
    /// <summary>
    /// Allows the user to browse a list of servers.
    /// </summary>
    public partial class PseudoComServerListDlg : Form
    {
        #region Constructors
        /// <summary>
        /// Initializes the dialog.
        /// </summary>
        public PseudoComServerListDlg()
        {
            InitializeComponent();
            this.Icon = ClientUtils.GetAppIcon();
        }
        #endregion
        
        #region Private Fields
        private ConfiguredEndpoint m_endpoint;
        private ApplicationConfiguration m_configuration;
        private string m_exportFile;
        #endregion
        
        #region Public Interface
        /// <summary>
        /// Displays the dialog.
        /// </summary>
        public ConfiguredEndpoint ShowDialog(ApplicationConfiguration configuration)
        {
            m_configuration = configuration;
            m_endpoint = null;

            ServersCTRL.Initialize(configuration);
            
            OkBTN.Enabled = false;
            ButtonsPN.Visible = true;
            
            if (ShowDialog() != DialogResult.OK)
            {
                return null;
            }
  
            return m_endpoint;
        }

        /// <summary>
        /// Displays the form.
        /// </summary>
        public void Show(ApplicationConfiguration configuration)
        {
            m_configuration = configuration;
            m_endpoint = null;

            ServersCTRL.Initialize(configuration);
            
            ButtonsPN.Visible = false;

            Show();
        }
        #endregion
        
        #region Event Handlers
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

        private void ServersCTRL_ItemsSelected(object sender, ListItemActionEventArgs e)
        {
            try
            {
                m_endpoint = null;

                foreach (ConfiguredEndpoint server in e.Items)
                {
                    m_endpoint = server;
                    break;
                }

                OkBTN.Enabled = m_endpoint != null;
            }
            catch (Exception exception)
            {
                GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void File_ExportMI_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_exportFile == null)
                {
                    m_exportFile = "ComServers.endpoints.xml";
                }

                FileInfo fileInfo = new FileInfo(m_exportFile);

				SaveFileDialog dialog = new SaveFileDialog();

				dialog.CheckFileExists  = false;
				dialog.CheckPathExists  = true;
				dialog.DefaultExt       = ".xml";
				dialog.Filter          = "Configuration Files (*.xml)|*.xml|All Files (*.*)|*.*";
				dialog.ValidateNames    = true;
				dialog.Title            = "Save Endpoint Configuration File";
				dialog.FileName         = fileInfo.Name;
                dialog.InitialDirectory = fileInfo.DirectoryName;

				if (dialog.ShowDialog() != DialogResult.OK)
				{
					return;
				}

                m_exportFile = dialog.FileName;

                ConfiguredEndpointCollection endpoints = new ConfiguredEndpointCollection(m_configuration);

                foreach (ConfiguredEndpoint endpoint in PseudoComServer.Enumerate())
                {
                    endpoints.Add(endpoint);
                }

                endpoints.Save(m_exportFile);
            }
            catch (Exception exception)
            {
                GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void File_ImportMI_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_exportFile == null)
                {
                    m_exportFile = "ComServers.endpoints.xml";
                }

                FileInfo fileInfo = new FileInfo(Utils.GetAbsoluteFilePath(m_exportFile));

                OpenFileDialog dialog = new OpenFileDialog();

			    dialog.CheckFileExists  = true;
			    dialog.CheckPathExists  = true;
			    dialog.DefaultExt       = ".xml";
			    dialog.Filter           = "Configuration Files (*.xml)|*.xml|All Files (*.*)|*.*";
			    dialog.Multiselect      = false;
			    dialog.ValidateNames    = true;
			    dialog.Title            = "Open Endpoint Configuration File";
			    dialog.FileName         = fileInfo.Name;
                dialog.InitialDirectory = fileInfo.DirectoryName;

			    if (dialog.ShowDialog() != DialogResult.OK)
			    {
				    return;
			    }

                m_exportFile = dialog.FileName;

                // load the endpoints from the file.
                ConfiguredEndpointCollection endpoints = ConfiguredEndpointCollection.Load(m_exportFile);

                // update the endpoint configuration.
                StringBuilder buffer = new StringBuilder();

                foreach (ConfiguredEndpoint endpoint in endpoints.Endpoints)
                {
                    if (endpoint.ComIdentity == null)
                    {
                        continue;
                    }

                    try
                    {
                        PseudoComServer.Save(endpoint);
                    }
                    catch (Exception exception)
                    {
                        if (buffer.Length > 0)
                        {
                            buffer.Append("\r\n");
                        }

                        buffer.AppendFormat(
                            "Error Registering COM pseudo-server '{0}': {1}", 
                            endpoint.ComIdentity.ProgId, 
                            exception.Message);
                    }
                }

                // display warning.
                if (buffer.Length > 0)
                {
                    MessageBox.Show(
                        buffer.ToString(), 
                        "Endpoint Import Errors", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Warning);
                }

                ServersCTRL.Initialize(m_configuration);
            }
            catch (Exception exception)
            {
                GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void File_ExitMI_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Modal)
                {
                    DialogResult = DialogResult.OK;
                    return;
                }
                    
                Close();
            }
            catch (Exception exception)
            {
                GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }

        }

        private void View_RefreshMI_Click(object sender, EventArgs e)
        {
            try
            {
                ServersCTRL.Initialize(m_configuration);
            }
            catch (Exception exception)
            {
                GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void Help_AboutMI_Click(object sender, EventArgs e)
        {
            try
            {
                // TBD
            }
            catch (Exception exception)
            {
                GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }
        #endregion
    }
}
