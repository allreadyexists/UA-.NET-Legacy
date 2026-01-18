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
    partial class NotificationMessageListCtrl
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
            this.RepublishMI = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteMI = new System.Windows.Forms.ToolStripMenuItem();
            this.Separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.OptionsMI = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearMI = new System.Windows.Forms.ToolStripMenuItem();
            this.PopupMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // ItemsLV
            // 
            this.ItemsLV.ContextMenuStrip = this.PopupMenu;
            this.ItemsLV.MultiSelect = false;
            // 
            // PopupMenu
            // 
            this.PopupMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewMI,
            this.RepublishMI,
            this.DeleteMI,
            this.Separator1,
            this.OptionsMI,
            this.ClearMI});
            this.PopupMenu.Name = "PopupMenu";
            this.PopupMenu.Size = new System.Drawing.Size(153, 142);
            // 
            // ViewMI
            // 
            this.ViewMI.Name = "ViewMI";
            this.ViewMI.Size = new System.Drawing.Size(152, 22);
            this.ViewMI.Text = "View...";
            this.ViewMI.Click += new System.EventHandler(this.ViewMI_Click);
            // 
            // RepublishMI
            // 
            this.RepublishMI.Name = "RepublishMI";
            this.RepublishMI.Size = new System.Drawing.Size(152, 22);
            this.RepublishMI.Text = "Republish...";
            this.RepublishMI.Click += new System.EventHandler(this.RepublishMI_Click);
            // 
            // DeleteMI
            // 
            this.DeleteMI.Name = "DeleteMI";
            this.DeleteMI.Size = new System.Drawing.Size(152, 22);
            this.DeleteMI.Text = "Delete";
            this.DeleteMI.Click += new System.EventHandler(this.DeleteMI_Click);
            // 
            // Separator1
            // 
            this.Separator1.Name = "Separator1";
            this.Separator1.Size = new System.Drawing.Size(149, 6);
            // 
            // OptionsMI
            // 
            this.OptionsMI.Name = "OptionsMI";
            this.OptionsMI.Size = new System.Drawing.Size(152, 22);
            this.OptionsMI.Text = "Options...";
            // 
            // ClearMI
            // 
            this.ClearMI.Name = "ClearMI";
            this.ClearMI.Size = new System.Drawing.Size(152, 22);
            this.ClearMI.Text = "Clear";
            this.ClearMI.Click += new System.EventHandler(this.ClearMI_Click);
            // 
            // NotificationMessageListCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "NotificationMessageListCtrl";
            this.Controls.SetChildIndex(this.ItemsLV, 0);
            this.PopupMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip PopupMenu;
        private System.Windows.Forms.ToolStripMenuItem ViewMI;
        private System.Windows.Forms.ToolStripSeparator Separator1;
        private System.Windows.Forms.ToolStripMenuItem OptionsMI;
        private System.Windows.Forms.ToolStripMenuItem ClearMI;
        private System.Windows.Forms.ToolStripMenuItem DeleteMI;
        private System.Windows.Forms.ToolStripMenuItem RepublishMI;
    }
}
