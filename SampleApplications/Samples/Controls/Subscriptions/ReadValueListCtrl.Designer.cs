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
    partial class ReadValueListCtrl
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
            this.NewMI = new System.Windows.Forms.ToolStripMenuItem();
            this.EditMI = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteMI = new System.Windows.Forms.ToolStripMenuItem();
            this.SetAttributeMI = new System.Windows.Forms.ToolStripMenuItem();
            this.SetEncodingMI = new System.Windows.Forms.ToolStripMenuItem();
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
            this.NewMI,
            this.EditMI,
            this.SetAttributeMI,
            this.SetEncodingMI,
            this.DeleteMI});
            this.PopupMenu.Name = "PopupMenu";
            this.PopupMenu.Size = new System.Drawing.Size(175, 114);
            // 
            // NewMI
            // 
            this.NewMI.Name = "NewMI";
            this.NewMI.Size = new System.Drawing.Size(174, 22);
            this.NewMI.Text = "New...";
            this.NewMI.Click += new System.EventHandler(this.NewMI_Click);
            // 
            // EditMI
            // 
            this.EditMI.Name = "EditMI";
            this.EditMI.Size = new System.Drawing.Size(174, 22);
            this.EditMI.Text = "Edit...";
            this.EditMI.Click += new System.EventHandler(this.EditMI_Click);
            // 
            // DeleteMI
            // 
            this.DeleteMI.Name = "DeleteMI";
            this.DeleteMI.Size = new System.Drawing.Size(174, 22);
            this.DeleteMI.Text = "Delete";
            this.DeleteMI.Click += new System.EventHandler(this.DeleteMI_Click);
            // 
            // SetAttributeMI
            // 
            this.SetAttributeMI.Name = "SetAttributeMI";
            this.SetAttributeMI.Size = new System.Drawing.Size(174, 22);
            this.SetAttributeMI.Text = "Set Attribute...";
            // 
            // SetEncodingMI
            // 
            this.SetEncodingMI.Name = "SetEncodingMI";
            this.SetEncodingMI.Size = new System.Drawing.Size(174, 22);
            this.SetEncodingMI.Text = "Set Data Encoding...";
            // 
            // ReadValueListCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "ReadValueListCtrl";
            this.Controls.SetChildIndex(this.ItemsLV, 0);
            this.PopupMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip PopupMenu;
        private System.Windows.Forms.ToolStripMenuItem EditMI;
        private System.Windows.Forms.ToolStripMenuItem NewMI;
        private System.Windows.Forms.ToolStripMenuItem DeleteMI;
        private System.Windows.Forms.ToolStripMenuItem SetAttributeMI;
        private System.Windows.Forms.ToolStripMenuItem SetEncodingMI;
    }
}
