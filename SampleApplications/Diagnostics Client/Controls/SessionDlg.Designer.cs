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

namespace Opc.Ua.Client.Diagnostic.Controls
{
  partial class SessionDlg
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.panel1 = new System.Windows.Forms.Panel();
      this.sessionDiagnosticsListCtrl = new Opc.Ua.Client.Diagnostic.Controls.SessionCtrl();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.sessionDiagnosticsListCtrl);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(509, 218);
      this.panel1.TabIndex = 0;
      // 
      // sessionDiagnosticsListCtrl
      // 
      this.sessionDiagnosticsListCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.sessionDiagnosticsListCtrl.Location = new System.Drawing.Point(0, 0);
      this.sessionDiagnosticsListCtrl.Name = "sessionDiagnosticsListCtrl";
      this.sessionDiagnosticsListCtrl.Size = new System.Drawing.Size(509, 218);
      this.sessionDiagnosticsListCtrl.TabIndex = 0;
      // 
      // SessionDlg
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSize = true;
      this.ClientSize = new System.Drawing.Size(509, 218);
      this.Controls.Add(this.panel1);
      this.Name = "SessionDlg";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
      this.Text = "SessionDiagnosticsDlg";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

   private System.Windows.Forms.Panel panel1;
    private SessionCtrl sessionDiagnosticsListCtrl;
  }
}
