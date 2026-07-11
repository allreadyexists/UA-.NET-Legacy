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
    public partial class BrowseDlg : Form
    {
        #region Constructors
        public BrowseDlg()
        {
            InitializeComponent();
            this.Icon = ClientUtils.GetAppIcon();
            m_SessionClosing = new EventHandler(Session_Closing);
        }
        #endregion
        
        #region Private Fields
        private Session m_session;
        private EventHandler m_SessionClosing;
        #endregion

        #region Public Interface
        /// <summary>
        /// Displays the address space with the specified view
        /// </summary>
        public void Show(Session session, NodeId startId)
        {   
            if (session == null) throw new ArgumentNullException("session");
            
            if (m_session != null)
            {
                m_session.SessionClosing -= m_SessionClosing;
            }

            m_session = session;            
            m_session.SessionClosing += m_SessionClosing;
            
            Browser browser  = new Browser(session);

            browser.BrowseDirection = BrowseDirection.Both;
            browser.ContinueUntilDone = true;
            browser.ReferenceTypeId = ReferenceTypeIds.References;

            BrowseCTRL.Initialize(browser, startId);
            
            UpdateNavigationBar();

            Show();
            BringToFront();
        }
        #endregion

        /// <summary>
        /// Updates the navigation bar with the current positions in the browse control.
        /// </summary>
        private void UpdateNavigationBar()
        {
            int index = 0;

            foreach (NodeId nodeId in BrowseCTRL.Positions)
            {
                Node node = m_session.NodeCache.Find(nodeId) as Node;

                string displayText = m_session.NodeCache.GetDisplayText(node);

                if (index < NodeCTRL.Items.Count)
                {
                    if (displayText != NodeCTRL.Items[index] as string)
                    {
                        NodeCTRL.Items[index] = displayText;
                    }
                }
                else
                {
                    NodeCTRL.Items.Add(displayText);
                }

                index++;
            }        
         
            while (index < NodeCTRL.Items.Count)
            {
                NodeCTRL.Items.RemoveAt(NodeCTRL.Items.Count-1);
            }
                                
            NodeCTRL.SelectedIndex = BrowseCTRL.Position;
        }
        
        private void Session_Closing(object sender, EventArgs e)
        {
            if (Object.ReferenceEquals(sender, m_session))
            {
                m_session.SessionClosing -= m_SessionClosing;
                m_session = null;
                Close();
            }
        }

        private void AddressSpaceDlg_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_session != null)
            {
                m_session.SessionClosing -= m_SessionClosing;
                m_session = null;
            }
        }

        private void BackBTN_Click(object sender, EventArgs e)
        {
            try
            {   
                BrowseCTRL.Back();
            }
            catch (Exception exception)
            {
				GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void ForwardBTN_Click(object sender, EventArgs e)
        {
            try
            {   
                BrowseCTRL.Forward();
            }
            catch (Exception exception)
            {
				GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void NodeCTRL_SelectedIndexChanged(object sender, EventArgs e)
        {            
            try
            {   
                BrowseCTRL.SetPosition(NodeCTRL.SelectedIndex);
            }
            catch (Exception exception)
            {
				GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void BrowseCTRL_PositionChanged(object sender, EventArgs e)
        { 
            try
            {
                if (BrowseCTRL.Position < NodeCTRL.Items.Count)
                {
                    NodeCTRL.SelectedIndex = BrowseCTRL.Position;
                }
                else
                {
                    NodeCTRL.SelectedIndex = -1;
                }                   
            }
            catch (Exception exception)
            {
				GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void BrowseCTRL_PositionAdded(object sender, EventArgs e)
        {
            try
            {
                UpdateNavigationBar();
            }
            catch (Exception exception)
            {
				GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }
    }
}
