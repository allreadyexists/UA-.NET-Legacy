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

namespace Opc.Ua.Sample.Controls
{
    public partial class NodeIdValueEditDlg : Form
    {
        #region Constructors
        public NodeIdValueEditDlg()
        {
            InitializeComponent();
        }
        #endregion
        
        #region Public Interface
        /// <summary>
        /// Displays the dialog.
        /// </summary>
        public NodeId ShowDialog(Session session, NodeId value)
        {
            if (session == null) throw new ArgumentNullException("session");

            ValueCTRL.Browser    = new Browser(session);
            ValueCTRL.RootId     = Objects.RootFolder;
            ValueCTRL.Identifier = value;

            if (ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            return ValueCTRL.Identifier;
        }

        /// <summary>
        /// Displays the dialog.
        /// </summary>
        public ExpandedNodeId ShowDialog(Session session, ExpandedNodeId value)
        {
            if (session == null) throw new ArgumentNullException("session");

            ValueCTRL.Browser    = new Browser(session);
            ValueCTRL.RootId     = Objects.RootFolder;
            ValueCTRL.Identifier = ExpandedNodeId.ToNodeId(value, session.NamespaceUris);

            if (ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            return ValueCTRL.Identifier;
        }
        #endregion
    }
}
