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
    public partial class WriteDlg : Form
    {
        #region Constructors
        public WriteDlg()
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
        public void Show(Session session, WriteValueCollection values)
        {
            if (session == null) throw new ArgumentNullException("session");
            
            m_session = session;

            BrowseCTRL.SetView(m_session, BrowseViewType.Objects, null);
            WriteValuesCTRL.Initialize(session, values);

            MoveBTN_Click(BackBTN, null);

            Show();
            BringToFront();
        }

        /// <summary>
        /// Writes the valus to the server.
        /// </summary>
        private void Write()
        {
            WriteValueCollection nodesToWrite = Utils.Clone(WriteValuesCTRL.GetValues()) as WriteValueCollection;

            if (nodesToWrite == null || nodesToWrite.Count == 0)
            {
                return;
            }

            foreach (WriteValue nodeToWrite in nodesToWrite)
            {
                NumericRange indexRange;
                ServiceResult result = NumericRange.Validate(nodeToWrite.IndexRange, out indexRange);

                if (ServiceResult.IsGood(result) && indexRange != NumericRange.Empty)
                {
                    // apply the index range.
                    object valueToWrite = nodeToWrite.Value.Value;

                    result = indexRange.ApplyRange(ref valueToWrite);

                    if (ServiceResult.IsGood(result))
                    {
                        nodeToWrite.Value.Value = valueToWrite;
                    }
                }
            }

            StatusCodeCollection results = null;
            DiagnosticInfoCollection diagnosticInfos = null;

            ResponseHeader responseHeader = m_session.Write(
                null,
                nodesToWrite,
                out results,
                out diagnosticInfos);

            ClientBase.ValidateResponse(results, nodesToWrite);
            ClientBase.ValidateDiagnosticInfos(diagnosticInfos, nodesToWrite);

            WriteResultsCTRL.ShowValue(results, true);
        }
        #endregion
        
        #region Event Handlers
        private void BrowseCTRL_ItemsSelected(object sender, NodesSelectedEventArgs e)
        {
            try
            {
                foreach (ReferenceDescription reference in e.References)
                {
                    if (reference.ReferenceTypeId == ReferenceTypeIds.HasProperty || reference.IsForward)
                    {
                        WriteValuesCTRL.AddValue(reference);
                    }
                }
            }
            catch (Exception exception)
            {
				GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void MoveBTN_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender == NextBTN)
                {
                    Write();

                    WriteValuesCTRL.Parent  = SplitterPN.Panel1;

                    BackBTN.Visible         = true;
                    NextBTN.Visible         = false;
                    WriteBTN.Visible         = true;
                    WriteValuesCTRL.Visible  = true;
                    WriteResultsCTRL.Visible = true;
                    BrowseCTRL.Visible      = false;
                }

                else if (sender == BackBTN)
                {
                    WriteValuesCTRL.Parent  = SplitterPN.Panel2;

                    BackBTN.Visible          = false;
                    NextBTN.Visible          = true;
                    WriteBTN.Visible          = false;
                    WriteResultsCTRL.Visible  = false;
                    BrowseCTRL.Visible       = true;
                    WriteValuesCTRL.Visible   = true;
                }
            }
            catch (Exception exception)
            {
				GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void WriteMI_Click(object sender, EventArgs e)
        {
            try
            {
                Write();
            }
            catch (Exception exception)
            {
				GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
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
				GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }
        #endregion
    }
}
