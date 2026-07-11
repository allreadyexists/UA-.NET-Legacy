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
    public partial class HistoryReadDlg : Form
    {
        #region Constructors
        public HistoryReadDlg()
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
        public void Show(Session session, ReadValueIdCollection valueIds)
        {
            if (session == null) throw new ArgumentNullException("session");
            
            m_session = session;

            BrowseCTRL.SetView(m_session, BrowseViewType.Objects, null);
            ReadValuesCTRL.Initialize(session, valueIds);

            MoveBTN_Click(BackBTN, null);

            Show();
            BringToFront();
        }

        private void Read()
        {
            ReadValueIdCollection nodesToRead = ReadValuesCTRL.GetValueIds();

            if (nodesToRead == null || nodesToRead.Count == 0)
            {
                return;
            }

            DataValueCollection values = null;
            DiagnosticInfoCollection diagnosticInfos = null;
            
            ResponseHeader responseHeader = m_session.Read(
                null,
                0,
                TimestampsToReturn.Both,
                nodesToRead,
                out values,
                out diagnosticInfos);

            ClientBase.ValidateResponse(values, nodesToRead);
            ClientBase.ValidateDiagnosticInfos(diagnosticInfos, nodesToRead);  
     
            ReadResultsCTRL.ShowValue(values, true);
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
                        ReadValuesCTRL.AddValueId(reference);
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
                    Read();

                    BackBTN.Visible         = true;
                    NextBTN.Visible         = false;
                    ReadBTN.Visible         = true;
                    ReadValuesCTRL.Visible  = true;
                    ReadResultsCTRL.Visible = true;
                    BrowseCTRL.Visible      = false;
                }

                else if (sender == BackBTN)
                {
                    BackBTN.Visible          = false;
                    NextBTN.Visible          = true;
                    ReadBTN.Visible          = false;
                    ReadResultsCTRL.Visible  = false;
                    BrowseCTRL.Visible       = true;
                    ReadValuesCTRL.Visible   = true;
                }
            }
            catch (Exception exception)
            {
				GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void ReadMI_Click(object sender, EventArgs e)
        {
            try
            {
                Read();
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
