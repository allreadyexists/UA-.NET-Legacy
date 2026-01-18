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
    public partial class SelectNodesDlg : Form
    {
        #region Constructors
        public SelectNodesDlg()
        {
            InitializeComponent();
            this.Icon = ClientUtils.GetAppIcon();
        }
        #endregion

        #region Private Fields
        private Session m_session;
        #endregion
        
        #region Public Interface
        /// <summary>
        /// Displays the dialog.
        /// </summary>
        public NodeIdCollection ShowDialog(
            Session          session, 
            BrowseViewType   browseView, 
            NodeIdCollection nodesIds,
            NodeClass        nodeClassMask)
        {
            if (session == null) throw new ArgumentNullException("session");

            m_session = session;

            BrowseCTRL.SetView(session, browseView, null);
            NodeListCTRL.Initialize(session, nodesIds, nodeClassMask);
            
            if (ShowDialog() != DialogResult.OK)
            {
                return null;
            }
                        
            return NodeListCTRL.GetNodeIds();
        }
        #endregion
        
        #region Private Methods
        #endregion
        
        #region Event Handler
        private void BrowseCTRL_NodesSelected(object sender, NodesSelectedEventArgs e)
        {
            try
            {
                foreach (ReferenceDescription reference in e.References)
                {
                    NodeListCTRL.AddNodeId(reference);
                }    
            }
            catch (Exception exception)
            {
                GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }
        #endregion
    }
}
