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
  public partial class ServerDiagnosticsDlg : Form
  {
    public ServerDiagnosticsDlg(Session session)
    {
      InitializeComponent();

      List<DiagnosticListViewItem> list = CreateItems(session);
      serverDiagnosticCtrl1.LoadItems(session, list);
    }
    public ServerDiagnosticsDlg(Session session, Subscription subscription)
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
      items.Add(new DiagnosticListViewItem(serverNode, "ServerDiagnostics.EnabledFlag", 0, false, true));
      items.Add(new DiagnosticListViewItem(serverNode, "ServerDiagnostics.ServerDiagnosticsSummary", 0, true, true));
      items.Add(new DiagnosticListViewItem(serverNode, "ServerDiagnostics.ServerDiagnosticsSummary.CumulatedSessionCount", 1, false, true));
      items.Add(new DiagnosticListViewItem(serverNode, "ServerDiagnostics.ServerDiagnosticsSummary.CumulatedSubscriptionCount", 1, false, true));
      items.Add(new DiagnosticListViewItem(serverNode, "ServerDiagnostics.ServerDiagnosticsSummary.CurrentSessionCount", 1, false, true));
      items.Add(new DiagnosticListViewItem(serverNode, "ServerDiagnostics.ServerDiagnosticsSummary.CurrentSubscriptionCount", 1, false, true));
      items.Add(new DiagnosticListViewItem(serverNode, "ServerDiagnostics.ServerDiagnosticsSummary.PublishingRateCount", 1, false, true));
      items.Add(new DiagnosticListViewItem(serverNode, "ServerDiagnostics.ServerDiagnosticsSummary.RejectedRequestsCount", 1, false, true));
      items.Add(new DiagnosticListViewItem(serverNode, "ServerDiagnostics.ServerDiagnosticsSummary.RejectedSessionCount", 1, false, true));
      items.Add(new DiagnosticListViewItem(serverNode, "ServerDiagnostics.ServerDiagnosticsSummary.SamplingRateCount", 1, false, true));
      items.Add(new DiagnosticListViewItem(serverNode, "ServerDiagnostics.ServerDiagnosticsSummary.SecurityRejectedRequestsCount", 1, false, true));
      items.Add(new DiagnosticListViewItem(serverNode, "ServerDiagnostics.ServerDiagnosticsSummary.SecurityRejectedSessionCount", 1, false, true));
      items.Add(new DiagnosticListViewItem(serverNode, "ServerDiagnostics.ServerDiagnosticsSummary.ServerViewCount", 1, false, true));
      items.Add(new DiagnosticListViewItem(serverNode, "ServerDiagnostics.ServerDiagnosticsSummary.SessionAbortCount", 1, false, true));
      items.Add(new DiagnosticListViewItem(serverNode, "ServerDiagnostics.ServerDiagnosticsSummary.SessionTimeoutCount", 1, false, true));
      items.Add(new DiagnosticListViewItem(serverNode, "ServerDiagnostics.SamplingRateDiagnosticsArray", 0, true, true));
      items.Add(new DiagnosticListViewItem(serverNode, "ServerDiagnostics.SubscriptionDiagnosticsArray", 0, true, true));
      items.Add(new DiagnosticListViewItem(serverNode,"ServerDiagnostics.SessionDiagnosticsSummary", 0, false, false));
      items.Add(new DiagnosticListViewItem(serverNode, "ServerDiagnostics.SessionDiagnosticsSummary.SessionDiagnosticsArray", 1, true, true));
      items.Add(new DiagnosticListViewItem(serverNode, "ServerDiagnostics.SessionDiagnosticsSummary.SessionSecurityDiagnosticsArray", 1, true, true));
                                                  
      return items;                               
    }                                             
                                                  
  }
}
