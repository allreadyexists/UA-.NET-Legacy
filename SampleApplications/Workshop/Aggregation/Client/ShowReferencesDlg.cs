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
using Opc.Ua.Client.Controls;

namespace AggregationClient
{
    /// <summary>
    /// Prompts the user to select an area to use as an event filter.
    /// </summary>
    public partial class ShowReferencesDlg : Form
    {
        #region Constructors
        /// <summary>
        /// Creates an empty form.
        /// </summary>
        public ShowReferencesDlg()
        {
            InitializeComponent();
        }
        #endregion
        
        #region Private Fields
        private Session m_session;
        private NodeId m_nodeId;
        private ReferenceDescription m_reference;
        #endregion
        
        #region Public Interface
        public ReferenceDescription ShowDialog(Session session, NodeId nodeId)
        {
            m_session = session;

            #region Task #B1 - Browse References
            UpdateList(session, nodeId);
            #endregion

            // display the dialog.
            if (ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            return m_reference;
        }
        #endregion

        #region Task #B1 - Browse References
        /// <summary>
        /// Updates the list of references.
        /// </summary>
        private void UpdateList(Session session, NodeId nodeId)
        {
            m_nodeId = nodeId;
            ReferencesLV.Items.Clear();
            List<ReferenceDescription> references = Browse(session, nodeId);
            DisplayReferences(session, references);
        }

        /// <summary>
        /// Fetches the references for the node.
        /// </summary>
        private List<ReferenceDescription> Browse(Session session, NodeId nodeId)
        {
            List<ReferenceDescription> references = new List<ReferenceDescription>();

            // specify the references to follow and the fields to return.
            BrowseDescription nodeToBrowse = new BrowseDescription();

            nodeToBrowse.NodeId = nodeId;
            nodeToBrowse.ReferenceTypeId = ReferenceTypeIds.References;
            nodeToBrowse.IncludeSubtypes = true;
            nodeToBrowse.BrowseDirection = BrowseDirection.Both;
            nodeToBrowse.NodeClassMask = 0;
            nodeToBrowse.ResultMask = (uint)BrowseResultMask.All;

            BrowseDescriptionCollection nodesToBrowse = new BrowseDescriptionCollection();
            nodesToBrowse.Add(nodeToBrowse);
            
            // start the browse operation.
            BrowseResultCollection results = null;
            DiagnosticInfoCollection diagnosticInfos = null;

            ResponseHeader responseHeader = session.Browse(
                null,
                null,
                2,
                nodesToBrowse,
                out results,
                out diagnosticInfos);

            // these do sanity checks on the result - make sure response matched the request.
            ClientBase.ValidateResponse(results, nodesToBrowse);
            ClientBase.ValidateDiagnosticInfos(diagnosticInfos, nodesToBrowse);

            // check status.
            if (StatusCode.IsBad(results[0].StatusCode))
            {
                // embed the diagnostic information in a exception.
                throw ServiceResultException.Create(results[0].StatusCode, 0, diagnosticInfos, responseHeader.StringTable);
            }

            // add first batch.
            references.AddRange(results[0].References);

            // check if server limited the results.
            while (results[0].ContinuationPoint != null && results[0].ContinuationPoint.Length > 0)
            {
                ByteStringCollection continuationPoints = new ByteStringCollection();
                continuationPoints.Add(results[0].ContinuationPoint);

                // continue browse operation.
                responseHeader = session.BrowseNext(
                    null,
                    false,
                    continuationPoints,
                    out results,
                    out diagnosticInfos);

                ClientBase.ValidateResponse(results, continuationPoints);
                ClientBase.ValidateDiagnosticInfos(diagnosticInfos, continuationPoints);
                
                // check status.
                if (StatusCode.IsBad(results[0].StatusCode))
                {
                    // embed the diagnostic information in a exception.
                    throw ServiceResultException.Create(results[0].StatusCode, 0, diagnosticInfos, responseHeader.StringTable);
                }

                // add next batch.
                references.AddRange(results[0].References);
            }

            return references;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Displays the references in the control.
        /// </summary>
        private void DisplayReferences(Session session, List<ReferenceDescription> references)
        {
            ReferencesLV.Items.Clear();

            for (int ii = 0; ii < references.Count; ii++)
            {
                ReferenceDescription reference = references[ii];

                string referenceType = null;

                // look up the name for the reference
                IReferenceType referenceTypeNode = session.NodeCache.Find(reference.ReferenceTypeId) as IReferenceType;

                if (referenceTypeNode != null)
                {
                    referenceType = referenceTypeNode.DisplayName.Text;

                    if (!reference.IsForward && !LocalizedText.IsNullOrEmpty(referenceTypeNode.InverseName))
                    {
                        referenceType = referenceTypeNode.InverseName.Text;
                    }
                }

                // the node cache is used to store the type model so it can be accessed locally.
                string typeDefinition = session.NodeCache.GetDisplayText(reference.TypeDefinition);

                ListViewItem item = new ListViewItem(referenceType);

                // the ToString() operator on the ReferenceDescription returns the target name.
                item.SubItems.Add(reference.ToString());
                item.SubItems.Add(reference.NodeClass.ToString());
                item.SubItems.Add(typeDefinition);

                item.Tag = reference;

                ReferencesLV.Items.Add(item);
            }

            // auto size the columns.
            for (int ii = 0; ii < ReferencesLV.Columns.Count; ii++)
            {
                ReferencesLV.Columns[ii].Width = -2;
            }
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Handles the click event for the OK button.
        /// </summary>
        private void OkBTN_Click(object sender, EventArgs e)
        {
            try
            {
                #region Task #B1 - Browse References
                if (ReferencesLV.SelectedItems.Count == 0)
                {
                    return;
                }

                m_reference = ReferencesLV.SelectedItems[0].Tag as ReferenceDescription;
                #endregion

                DialogResult = DialogResult.OK;
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException(this.Text, exception);
            }
        }
        #endregion
    }
}
