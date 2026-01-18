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
    public partial class SelectClauseListCtrl : Opc.Ua.Client.Controls.BaseListCtrl
    {
        public SelectClauseListCtrl()
        {
            InitializeComponent();                        
			SetColumns(m_ColumnNames);
        }

        #region Private Fields
        private Session m_session;
        private SimpleAttributeOperandCollection m_selectClauses;

        /// <summary>
		/// The columns to display in the control.
		/// </summary>
		private readonly object[][] m_ColumnNames = new object[][]
		{
			new object[] { "Event Type",  HorizontalAlignment.Left, null },  
			new object[] { "Field Name",  HorizontalAlignment.Left, null },  
			new object[] { "Index Range", HorizontalAlignment.Left, null }
		};
		#endregion

        #region Public Interface
        /// <summary>
        /// Clears the contents of the control,
        /// </summary>
        public void Clear()
        {
            ItemsLV.Items.Clear();
            AdjustColumns();
        }

        /// <summary>
        /// Sets the nodes in the control.
        /// </summary>
        public void Initialize(Session session, SimpleAttributeOperandCollection selectClauses)
        {
            if (session == null) throw new ArgumentNullException("session");
            
            Clear();
            
            m_session       = session;
            m_selectClauses = selectClauses;

            if (selectClauses == null)
            {
                return;                
            }

            foreach (SimpleAttributeOperand clause in selectClauses)
            {
                if (clause != null)
                {
                    AddItem(clause, "Property", -1);
                }
            }

            AdjustColumns();
        }

        /// <summary>
        /// Adds a select clause to the control.
        /// </summary>
        public void AddSelectClause(ReferenceDescription reference)
        {
            if (reference == null)
            {
                return;
            }

            ILocalNode node = m_session.NodeCache.Find(reference.NodeId) as ILocalNode;

            if (node == null)
            {
                return;
            }
            
            SimpleAttributeOperand clause = new SimpleAttributeOperand();
            
            clause.TypeDefinitionId = m_session.NodeCache.BuildBrowsePath(node, clause.BrowsePath);
            clause.AttributeId      = Attributes.Value;
            
            AddItem(clause, "Property", -1);

            AdjustColumns();            
        }

        /// <summary>
        /// Returns the SelectClauses in the control.
        /// </summary>
        public SimpleAttributeOperandCollection GetSelectClauses()
        {
            SimpleAttributeOperandCollection clauses = new SimpleAttributeOperandCollection();

            foreach (ListViewItem listItem in ItemsLV.Items)
            {
                SimpleAttributeOperand clause = listItem.Tag as SimpleAttributeOperand;

                if (clause != null)
                {
                    clauses.Add(clause);
                }
            }

            return clauses;
        }
		#endregion
        
        #region Overridden Methods
        /// <see cref="BaseListCtrl.EnableMenuItems" />
		protected override void EnableMenuItems(ListViewItem clickedItem)
		{               
            ViewMI.Enabled   = ItemsLV.SelectedItems.Count == 1;
            DeleteMI.Enabled = ItemsLV.SelectedItems.Count > 0;
		}
        
        /// <see cref="BaseListCtrl.UpdateItem" />
        protected override void UpdateItem(ListViewItem listItem, object item)
        {
			SimpleAttributeOperand clause = item as SimpleAttributeOperand;

			if (clause == null)
			{
				base.UpdateItem(listItem, item);
				return;
			}

            INode eventType = m_session.NodeCache.Find(clause.TypeDefinitionId);

            if (eventType != null)
            {
                listItem.SubItems[0].Text = String.Format("{0}", eventType);
            }
            else
            {
                listItem.SubItems[0].Text = String.Format("(unspecified)");
            }

		    listItem.SubItems[1].Text = String.Format("{0}", SimpleAttributeOperand.Format(clause.BrowsePath));
		    listItem.SubItems[2].Text = String.Format("{0}", clause.IndexRange);

			listItem.Tag = item;
        }

        /// <summary>
        /// Handles a drop event.
        /// </summary>
        protected override void ItemsLV_DragDrop(object sender, DragEventArgs e)
        {            
            try
            {
                ReferenceDescription reference = e.Data.GetData(typeof(ReferenceDescription)) as ReferenceDescription;

                if (reference == null)
                {
                    return;
                }
                    
                AddSelectClause(reference);
            }
            catch (Exception exception)
            {
				GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }
        #endregion
        
        #region Event Handlers
        private void ViewMI_Click(object sender, EventArgs e)
        {
            try
            {
                SimpleAttributeOperand[] clauses = GetSelectedItems(typeof(SimpleAttributeOperand)) as SimpleAttributeOperand[];

                if (clauses == null || clauses.Length == 1)
                {
                    // TBD
                }
            }
            catch (Exception exception)
            {
				GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }

        private void DeleteMI_Click(object sender, EventArgs e)
        {
            try
            {
                List<ListViewItem> items = new List<ListViewItem>();

                foreach (ListViewItem item in ItemsLV.SelectedItems)
                {
                    items.Add(item);
                }

                foreach (ListViewItem item in items)
                {
                    item.Remove();
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
