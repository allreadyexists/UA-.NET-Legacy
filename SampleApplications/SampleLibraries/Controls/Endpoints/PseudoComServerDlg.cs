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
using System.IO;
using System.Threading;
using Opc.Ua.Configuration;

namespace Opc.Ua.Client.Controls
{
    /// <summary>
    /// Prompts the user to edit a PseudoComServerDlg.
    /// </summary>
    public partial class PseudoComServerDlg : Form
    {
        #region Constructors
        /// <summary>
        /// Initializes the dialog.
        /// </summary>
        public PseudoComServerDlg()
        {
            InitializeComponent();
            this.Icon = ClientUtils.GetAppIcon();

            foreach (ComSpecification value in Enum.GetValues(typeof(ComSpecification)))
            {
                SpecificationCB.Items.Add(value);
            }
        }
        #endregion

        #region Private Fields
        private EndpointComIdentity m_comIdentity;
        #endregion

        #region Public Interface
        /// <summary>
        /// Displays the dialog.
        /// </summary>
        public EndpointComIdentity ShowDialog(ConfiguredEndpoint endpoint)
        {
            if (endpoint == null) throw new ArgumentNullException("endpoint");

            m_comIdentity = endpoint.ComIdentity;

            // assign a default prog id/clsid.
            if (String.IsNullOrEmpty(m_comIdentity.ProgId))
            {
                m_comIdentity.ProgId = PseudoComServer.CreateProgIdFromUrl(ComSpecification.DA, endpoint.EndpointUrl.ToString());
                m_comIdentity.Clsid = ConfigUtils.CLSIDFromProgID(m_comIdentity.ProgId);

                if (m_comIdentity.Clsid == Guid.Empty)
                {
                    m_comIdentity.Clsid = Guid.NewGuid();
                }
            }

            SpecificationCB.SelectedItem = m_comIdentity.Specification;
            ClsidTB.Text  = m_comIdentity.Clsid.ToString();
            ProgIdTB.Text = m_comIdentity.ProgId;

            if (ShowDialog() != DialogResult.OK)
            {
                return null;
            }
                        
            return m_comIdentity;      
        }
        #endregion

        #region Private Methods
        #endregion

        #region Event Handlers
        private void OkBTN_Click(object sender, EventArgs e)
        {
            try
            {
                Guid clsid = Guid.Empty;

                try
                {
                    clsid = new Guid(ClsidTB.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Please specify a valid CLSID.");
                    return;
                }

                string progId = ProgIdTB.Text;

                if (String.IsNullOrEmpty(progId))
                {
                    MessageBox.Show("Please specify a valid ProgID.");
                    return;
                }

                m_comIdentity.Specification = (ComSpecification)SpecificationCB.SelectedItem;
                m_comIdentity.Clsid = clsid;
                m_comIdentity.ProgId = progId;

                DialogResult = DialogResult.OK;
            }
            catch (Exception exception)
            {
                GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void SpecificationCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string progId = ProgIdTB.Text;

                if (!String.IsNullOrEmpty(progId))
                {
                    StringBuilder buffer = new StringBuilder();
                    ComSpecification specification = (ComSpecification)SpecificationCB.SelectedItem;

                    switch (specification)
                    {
                        case ComSpecification.DA:
                        {
                            buffer.Append("OpcDa.");
                            break;
                        }

                        case ComSpecification.AE:
                        {
                            buffer.Append("OpcAe.");
                            break;
                        }

                        case ComSpecification.HDA:
                        {
                            buffer.Append("OpcHda.");
                            break;
                        }
                    }

                    int index = progId.IndexOf('.');

                    if (index != -1)
                    {
                        buffer.Append(progId.Substring(index+1));
                    }
                    else
                    {
                        buffer.Append(progId);
                    }

                    ProgIdTB.Text = buffer.ToString();
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
