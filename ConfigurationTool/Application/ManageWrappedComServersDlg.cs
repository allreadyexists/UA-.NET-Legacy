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
using System.IO;
using Opc.Ua;
using Opc.Ua.Com;
using Opc.Ua.Com.Client;
using Opc.Ua.Client.Controls;

namespace Opc.Ua.Configuration
{
    /// <summary>
    /// Prompts the user to select a COM server to wrap.
    /// </summary>
    public partial class ManageWrappedComServersDlg : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManageWrappedComServersDlg"/> class.
        /// </summary>
        public ManageWrappedComServersDlg()
        {
            InitializeComponent();
            ApplicationToManageCB_DropDown(this, null);

            if (ApplicationToManageCB.Items.Count > 0)
            {
                ApplicationToManageCB.SelectedIndex = 0;
            }
        }

        private string m_currentDirectory;
        private ApplicationConfiguration m_configuration;

        private void UpdateServers()
        {
            ServersLV.Items.Clear();

            // extract the wrapper configuration from the application configuration.
            ComWrapperServerConfiguration wrapperConfig = m_configuration.ParseExtension<ComWrapperServerConfiguration>();

            if (wrapperConfig == null)
            {
                wrapperConfig = new ComWrapperServerConfiguration();
            }

            if (wrapperConfig.WrappedServers == null)
            {
                wrapperConfig.WrappedServers = new ComClientConfigurationCollection();
            }

            // populate the list of wrapped servers.
            for (int ii = 0; ii < wrapperConfig.WrappedServers.Count; ii++)
            {
                ComClientConfiguration server = wrapperConfig.WrappedServers[ii];

                ListViewItem item = new ListViewItem(server.ServerName);
                item.SubItems.Add(server.ServerUrl);
                item.Tag = server;

                ServersLV.Items.Add(item);
            }

            for (int ii = 0; ii < ServersLV.Columns.Count; ii++)
            {
                ServersLV.Columns[ii].Width = -2;
            }
        }

        private int FindServer(ComWrapperServerConfiguration configuration, ComClientConfiguration server)
        {
            for (int ii = 0; ii < configuration.WrappedServers.Count; ii++)
            {
                if (server.GetType().Name != configuration.WrappedServers[ii].GetType().Name)
                {
                    continue;
                }

                if (configuration.WrappedServers[ii].ServerUrl == server.ServerUrl)
                {
                    return ii;
                }
            }

            return -1;
        }
        
        private void AddBTN_Click(object sender, EventArgs e)
        {
            try
            {
                if (new SelectComServerDlg().ShowDialog(m_configuration))
                {
                    m_configuration.SaveToFile(m_configuration.SourceFilePath);
                    UpdateServers();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void RemoveBTN_Click(object sender, EventArgs e)
        {
            try
            {
                if (ServersLV.SelectedItems.Count != 1)
                {
                    return;
                }
                
                // extract the wrapper configuration from the application configuration.
                ComWrapperServerConfiguration wrapperConfig = m_configuration.ParseExtension<ComWrapperServerConfiguration>();

                if (wrapperConfig == null)
                {
                    wrapperConfig = new ComWrapperServerConfiguration();
                }

                if (wrapperConfig.WrappedServers == null)
                {
                    wrapperConfig.WrappedServers = new ComClientConfigurationCollection();
                }

                // get the selected server.
                int index = FindServer(wrapperConfig, (ComClientConfiguration)ServersLV.SelectedItems[0].Tag);

                if (index < 0)
                {
                    return;
                }

                // update the configuration.
                wrapperConfig.WrappedServers.RemoveAt(index);
                m_configuration.UpdateExtension<ComWrapperServerConfiguration>(null, wrapperConfig);
                m_configuration.SaveToFile(m_configuration.SourceFilePath);
                ServersLV.SelectedItems[0].Remove();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void NewApplicationBTN_Click(object sender, EventArgs e)
        {
            try
            {
                // set current directory.
                if (m_currentDirectory == null)
                {
                    m_currentDirectory = Utils.GetAbsoluteDirectoryPath("%ProgramFiles%", false, false);
                }

                // open file dialog.
                OpenFileDialog dialog = new OpenFileDialog();

                dialog.CheckFileExists = true;
                dialog.CheckPathExists = true;
                dialog.DefaultExt = ".exe";
                dialog.Filter = "Executable Files (*.exe)|*.exe|All Files (*.*)|*.*";
                dialog.Multiselect = false;
                dialog.ValidateNames = true;
                dialog.Title = "Select Wrapper Executable File";
                dialog.FileName = null;
                dialog.InitialDirectory = m_currentDirectory;
                dialog.RestoreDirectory = true;

                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                FileInfo executableFile = new FileInfo(dialog.FileName);
                m_currentDirectory = executableFile.Directory.FullName;

                ManagedApplication application = new ManagedApplication();
                application.SetExecutableFile(executableFile.FullName);

                for (int ii = 0; ii < ApplicationToManageCB.Items.Count; ii++)
                {
                    ManagedApplication item = ApplicationToManageCB.Items[ii] as ManagedApplication;

                    if (item != null)
                    {
                        if (String.Compare(item.ExecutablePath, application.ExecutablePath, StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            ApplicationToManageCB.SelectedIndex = ii;
                            return;
                        }
                    }
                }

                ApplicationToManageCB.SelectedIndex = ApplicationToManageCB.Items.Add(application);
            }
            catch (Exception exception)
            {
                GuiUtils.HandleException(this.Text, System.Reflection.MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void ApplicationToManageCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // ensure something is selected.
                if (ApplicationToManageCB.SelectedIndex == -1)
                {
                    if (ApplicationToManageCB.Items.Count > 0)
                    {
                        ApplicationToManageCB.SelectedIndex = 0;
                    }

                    return;
                }

                ServersLV.Items.Clear();

                ManagedApplication application = ApplicationToManageCB.SelectedItem as ManagedApplication;

                if (application == null)
                {
                    return;
                }

                m_configuration = ApplicationConfiguration.Load(new FileInfo(application.ConfigurationPath), ApplicationType.Server, null);
                UpdateServers();
                
                Utils.UpdateRecentFileList("COM Wrappers", application.ExecutablePath, 16);
            }
            catch (Exception exception)
            {
                GuiUtils.HandleException(this.Text, System.Reflection.MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void ApplicationToManageCB_DropDown(object sender, EventArgs e)
        {
            try
            {
                ApplicationToManageCB.Items.Clear();

                List<string> applications = Utils.GetRecentFileList("COM Wrappers");

                for (int ii = 0; ii < applications.Count; ii++)
                {
                    if (File.Exists(applications[ii]))
                    {
                        ManagedApplication application = new ManagedApplication();
                        application.SetExecutableFile(applications[ii]);
                        ApplicationToManageCB.Items.Add(application);
                    }
                }

                if (ApplicationToManageCB.Items.Count == 0)
                {
                    // find the wrapper.
                    string path = Utils.FindInstalledFile("Opc.Ua.ComServerWrapper.exe");

                    if (path != null)
                    {
                        ManagedApplication application = new ManagedApplication();
                        application.SetExecutableFile(path);
                        ApplicationToManageCB.Items.Add(application);
                    }
                }
            }
            catch (Exception exception)
            {
                GuiUtils.HandleException(this.Text, System.Reflection.MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void EditBTN_Click(object sender, EventArgs e)
        {
            try
            {
                if (ServersLV.SelectedItems.Count != 1)
                {
                    return;
                }

                // extract the wrapper configuration from the application configuration.
                ComWrapperServerConfiguration wrapperConfig = m_configuration.ParseExtension<ComWrapperServerConfiguration>();

                if (wrapperConfig == null)
                {
                    return;
                }

                // get the selected server.
                int index = FindServer(wrapperConfig, (ComClientConfiguration)ServersLV.SelectedItems[0].Tag);

                if (index < 0)
                {
                    return;
                }

                // edit server.
                if (new EditComServerDlg().ShowDialog(wrapperConfig.WrappedServers[index]))
                {
                    m_configuration.UpdateExtension<ComWrapperServerConfiguration>(null, wrapperConfig);
                    m_configuration.SaveToFile(m_configuration.SourceFilePath);
                    UpdateServers();
                }

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void AccountsBTN_Click(object sender, EventArgs e)
        {
            try
            {
                new UserNameListForm(m_configuration.ApplicationName).ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
