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
using System.Security.Principal;
using CubicOrange.Windows.Forms.ActiveDirectory;

using Opc.Ua.Client.Controls;
using Opc.Ua.Configuration;

namespace Opc.Ua.Configuration
{
    /// <summary>
    /// Prompts the user to specify a new access rule for a file.
    /// </summary>
    public partial class FirewallAccessDlg : Form
    {
        #region Constructors
        /// <summary>
        /// Initializes the dialog.
        /// </summary>
        public FirewallAccessDlg()
        {
            InitializeComponent();
            ApplicationAccessGrantedCK_CheckedChanged(this, null);
        }
        #endregion
        
        #region Private Fields
        ManagedApplication m_application;
        #endregion
        
        #region Public Interface
        /// <summary>
        /// Displays the dialog.
        /// </summary>
        public bool ShowDialog(ManagedApplication application)
        {
            m_application = application;
            
            if (m_application != null)
            {
                ApplicationAccessGrantedCK.Checked = ConfigUtils.CheckFirewallAccess(m_application.ExecutablePath, null);
            }

            UpdatePorts();

            if (ShowDialog() != DialogResult.OK)
            {
                return false;
            }
                
            return true;
        }
        #endregion

        #region Event Handlers
        private void UpdatePorts()
        {
            PortsLV.Items.Clear();

            if (m_application != null)
            {
                int[] ports = ConfigUtils.GetFirewallAccess(m_application.ExecutablePath);

                if (ports != null)
                {
                    for (int ii = 0; ii < ports.Length; ii++)
                    {
                        ListViewItem item = new ListViewItem(ports[ii].ToString());
                        item.Tag = ports[ii];
                        PortsLV.Items.Add(item);
                    }
                }
            }

            PortsChanged();
        }

        private void PortsChanged()
        {
            List<CheckBox> portsInUse = new List<CheckBox>();
            PortsPN.Controls.Clear();

            if (m_application != null && m_application.BaseAddresses != null)
            {
                foreach (string baseAddresses in m_application.BaseAddresses)
                {
                    Uri url = Utils.ParseUri(baseAddresses);

                    if (url != null)
                    {
                        CheckBox box = new CheckBox();
                        box.Text = url.Port.ToString();
                        box.Tag = url.Port;
                        portsInUse.Add(box);
                        
                        bool found = false;

                        for (int ii = 0; ii < PortsLV.Items.Count; ii++)
                        {
                            int? port = PortsLV.Items[ii].Tag as int?;

                            if (port != null)
                            {
                                for (int jj = 0; jj < portsInUse.Count; jj++)
                                {
                                    if ((int)portsInUse[jj].Tag == port)
                                    {
                                        portsInUse[jj].Checked = true;
                                        found = true;
                                        break;
                                    }
                                }
                            }

                            if (found)
                            {
                                break;
                            }
                        }

                        box.CheckedChanged += new EventHandler(Port_CheckedChanged);
                        PortsPN.Controls.Add(box);
                    }
                }
            }
        }

        void Port_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox box = (CheckBox)sender;

                if (box.Checked)
                {
                    ConfigUtils.SetFirewallAccess(m_application.ExecutablePath, (int)box.Tag);
                }
                else
                {
                    ConfigUtils.RemoveFirewallAccess((int)box.Tag);
                }

                UpdatePorts();
            }
            catch (Exception exception)
            {
                GuiUtils.HandleException(this.Text, System.Reflection.MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void AddTN_Click(object sender, EventArgs e)
        {
            try
            {
                int port = new NewPortDlg().ShowDialog(0);

                if (port > 0)
                {
                    ConfigUtils.SetFirewallAccess(m_application.ExecutablePath, port);
                    UpdatePorts();
                }
            }
            catch (Exception exception)
            {
                GuiUtils.HandleException(this.Text, System.Reflection.MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void RemoveBTN_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> portsToRemove = new List<int>();
                List<ListViewItem> itemsToRemove = new List<ListViewItem>();
                    
                for (int ii = 0; ii < PortsLV.SelectedItems.Count; ii++)
                {
                    int? port = PortsLV.SelectedItems[ii].Tag as int?;

                    if (port != null)
                    {
                        portsToRemove.Add(port.Value);
                        itemsToRemove.Add(PortsLV.SelectedItems[ii]);
                    }
                }

                if (portsToRemove.Count > 0)
                {
                    ConfigUtils.RemoveFirewallAccess(portsToRemove.ToArray());
                    UpdatePorts();
                }
            }
            catch (Exception exception)
            {
                GuiUtils.HandleException(this.Text, System.Reflection.MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void ApplicationAccessGrantedCK_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_application != null)
                {
                    bool accessible = ConfigUtils.CheckFirewallAccess(m_application.ExecutablePath, null);

                    if (ApplicationAccessGrantedCK.Checked)
                    {
                        if (!accessible)
                        {
                            ConfigUtils.SetFirewallAccess(m_application.ExecutablePath, null);
                        }
                    }
                    else
                    {
                        if (accessible)
                        {
                            ConfigUtils.RemoveFirewallAccess(m_application.ExecutablePath, null);
                        }
                    }
                }
                
                PortsLV.Enabled = ApplicationAccessGrantedCK.Checked;
                AddBTN.Enabled = PortsLV.Enabled;
                RemoveBTN.Enabled = PortsLV.Enabled;
            }
            catch (Exception exception)
            {
                GuiUtils.HandleException(this.Text, System.Reflection.MethodBase.GetCurrentMethod(), exception);
            }
        }
        #endregion
    }
}
