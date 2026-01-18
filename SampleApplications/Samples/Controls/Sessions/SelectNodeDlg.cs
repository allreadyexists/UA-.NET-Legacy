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
using System.Security.Cryptography.X509Certificates;

using Opc.Ua.Client;
using Opc.Ua.Client.Controls;

namespace Opc.Ua.Sample.Controls
{
    public partial class SelectNodeDlg : Form
    {
        #region Constructors
        public SelectNodeDlg()
        {
            InitializeComponent();
            this.Icon = ClientUtils.GetAppIcon();

            foreach (IdType idType in Enum.GetValues(typeof(IdType)))
            {
                IdentifierTypeCB.Items.Add(idType);
            }

            foreach (NodeClass nodeClass in Enum.GetValues(typeof(NodeClass)))
            {
                NodeClassCB.Items.Add(nodeClass);
            }
        }
        #endregion

        #region Private Fields
        private ReferenceDescription m_reference;
        #endregion
        
        #region Public Interface
        /// <summary>
        /// Displays the dialog.
        /// </summary>
        public ReferenceDescription ShowDialog(Browser browser, NodeId rootId)
        {
            if (browser == null) throw new ArgumentNullException("browser");

            BrowseCTRL.SetRoot(browser, rootId);

            NamespaceUriCB.Items.Clear();
            NamespaceUriCB.Items.AddRange(browser.Session.NamespaceUris.ToArray());
            
            OkBTN.Enabled = false;

            if (ShowDialog() != DialogResult.OK)
            {
                return null;
            }
            
            return m_reference;
        }
        #endregion

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

        private void NamespaceUriCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_reference = null;
            OkBTN.Enabled = false;
        }

        private void NamespaceUriCB_TextChanged(object sender, EventArgs e)
        {
            m_reference = null;
            OkBTN.Enabled = false;
        }

        private void NodeIdentifierTB_TextChanged(object sender, EventArgs e)
        {
            m_reference = null;
            OkBTN.Enabled = false;
        }

        private void IdentifierTypeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_reference = null;
            OkBTN.Enabled = false;
        }

        private void BrowseCTRL_NodeSelected(object sender, TreeNodeActionEventArgs e)
        {
            try
            {
                // disable ok button if selection is not valid.
                OkBTN.Enabled = false;

                ReferenceDescription reference = e.Node as ReferenceDescription;
                
                if (reference == null)
                {
                    return;
                }

                if (NodeId.IsNull(reference.NodeId))
                {
                    return;
                }

                // set the display name.
                DisplayNameTB.Text       = reference.ToString();
                NodeClassCB.SelectedItem = (NodeClass)reference.NodeClass;
                
                // set identifier type.
                IdentifierTypeCB.SelectedItem = reference.NodeId.IdType;

                // set namespace uri.
                if (!String.IsNullOrEmpty(reference.NodeId.NamespaceUri))
                {
                    NamespaceUriCB.SelectedIndex = -1;
                    NamespaceUriCB.Text = reference.NodeId.NamespaceUri;
                }
                else
                {
                    if (reference.NodeId.NamespaceIndex < NamespaceUriCB.Items.Count)
                    {
                        NamespaceUriCB.SelectedIndex = (int)reference.NodeId.NamespaceIndex;
                    }
                    else
                    {
                        NamespaceUriCB.SelectedIndex = -1;
                        NamespaceUriCB.Text = null;
                    }
                }
                
                // set identifier.
                switch (reference.NodeId.IdType)
                {
                    case IdType.Opaque:
                    {
                        NodeIdentifierTB.Text = Convert.ToBase64String((byte[])reference.NodeId.Identifier);
                        break;
                    }

                    default:
                    {
                        NodeIdentifierTB.Text = Utils.Format("{0}", reference.NodeId.Identifier);
                        break;
                    }
                }

                // selection valid - enable ok.
                OkBTN.Enabled = true;
                m_reference = reference;
            }
            catch (Exception exception)
            {
                GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }
    }
}
