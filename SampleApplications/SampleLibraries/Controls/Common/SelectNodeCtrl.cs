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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Opc.Ua.Client.Controls
{
    /// <summary>
    /// A control with button that displays an open file dialog.
    /// </summary>
    public partial class SelectNodeCtrl : UserControl
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of the control.
        /// </summary>
        public SelectNodeCtrl()
        {
            InitializeComponent();
        }
        #endregion
        
        #region Private Fields
        private event EventHandler m_NodeSelected;
        private ReferenceDescription m_selectedNode;
        #endregion

        #region Public Interface
        /// <summary>
        /// Gets or sets the current session.
        /// </summary>
        public Session Session { get; set; }

        /// <summary>
        /// Gets or sets starting node.
        /// </summary>
        public NodeId RootId { get; set; }

        /// <summary>
        /// Gets or sets the view to use.
        /// </summary>
        public ViewDescription View { get; set; }

        /// <summary>
        /// Gets or sets the reference types to follow.
        /// </summary>
        public NodeId[] ReferenceTypeIds { get; set; }

        /// <summary>
        /// Gets or sets the current selected node.
        /// </summary>
        public ReferenceDescription SelectedNode 
        {
            get
            {
                return m_selectedNode;
            }

            set
            {
                if (NodeControl != null)
                {
                    NodeControl.Text = null;

                    if (value != null)
                    {
                        NodeControl.Text = value.ToString();
                    }
                }

                m_selectedNode = value;
            }
        }

        /// <summary>
        /// Gets or sets the control that is stores with the current node.
        /// </summary>
        public Control NodeControl { get; set; }

        /// <summary>
        /// Raised when a new node is selected.
        /// </summary>
        public event EventHandler NodeSelected
        {
            add { m_NodeSelected += value; }
            remove { m_NodeSelected -= value; }
        }
        #endregion

        #region Event Handlers
        private void BrowseBTN_Click(object sender, EventArgs e)
        {
            ReferenceDescription reference = new SelectNodeDlg().ShowDialog(
                Session,
                RootId,
                View,
                null,
                ReferenceTypeIds);

            if (reference != null)
            {
                SelectedNode = reference;

                if (m_NodeSelected != null)
                {
                    m_NodeSelected(this, new EventArgs());
                }
            }
        }
        #endregion
    }
}
