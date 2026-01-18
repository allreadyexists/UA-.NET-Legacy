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

namespace Opc.Ua.Client.Diagnostic.Controls
{
  public partial class ServerStatusDlg : Form
  {
    public ServerStatusDlg(Session session)
    {
      InitializeComponent();

      List<DiagnosticListViewItem> list = CreateItems(session);
      serverDiagnosticCtrl1.LoadItems(session, list);
    }
    public ServerStatusDlg(Session session, Subscription subscription)
    {
      InitializeComponent();
      List<DiagnosticListViewItem> list = CreateItems(session);
      serverDiagnosticCtrl1.LoadItems(session, list, subscription);
    }
    private void OnFormClosing(object sender, FormClosingEventArgs e)
    {
      serverDiagnosticCtrl1.Close();
    }
    private List<DiagnosticListViewItem> CreateItems(Session session)
    {
      List<DiagnosticListViewItem> items = new List<DiagnosticListViewItem>();
      NodeId serverNode = new NodeId(Objects.Server);
      items.Add(new DiagnosticListViewItem(serverNode,  "ServerStatus", 0, true, true));
      items.Add(new DiagnosticListViewItem(serverNode,  "ServerStatus.StartTime", 1, false, true));
      items.Add(new DiagnosticListViewItem(serverNode,  "ServerStatus.CurrentTime", 1, false, true));
      items.Add(new DiagnosticListViewItem(serverNode,  "ServerStatus.State", 1, false, true));
      items.Add(new DiagnosticListViewItem(serverNode,  "ServerStatus.BuildInfo", 1, true, true));
      items.Add(new DiagnosticListViewItem(serverNode,  "ServerStatus.BuildInfo.ApplicationUri", 2,false, true));
      items.Add(new DiagnosticListViewItem(serverNode,  "ServerStatus.BuildInfo.ManufacturerName", 2, false, true));
      items.Add(new DiagnosticListViewItem(serverNode,  "ServerStatus.BuildInfo.ApplicationName", 2, false, true));
      items.Add(new DiagnosticListViewItem(serverNode,  "ServerStatus.BuildInfo.SoftwareVersion", 2, false, true));
      items.Add(new DiagnosticListViewItem(serverNode,  "ServerStatus.BuildInfo.BuildNumber", 2, false, true));
      items.Add(new DiagnosticListViewItem(serverNode,  "ServerStatus.BuildInfo.BuildDate", 2, false, true));
      return items;                                
    }

  }
}
