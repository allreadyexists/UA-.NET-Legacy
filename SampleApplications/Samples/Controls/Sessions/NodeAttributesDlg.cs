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
    public partial class NodeAttributesDlg : Form
    {
        #region Constructors
        public NodeAttributesDlg()
        {
            InitializeComponent();
            this.Icon = ClientUtils.GetAppIcon();
        }
        #endregion

        #region Private Fields
        private Session m_session;
        private ExpandedNodeId m_nodeId;
        #endregion
        
        #region Public Interface
        /// <summary>
        /// Displays the dialog.
        /// </summary>
        public void ShowDialog(Session session, ExpandedNodeId nodeId)
        {
            if (session == null)   throw new ArgumentNullException("session");
            if (nodeId == null) throw new ArgumentNullException("nodeId");
            
            m_session = session;
            m_nodeId  = nodeId;

            AttributesCTRL.Initialize(session, nodeId);

            if (ShowDialog() != DialogResult.OK)
            {
                return;
            }
        }
        #endregion

        private void OkBTN_Click(object sender, EventArgs e)
        {
            try
            {
                AttributesCTRL.Initialize(m_session, m_nodeId);
            }
            catch (Exception exception)
            {
                GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }
    }
}
