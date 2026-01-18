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
using System.Windows.Forms;
using System.Text;
using Opc.Ua;
using Opc.Ua.Client;

namespace Opc.Ua.Client.Controls
{
    /// <summary>
    /// Prompts the user to select an area to use as an event filter.
    /// </summary>
    public partial class SelectNodeDlg : Form
    {
        #region Constructors
        /// <summary>
        /// Creates an empty form.
        /// </summary>
        public SelectNodeDlg()
        {
            InitializeComponent();
        }
        #endregion
        
        #region Private Fields
        #endregion

        #region Public Interface
        /// <summary>
        /// Prompts the user to select a node.
        /// </summary>
        public NodeId ShowDialog(
            Session session,
            NodeId rootId,
            string caption,
            params NodeId[] referenceTypeIds)
        {
            // set the caption.
            if (!String.IsNullOrEmpty(caption))
            {
                this.Text = caption;
            }

            // set default root.
            if (NodeId.IsNull(rootId))
            {
                rootId = Opc.Ua.ObjectIds.ObjectsFolder;
            }

            // set default reference type.
            if (referenceTypeIds == null)
            {
                referenceTypeIds = new NodeId[] { Opc.Ua.ReferenceTypeIds.HierarchicalReferences };
            }

            // initialize the control.
            BrowseCTRL.Initialize(session, rootId, referenceTypeIds);

            // display the dialog.
            if (ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            // convert to a node id.
            ReferenceDescription reference = BrowseCTRL.SelectedNode;

            if (reference != null && !reference.NodeId.IsAbsolute)
            {
                return (NodeId)reference.NodeId;
            }

            return null;
        }

        /// <summary>
        /// Prompts the user to select a node.
        /// </summary>
        public ReferenceDescription ShowDialog(
            Session session,
            NodeId rootId,
            ViewDescription view, 
            string caption,
            params NodeId[] referenceTypeIds)
        {
            // set the caption.
            if (!String.IsNullOrEmpty(caption))
            {
                this.Text = caption;
            }
            
            // set default root.
            if (NodeId.IsNull(rootId))
            {
                rootId = Opc.Ua.ObjectIds.ObjectsFolder;
            }

            // set default reference type.
            if (referenceTypeIds == null)
            {
                referenceTypeIds = new NodeId[] { Opc.Ua.ReferenceTypeIds.HierarchicalReferences };
            }

            // initialize the control.
            BrowseCTRL.Initialize(session, rootId, referenceTypeIds);
            BrowseCTRL.View = view;

            // display the dialog.
            if (ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            return BrowseCTRL.SelectedNode;
        }
        #endregion
        
        #region Private Methods
        #endregion

        #region Event Handlers
        #endregion
    }
}
