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
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using Opc.Ua.Client;
using Opc.Ua.Client.Controls;

namespace Opc.Ua.Client.Controls
{
    /// <summary>
    /// A list of node ids.
    /// </summary>
    public partial class NodeIdCtrl : UserControl
    {
		#region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="NodeIdCtrl"/> class.
        /// </summary>
        public NodeIdCtrl()
        {
            InitializeComponent();

            m_rootId = Objects.RootFolder;
            BrowseBTN.Enabled = false;
        }
		#endregion
        
		#region Event Handlers
        private Browser m_browser;
        private NodeId m_rootId;
        private ReferenceDescription m_reference;
        private event EventHandler m_IdentifierChanged;
		#endregion
        
		#region Public Interface
        /// <summary>
        /// Raised if the node id is changed.
        /// </summary>
        public event EventHandler IdentifierChanged
        {
            add    { m_IdentifierChanged += value; }
            remove { m_IdentifierChanged -= value; }
        }
        
        /// <summary>
        /// The browser to used browse for a node id.
        /// </summary>
        [DefaultValue(null)]
        public Browser Browser
        {
            get 
            { 
                return m_browser;  
            }
            
            set 
            { 
                m_browser = value; 
                BrowseBTN.Enabled = m_browser != null;
            }
        }
        
        /// <summary>
        /// The root node id to display when browsing.
        /// </summary>
        [DefaultValue(null)]
        public NodeId RootId
        {
            get
            {
                return m_rootId;                
            }

            set
            {
                m_rootId = value;

                if (NodeId.IsNull(m_rootId))
                {
                    m_rootId = Objects.RootFolder;
                }
            }
        }

        /// <summary>
        /// Returns true if the control is empty.
        /// </summary>
        [DefaultValue(false)]
        public bool IsEmpty
        {
            get
            { 
                return String.IsNullOrEmpty(NodeIdTB.Text);
            }
        }

        /// <summary>
        /// The node identifier specified in the control.
        /// </summary>
        [DefaultValue(null)]
        public NodeId Identifier
        {
            get
            { 
                return NodeId.Parse(NodeIdTB.Text);  
            }
            
            set
            { 
                NodeIdTB.Text = Utils.Format("{0}", value); 
            }
        }
        
        /// <summary>
        /// The reference seleected if the browse feature was used.
        /// </summary>
        [DefaultValue(null)]
        public ReferenceDescription Reference
        {
            get
            { 
                return m_reference;  
            }
            
            set
            { 
                m_reference = value;

                if (m_reference != null)
                {
                    NodeIdTB.Text = Utils.Format("{0}", m_reference.NodeId);
                }
            }
        }
		#endregion
        
		#region Event Handlers
        private void BrowseBTN_Click(object sender, EventArgs e)
        {
            try
            {
                /*
                ReferenceDescription reference = new SelectNodeDlg().ShowDialog(m_browser, RootId);

                if (reference != null)
                {
                    NodeIdTB.Text = Utils.Format("{0}", reference.NodeId);
                    m_reference = reference;

                    if (m_IdentifierChanged != null)
                    {
                        m_IdentifierChanged(this, null);
                    }
                }
                 * */
            }
            catch (Exception exception)
            {
				GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void NodeIdTB_TextChanged(object sender, EventArgs e)
        {
            if (m_IdentifierChanged != null)
            {
                m_IdentifierChanged(this, null);
            }
        }
		#endregion
    }
}
