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
    partial class EventListView
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
            this.EventsLV = new System.Windows.Forms.ListView();
            this.PopupMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ViewDetailsMI = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteHistoryMI = new System.Windows.Forms.ToolStripMenuItem();
            this.PopupMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // EventsLV
            // 
            this.EventsLV.ContextMenuStrip = this.PopupMenu;
            this.EventsLV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EventsLV.FullRowSelect = true;
            this.EventsLV.Location = new System.Drawing.Point(0, 0);
            this.EventsLV.Name = "EventsLV";
            this.EventsLV.Size = new System.Drawing.Size(961, 570);
            this.EventsLV.TabIndex = 2;
            this.EventsLV.UseCompatibleStateImageBehavior = false;
            this.EventsLV.View = System.Windows.Forms.View.Details;
            // 
            // PopupMenu
            // 
            this.PopupMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewDetailsMI,
            this.DeleteHistoryMI});
            this.PopupMenu.Name = "PopupMenu";
            this.PopupMenu.Size = new System.Drawing.Size(197, 70);
            // 
            // ViewDetailsMI
            // 
            this.ViewDetailsMI.Name = "ViewDetailsMI";
            this.ViewDetailsMI.Size = new System.Drawing.Size(196, 22);
            this.ViewDetailsMI.Text = "View Details...";
            this.ViewDetailsMI.Click += new System.EventHandler(this.ViewDetailsMI_Click);
            // 
            // DeleteHistoryMI
            // 
            this.DeleteHistoryMI.Name = "DeleteHistoryMI";
            this.DeleteHistoryMI.Size = new System.Drawing.Size(196, 22);
            this.DeleteHistoryMI.Text = "Delete from Historian...";
            this.DeleteHistoryMI.Click += new System.EventHandler(this.DeleteHistoryMI_Click);
            // 
            // EventListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.EventsLV);
            this.Name = "EventListView";
            this.Size = new System.Drawing.Size(961, 570);
            this.PopupMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView EventsLV;
        private System.Windows.Forms.ContextMenuStrip PopupMenu;
        private System.Windows.Forms.ToolStripMenuItem ViewDetailsMI;
        private System.Windows.Forms.ToolStripMenuItem DeleteHistoryMI;
    }
}
