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
    partial class CertificateStoreTreeCtrl
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
            this.CopyMI = new System.Windows.Forms.ToolStripMenuItem();
            this.PasteMI = new System.Windows.Forms.ToolStripMenuItem();
            this.PopupMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // NodesTV
            // 
            this.NodesTV.ContextMenuStrip = this.PopupMenu;
            this.NodesTV.LineColor = System.Drawing.Color.Black;
            this.NodesTV.ShowRootLines = false;
            // 
            // PopupMenu
            // 
            this.PopupMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CopyMI,
            this.PasteMI});
            this.PopupMenu.Name = "PopupMenu";
            this.PopupMenu.Size = new System.Drawing.Size(153, 70);
            // 
            // CopyMI
            // 
            this.CopyMI.Name = "CopyMI";
            this.CopyMI.Size = new System.Drawing.Size(152, 22);
            this.CopyMI.Text = "Copy";
            this.CopyMI.Click += new System.EventHandler(this.CopyMI_Click);
            // 
            // PasteMI
            // 
            this.PasteMI.Name = "PasteMI";
            this.PasteMI.Size = new System.Drawing.Size(152, 22);
            this.PasteMI.Text = "Paste";
            this.PasteMI.Click += new System.EventHandler(this.PasteMI_Click);
            // 
            // CertificateStoreTreeCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.EnableDragging = true;
            this.Name = "CertificateStoreTreeCtrl";
            this.Controls.SetChildIndex(this.NodesTV, 0);
            this.PopupMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip PopupMenu;
        private System.Windows.Forms.ToolStripMenuItem CopyMI;
        private System.Windows.Forms.ToolStripMenuItem PasteMI;

    }
}
