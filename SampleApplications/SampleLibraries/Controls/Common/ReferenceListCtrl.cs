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

namespace Opc.Ua.Client.Controls
{
    /// <summary>
    /// Displays a list of references for a node.
    /// </summary>
    public partial class ReferenceListCtrl : UserControl
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public ReferenceListCtrl()
        {
            InitializeComponent();
            BrowseDirection = BrowseDirection.Both;
            ReferencesDV.AutoGenerateColumns = false;
            ReferencesDV.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ImageList = new ClientUtils().ImageList;

            m_dataset = new DataSet();

            m_dataset.Tables.Add("References");
            m_dataset.Tables[0].Columns.Add("Reference", typeof(ReferenceDescription));
            m_dataset.Tables[0].Columns.Add("TargetName", typeof(string));
            m_dataset.Tables[0].Columns.Add("ReferenceType", typeof(string));
            m_dataset.Tables[0].Columns.Add("IsForward", typeof(bool));
            m_dataset.Tables[0].Columns.Add("NodeClass", typeof(string));
            m_dataset.Tables[0].Columns.Add("TargetType", typeof(string));
            m_dataset.Tables[0].Columns.Add("Image", typeof(Image));

            ReferencesDV.DataSource = m_dataset.Tables[0].DefaultView;
        }
        #endregion
        
        #region Private Fields
        private Session m_session;
        private DataSet m_dataset;
        #endregion
        
        #region Public Interface
        /// <summary>
        /// The node id shown in the control.
        /// </summary>
        NodeId NodeId { get; set; }

        /// <summary>
        /// The view used when browsing.
        /// </summary>
        ViewDescription View { get; set; }

        /// <summary>
        /// The list of references to browse.
        /// </summary>
        NodeId[] ReferenceTypeIds { get; set; }

        /// <summary>
        /// The direction of browsing.
        /// </summary>
        BrowseDirection BrowseDirection { get; set; }

        /// <summary>
        /// Gets or sets the context menu for references list.
        /// </summary>
        public ContextMenuStrip ReferencesMenuStrip
        {
            get { return ReferencesDV.ContextMenuStrip; }
            set { ReferencesDV.ContextMenuStrip = value; }
        }

        /// <summary>
        /// Changes the session.
        /// </summary>
        public void ChangeSession(Session session)
        {
            // do nothing if no change or no node id.
            if (Object.ReferenceEquals(m_session, session))
            {
                return;
            }

            m_session = session;

            // update the display.
            Browse();
        }

        /// <summary>
        /// Changes the node id.
        /// </summary>
        public void ChangeNodeId(NodeId nodeId)
        {
            // do nothing if no change or no session.
            if (NodeId == nodeId)
            {
                return;
            }

            // save the node.
            NodeId = nodeId;

            // update the display.
            Browse();            
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Gets the list of references to follow.
        /// </summary>
        private BrowseDescriptionCollection CreateNodesToBrowse()
        {
            BrowseDescriptionCollection nodesToBrowse = new BrowseDescriptionCollection();

            if (ReferenceTypeIds != null && ReferenceTypeIds.Length > 0)
            {
                for (int ii = 0; ii < ReferenceTypeIds.Length; ii++)
                {
                    BrowseDescription nodeToBrowse = new BrowseDescription();

                    nodeToBrowse.NodeId = NodeId;
                    nodeToBrowse.BrowseDirection = BrowseDirection;
                    nodeToBrowse.ReferenceTypeId = ReferenceTypeIds[ii];
                    nodeToBrowse.IncludeSubtypes = true;
                    nodeToBrowse.NodeClassMask = 0;
                    nodeToBrowse.ResultMask = (uint)BrowseResultMask.All;

                    nodesToBrowse.Add(nodeToBrowse);
                }
            }
            else
            {
                BrowseDescription nodeToBrowse = new BrowseDescription();

                nodeToBrowse.NodeId = NodeId;
                nodeToBrowse.BrowseDirection = BrowseDirection;
                nodeToBrowse.ReferenceTypeId = Opc.Ua.ReferenceTypeIds.References;
                nodeToBrowse.IncludeSubtypes = true;
                nodeToBrowse.NodeClassMask = 0;
                nodeToBrowse.ResultMask = (uint)BrowseResultMask.All;

                nodesToBrowse.Add(nodeToBrowse);
            }

            return nodesToBrowse;
        }

        /// <summary>
        /// Browses for the requested references.
        /// </summary>
        private void Browse()
        {
            m_dataset.Tables[0].Rows.Clear();

            if (m_session == null)
            {
                return;
            }

            ReferenceDescriptionCollection references = ClientUtils.Browse(m_session, View, CreateNodesToBrowse(), false);

            for (int ii = 0; references != null && ii < references.Count; ii++)
            {
                ReferenceDescription reference = references[ii];

                DataRow row = m_dataset.Tables[0].NewRow();

                row[0] = reference;
                row[1] = m_session.NodeCache.GetDisplayText(reference.NodeId);
                row[2] = m_session.NodeCache.GetDisplayText(reference.ReferenceTypeId);
                row[3] = reference.IsForward.ToString();
                row[4] = reference.NodeClass.ToString();
                row[5] = m_session.NodeCache.GetDisplayText(reference.TypeDefinition);
                row[6] = ImageList.Images[ClientUtils.GetImageIndex(m_session, reference.NodeClass, reference.TypeDefinition, false)];

                m_dataset.Tables[0].Rows.Add(row);
            }

            for (int ii = 0; ii < ReferencesDV.SelectedRows.Count; ii++)
            {
                ReferencesDV.SelectedRows[ii].Selected = false;
            }
        }
        #endregion
    }
}
