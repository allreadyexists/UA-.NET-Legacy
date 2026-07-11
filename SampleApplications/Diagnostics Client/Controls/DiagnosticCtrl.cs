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
using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Client.Controls;
using Opc.Ua.Sample.Controls;

namespace Opc.Ua.Client.Diagnostic.Controls
{
  public partial class DiagnosticCtrl : Opc.Ua.Client.Controls.BaseListCtrl
  {

    #region Private Fields

		private readonly object[][] m_ColumnNames = new object[][]
		{
			new object[] { "Name",  HorizontalAlignment.Left, null, 146 },  
			new object[] { "Value", HorizontalAlignment.Left, null, 250 }, 
			new object[] { "Type",  HorizontalAlignment.Left, null, 146}, 
			new object[] { "StatusCode",  HorizontalAlignment.Left, null, 146}, 
			new object[] { "Source TimeStamp",  HorizontalAlignment.Left, null, 146}, 
			new object[] { "Server TimeStamp",  HorizontalAlignment.Left, null, 146} 
		};

    protected Session m_Session;
    protected Subscription m_Subscription;
    protected MonitoredItemNotificationEventHandler m_ItemNotification;
    public List<Form> m_Forms = new List<Form>();
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1 = new ContextMenuStrip();
    private System.Windows.Forms.ToolStripMenuItem detailsToolStripMenuItem = new ToolStripMenuItem();

    #endregion

    public DiagnosticCtrl()
    {
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.detailsToolStripMenuItem});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(153, 48);
      // 
      // detailsToolStripMenuItem
      // 
      this.detailsToolStripMenuItem.Name = "detailsToolStripMenuItem";
      this.detailsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.detailsToolStripMenuItem.Text = "Details";
      this.detailsToolStripMenuItem.Click += new System.EventHandler(this.DetailsToolStripMenuItem_Click);

      InitializeComponent();
      SetColumns(m_ColumnNames);

      m_ItemNotification = new MonitoredItemNotificationEventHandler(ItemNotification);

      ItemsLV.Sorting = SortOrder.None;
      ItemsLV.View = View.Details;
      ItemsLV.HideSelection = false;
      ItemsLV.MultiSelect = false;
      ItemsLV.HeaderStyle = ColumnHeaderStyle.Nonclickable;
      ItemsLV.FullRowSelect = true;
      ItemsLV.Width = 200;
      ItemsLV.ContextMenuStrip = contextMenuStrip1;

    }

    public void LoadItems(Session session, List<DiagnosticListViewItem> items, Subscription subscription)
    {
      m_Session = session;
      m_Subscription = subscription;
      ItemsLV.Items.Clear();
      foreach (DiagnosticListViewItem item in items)
      {
        MonitoredItem mi = item.CreateMonitoredItem(m_Session, m_Subscription, ItemsLV);
        if (mi != null)
        {
          mi.Notification += m_ItemNotification;
          item.MonitoredItem = mi;
        }
      }
      m_Subscription.ApplyChanges();
    }

    public void LoadItems(Session session, List<DiagnosticListViewItem> items)
    {
      Subscription subscription = new Subscription(session.DefaultSubscription);
      bool bResult = session.AddSubscription(subscription);
      subscription.DisplayName = session.ToString();
      subscription.Create();
      LoadItems(session, items, subscription);
    }

    /// <summary>
    /// 
    /// </summary>
    public void Close()
    {
      foreach (Form form in m_Forms)
      {
        form.Close();
      }
      m_Forms.Clear();

      foreach (ListViewItem lvi in ItemsLV.Items)
      {
        DiagnosticListViewItem dlvi = lvi.Tag as DiagnosticListViewItem;
        dlvi.MonitoredItem.Notification -= m_ItemNotification;
        m_Subscription.RemoveItem(dlvi.MonitoredItem);
      }
      m_Session.RemoveSubscription(m_Subscription);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="monitoredItem"></param>
    /// <param name="e"></param>
    protected virtual void ItemNotificationHandler(MonitoredItem monitoredItem, MonitoredItemNotificationEventArgs e)
    {
      try
      {
        if (monitoredItem != null)
        {
          Opc.Ua.MonitoredItemNotification change = e.NotificationValue as Opc.Ua.MonitoredItemNotification;
          if (change != null)
          {
            DataValue dv = change.Value;
            if (dv != null)
            {
              ListViewItem[] lvis = ItemsLV.Items.Find(monitoredItem.ClientHandle.ToString(), false);
              if (lvis.Length > 0)
              {
                DiagnosticListViewItem dlvi = lvis[0].Tag as DiagnosticListViewItem;
                dlvi.UpdateInListView(lvis[0], dv, m_Session);
              }
            }
            else
            {
              Utils.Trace("dv is null: {0}", MethodBase.GetCurrentMethod());
            }
          }
          else
          {
            EventFieldList eventFields = e.NotificationValue as EventFieldList;
            if (eventFields != null)
            {

              // get the event fields.
              NodeId eventType      = monitoredItem.GetFieldValue(eventFields, ObjectTypes.BaseEventType, BrowseNames.EventType) as NodeId;
              string sourceName     = monitoredItem.GetFieldValue(eventFields, ObjectTypes.BaseEventType, BrowseNames.SourceName) as string;
              DateTime? time        = monitoredItem.GetFieldValue(eventFields, ObjectTypes.BaseEventType, BrowseNames.Time) as DateTime?;
              ushort? severity      = monitoredItem.GetFieldValue(eventFields, ObjectTypes.BaseEventType, BrowseNames.Severity) as ushort?;
              LocalizedText message = monitoredItem.GetFieldValue(eventFields, ObjectTypes.BaseEventType, BrowseNames.Message) as LocalizedText;
              NodeId sourceNode     = monitoredItem.GetFieldValue(eventFields, ObjectTypes.BaseEventType, BrowseNames.SourceNode) as NodeId;

              if (eventType == new NodeId(ObjectTypes.AuditAddNodesEventType))
              {
              }
              else
              {
              }
            }
            else
            {
              Utils.Trace("eventFields is null " + MethodBase.GetCurrentMethod());
            }
          }
        }
      }
      catch (Exception exception)
      {
        GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
      }
    }

    /// <summary>
    /// Processes a Publish response from the server.
    /// </summary>
    /// 
    void ItemNotification(MonitoredItem monitoredItem, MonitoredItemNotificationEventArgs e)
    {
      if (InvokeRequired)
      {
        BeginInvoke(new MonitoredItemNotificationEventHandler(ItemNotification), monitoredItem, e);
        return;
      }
      else if (!IsHandleCreated)
      {
        return;
      }
      ItemNotificationHandler(monitoredItem, e);
    }

		/// <summary>
		/// Enables the state of menu items.
		/// </summary>
		protected override void EnableMenuItems(ListViewItem clickedItem)
		{
      DiagnosticListViewItem dlvi = clickedItem.Tag as DiagnosticListViewItem;
      bool hasDetails = false;
      if (dlvi != null)
      {
        hasDetails = dlvi.HasDetails();
      }
      detailsToolStripMenuItem.Enabled = hasDetails;
		}

    private void DetailsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (ItemsLV.SelectedItems.Count > 0)
      {
        DiagnosticListViewItem dlvi = ItemsLV.SelectedItems[0].Tag as DiagnosticListViewItem;
        if (dlvi != null)
        {
          MonitoredItemDlg dlg = new MonitoredItemDlg();
          m_Forms.Add(dlg);
          dlg.Show(dlvi.MonitoredItem);
        }
      }
    }
  }
}
