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

namespace TutorialClient
{
    /// <summary>
    /// Prompts the user to select an area to use as an event filter.
    /// </summary>
    public partial class CallMethodDlg : Form
    {
        #region Constructors
        /// <summary>
        /// Creates an empty form.
        /// </summary>
        public CallMethodDlg()
        {
            InitializeComponent();
        }
        #endregion
        
        #region Private Fields
        private Session m_session;
        private NodeId m_objectId;
        private NodeId m_methodId;
        private int m_firstOutputArgument;
        #endregion
        
        #region Public Interface
        public void Show(Session session, NodeId objectId, NodeId methodId)
        {
            m_session = session;
            m_objectId = objectId;
            m_methodId = methodId;

            #region Task #B4 - Call a Method
            UpdateArguments(session, methodId);
            #endregion

            Show();
        }
        #endregion

        #region Task #B4 - Call a Method
        /// <summary>
        /// Updates the list of references.
        /// </summary>
        private void UpdateArguments(Session session, NodeId nodeId)
        {
            ArgumentsLV.Items.Clear();

            // need to fetch the node ids for the argument properties.
            BrowsePathCollection browsePaths = new BrowsePathCollection();

            foreach (string browseName in new string[] { BrowseNames.InputArguments, BrowseNames.OutputArguments })
            {
                BrowsePath browsePath = new BrowsePath();
                browsePath.StartingNode = nodeId;
                browsePath.Handle = browseName;

                RelativePathElement element = new RelativePathElement();
                element.ReferenceTypeId = ReferenceTypeIds.HasProperty;
                element.IsInverse = false;
                element.IncludeSubtypes = true;
                element.TargetName = browseName;

                browsePath.RelativePath.Elements.Add(element);
                browsePaths.Add(browsePath);
            }

            // translate property names.
            BrowsePathResultCollection results = null;
            DiagnosticInfoCollection diagnosticInfos = null;

            session.TranslateBrowsePathsToNodeIds(
                null,
                browsePaths,
                out results,
                out diagnosticInfos);

            ClientBase.ValidateResponse(results, browsePaths);
            ClientBase.ValidateDiagnosticInfos(diagnosticInfos, browsePaths);

            // create a list of values to read.
            ReadValueIdCollection valuesToRead = new ReadValueIdCollection();

            for (int ii = 0; ii < results.Count; ii++)
            {
                if (StatusCode.IsBad(results[ii].StatusCode) || results[ii].Targets.Count <= 0)
                {
                    continue;
                }

                ReadValueId valueToRead = new ReadValueId();
                valueToRead.NodeId = (NodeId)results[ii].Targets[0].TargetId;
                valueToRead.AttributeId = Attributes.Value;
                valueToRead.Handle = browsePaths[ii].Handle;
                valuesToRead.Add(valueToRead);
            }

            // read the values.
            if (valuesToRead.Count > 0)
            {
                DataValueCollection values = null;

                session.Read(
                    null,
                    0,
                    TimestampsToReturn.Neither,
                    valuesToRead,
                    out values,
                    out diagnosticInfos);

                ClientBase.ValidateResponse(results, valuesToRead);
                ClientBase.ValidateDiagnosticInfos(diagnosticInfos, valuesToRead);

                // update the list control.
                for (int ii = 0; ii < values.Count; ii++)
                {
                    // all structures are wrapped in extension objects.
                    ExtensionObject[] extensions = values[ii].GetValue<ExtensionObject[]>(null);

                    if (extensions != null)
                    {
                        // convert to an argument structure.
                        Argument[] arguments = (Argument[])ExtensionObject.ToArray(extensions, typeof(Argument));
                        UpdateList(session, arguments, (string)valuesToRead[ii].Handle);
                    }
                }
            }

            // auto size the columns.
            for (int ii = 0; ii < ArgumentsLV.Columns.Count; ii++)
            {
                ArgumentsLV.Columns[ii].Width = -2;
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Updates the list control.
        /// </summary>
        private void UpdateList(Session session, Argument[] arguments, string browseName)
        {
            for (int ii = 0; ii < arguments.Length; ii++)
            {
                Argument argument = arguments[ii];
                Variant defaultValue = new Variant(TypeInfo.GetDefaultValue(argument.DataType, argument.ValueRank));

                ListViewItem item = new ListViewItem(arguments[ii].Name);

                if (browseName == BrowseNames.InputArguments)
                {
                    item.SubItems.Add("IN");
                    m_firstOutputArgument++;
                }
                else
                {
                    item.SubItems.Add("OUT");
                }

                string dataType = session.NodeCache.GetDisplayText(arguments[ii].DataType);

                if (arguments[ii].ValueRank >= 0)
                {
                    dataType += "[]";
                }

                item.SubItems.Add(defaultValue.ToString());
                item.SubItems.Add(dataType);
                item.SubItems.Add(Utils.Format("{0}", arguments[ii].Description));
                item.Tag = defaultValue;

                ArgumentsLV.Items.Add(item);
            }
        }
        #endregion

        #region Event Handlers
        private void OkBTN_Click(object sender, EventArgs e)
        {
            try
            {
                #region Task #B4 - Call a Method
                List<object> inputArguments = new List<object>();

                for (int ii = 0; ii < m_firstOutputArgument; ii++)
                {
                    Variant value = (Variant)ArgumentsLV.Items[ii].Tag;
                    inputArguments.Add(value.Value);
                }

                IList<object> outputArguments = m_session.Call(m_objectId, m_methodId, inputArguments.ToArray());

                for (int ii = m_firstOutputArgument; ii-m_firstOutputArgument < outputArguments.Count && ii < ArgumentsLV.Items.Count; ii++)
                {
                    Variant value = new Variant(outputArguments[ii-m_firstOutputArgument]);
                    ArgumentsLV.Items[ii].SubItems[2].Text = value.ToString();
                }

                MessageBox.Show("Call succeeded.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                #endregion
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException(this.Text, exception);
            }
        }

        private void CancelBTN_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException(this.Text, exception);
            }
        }
        #endregion

    }
}
