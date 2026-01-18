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

namespace Opc.Ua.Sample.Controls
{
    partial class AttributeListCtrl
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
            this.components = new System.ComponentModel.Container();
            this.PopupMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ViewMI = new System.Windows.Forms.ToolStripMenuItem();
            this.Separator01 = new System.Windows.Forms.ToolStripSeparator();
            this.RefreshMI = new System.Windows.Forms.ToolStripMenuItem();
            this.PopupMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // ItemsLV
            // 
            this.ItemsLV.ContextMenuStrip = this.PopupMenu;
            // 
            // PopupMenu
            // 
            this.PopupMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewMI,
            this.Separator01,
            this.RefreshMI});
            this.PopupMenu.Name = "PopupMenu";
            this.PopupMenu.Size = new System.Drawing.Size(153, 76);
            // 
            // ViewMI
            // 
            this.ViewMI.Name = "ViewMI";
            this.ViewMI.Size = new System.Drawing.Size(152, 22);
            this.ViewMI.Text = "View...";
            this.ViewMI.Click += new System.EventHandler(this.EditMI_Click);
            // 
            // Separator01
            // 
            this.Separator01.Name = "Separator01";
            this.Separator01.Size = new System.Drawing.Size(149, 6);
            // 
            // RefreshMI
            // 
            this.RefreshMI.Name = "RefreshMI";
            this.RefreshMI.Size = new System.Drawing.Size(152, 22);
            this.RefreshMI.Text = "Refresh";
            // 
            // AttributeListCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "AttributeListCtrl";
            this.Controls.SetChildIndex(this.ItemsLV, 0);
            this.PopupMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip PopupMenu;
        private System.Windows.Forms.ToolStripMenuItem ViewMI;
        private System.Windows.Forms.ToolStripMenuItem RefreshMI;
        private System.Windows.Forms.ToolStripSeparator Separator01;
    }
}
