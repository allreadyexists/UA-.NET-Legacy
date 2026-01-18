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

namespace Opc.Ua.Client.Controls
{
    /// <summary>
    /// A dialog used to selected one or more nodes.
    /// </summary>
    public partial class SelectNodesDlg : Form
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectNodesDlg"/> class.
        /// </summary>
        public SelectNodesDlg()
        {
            InitializeComponent();
            this.Icon = ClientUtils.GetAppIcon();
        }
        #endregion

        #region Private Fields
        #endregion
        
        #region Public Interface
        /// <summary>
        /// Displays the dialog.
        /// </summary>
        public IList<ILocalNode> ShowDialog(Session session, NodeId rootId, IList<NodeId> nodeIds)
        {
            BrowseCTRL.Initialize(session, rootId, null, null, BrowseDirection.Forward);
            ReferencesCTRL.Initialize(session, rootId);
            AttributesCTRL.Initialize(session, rootId);
            NodesCTRL.Initialize(session, nodeIds);

            if (ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            return NodesCTRL.GetNodeList();
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

        private void BrowseCTRL_NodesSelected(object sender, BrowseTreeCtrl.NodesSelectedEventArgs e)
        {
            try
            {
                foreach (ReferenceDescription reference in e.Nodes)
                {
                    if (!reference.NodeId.IsAbsolute)
                    {
                        NodesCTRL.Add((NodeId)reference.NodeId);
                    }
                }
            }
            catch (Exception exception)
            {
                GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }

        }
    }
}
