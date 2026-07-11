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

namespace Opc.Ua.Client.Controls
{
    partial class BrowseTreeViewCtrl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.BrowseMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Browse_RefreshMI = new System.Windows.Forms.ToolStripMenuItem();
            this.BrowseTV = new System.Windows.Forms.TreeView();
            this.BrowseMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // BrowseMenu
            // 
            this.BrowseMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Browse_RefreshMI});
            this.BrowseMenu.Name = "BrowseMenu";
            this.BrowseMenu.Size = new System.Drawing.Size(114, 26);
            // 
            // Browse_RefreshMI
            // 
            this.Browse_RefreshMI.Name = "Browse_RefreshMI";
            this.Browse_RefreshMI.Size = new System.Drawing.Size(113, 22);
            this.Browse_RefreshMI.Text = "Refresh";
            this.Browse_RefreshMI.Click += new System.EventHandler(this.Browse_RefreshMI_Click);
            // 
            // BrowseTV
            // 
            this.BrowseTV.ContextMenuStrip = this.BrowseMenu;
            this.BrowseTV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BrowseTV.Location = new System.Drawing.Point(0, 0);
            this.BrowseTV.Name = "BrowseTV";
            this.BrowseTV.Size = new System.Drawing.Size(1003, 569);
            this.BrowseTV.TabIndex = 2;
            this.BrowseTV.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.BrowseTV_BeforeExpand);
            this.BrowseTV.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.BrowseTV_AfterSelect);
            // 
            // BrowseTreeViewCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BrowseTV);
            this.Name = "BrowseTreeViewCtrl";
            this.Size = new System.Drawing.Size(1003, 569);
            this.BrowseMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip BrowseMenu;
        private System.Windows.Forms.ToolStripMenuItem Browse_RefreshMI;

        /// <summary>
        /// The tree control.
        /// </summary>
        public System.Windows.Forms.TreeView BrowseTV;
    }
}
