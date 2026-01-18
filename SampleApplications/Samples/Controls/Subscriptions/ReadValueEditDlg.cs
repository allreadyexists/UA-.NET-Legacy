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
    public partial class ReadValueEditDlg : Form
    {
        public ReadValueEditDlg()
        {
            InitializeComponent();
            this.Icon = ClientUtils.GetAppIcon();
            AttributeIdCB.Items.AddRange(Attributes.GetBrowseNames());
        }

        /// <summary>
        /// Prompts the user to specify the browse options.
        /// </summary>
        public bool ShowDialog(Session session, ReadValueId valueId)
        {
            if (session == null) throw new ArgumentNullException("session");
            if (valueId == null) throw new ArgumentNullException("valueId");

            NodeIdCTRL.Browser = new Browser(session);

            INode node = session.NodeCache.Find(valueId.NodeId);

            if (node != null)
            {
                DisplayNameTB.Text = node.ToString();
            }

            NodeIdCTRL.Identifier      = valueId.NodeId;
            AttributeIdCB.SelectedItem = Attributes.GetBrowseName(valueId.AttributeId);
            IndexRangeTB.Text          = valueId.IndexRange;
            EncodingCB.Text            = (valueId.DataEncoding != null)?valueId.DataEncoding.Name:null;
         
            if (ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            valueId.NodeId      = NodeIdCTRL.Identifier;
            valueId.AttributeId = Attributes.GetIdentifier((string)AttributeIdCB.SelectedItem);
            valueId.IndexRange  = IndexRangeTB.Text;            
         
            if (String.IsNullOrEmpty(EncodingCB.Text))
            {
                valueId.DataEncoding = new QualifiedName(EncodingCB.Text);
            }

            return true;
        }

        private void OkBTN_Click(object sender, EventArgs e)
        {              
            try
            {
                NodeId nodeId = NodeIdCTRL.Identifier;
            }
            catch (Exception)
            {
				MessageBox.Show("Please enter a valid node id.", this.Text);
            }
                        
            try
            {
                if (!String.IsNullOrEmpty(IndexRangeTB.Text))
                {
                    NumericRange indexRange = NumericRange.Parse(IndexRangeTB.Text);
                }
            }
            catch (Exception)
            {
				MessageBox.Show("Please enter a valid index range.", this.Text);
            }

            DialogResult = DialogResult.OK;
        }

        private void NodeIdCTRL_IdentifierChanged(object sender, EventArgs e)
        {
            if (NodeIdCTRL.Reference != null)
            {
                DisplayNameTB.Text = NodeIdCTRL.Reference.ToString();

                if (AttributeIdCB.SelectedItem == null)
                {
                    AttributeIdCB.SelectedItem = Attributes.GetBrowseName(Attributes.Value);
                }
            }
        }
    }
}
