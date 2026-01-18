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
    partial class ArgumentListCtrl
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
            this.EditMI = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearValueMI = new System.Windows.Forms.ToolStripMenuItem();
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
            this.EditMI,
            this.ClearValueMI});
            this.PopupMenu.Name = "PopupMenu";
            this.PopupMenu.Size = new System.Drawing.Size(153, 70);
            // 
            // EditMI
            // 
            this.EditMI.Name = "EditMI";
            this.EditMI.Size = new System.Drawing.Size(152, 22);
            this.EditMI.Text = "Edit...";
            this.EditMI.Click += new System.EventHandler(this.EditMI_Click);
            // 
            // ClearValueMI
            // 
            this.ClearValueMI.Name = "ClearValueMI";
            this.ClearValueMI.Size = new System.Drawing.Size(152, 22);
            this.ClearValueMI.Text = "Clear Value";
            this.ClearValueMI.Click += new System.EventHandler(this.ClearValueMI_Click);
            // 
            // ArgumentListCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "ArgumentListCtrl";
            this.Controls.SetChildIndex(this.ItemsLV, 0);
            this.PopupMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip PopupMenu;
        private System.Windows.Forms.ToolStripMenuItem EditMI;
        private System.Windows.Forms.ToolStripMenuItem ClearValueMI;
    }
}
