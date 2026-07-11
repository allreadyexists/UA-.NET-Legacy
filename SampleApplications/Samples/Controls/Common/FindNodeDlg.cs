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

using Opc.Ua.Client;
using Opc.Ua.Client.Controls;

namespace Opc.Ua.Sample.Controls
{
    public partial class FindNodeDlg : Form
    {
        public FindNodeDlg()
        {
            InitializeComponent();
            this.Icon = ClientUtils.GetAppIcon();
        }

        private Session m_session;

        /// <summary>
        /// Displays the dialog.
        /// </summary>
        public NodeIdCollection ShowDialog(Session session, NodeId startNodeId)
        {
            m_session = session;

            StartNode.Text    = String.Format("{0}", startNodeId);
            RelativePath.Text = null;

            if (ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            return null;
        }

        private void OkBTN_Click(object sender, EventArgs e)
        {
            try
            {                
                BrowsePathCollection browsePaths = new BrowsePathCollection();
                
                BrowsePath browsePath = new BrowsePath();

                browsePath.StartingNode = NodeId.Parse(StartNode.Text);
                browsePath.RelativePath = Opc.Ua.RelativePath.Parse(RelativePath.Text, m_session.TypeTree);
                
                browsePaths.Add(browsePath);

                BrowsePathResultCollection results = null;
                DiagnosticInfoCollection diagnosticInfos = null;

                m_session.TranslateBrowsePathsToNodeIds(
                    null,
                    browsePaths,
                    out results,
                    out diagnosticInfos);

                if (results != null && results.Count == 1)
                {
                    // NodesCTRL.SetNodeList(results[0].MatchingNodeIds);
                }    
            }
            catch (Exception exception)
            {
				GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }
    }
}
