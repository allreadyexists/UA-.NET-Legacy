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
    partial class BrowseTreeCtrl
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
            this.TopPN = new System.Windows.Forms.Panel();
            this.RootBTN = new System.Windows.Forms.Button();
            this.BrowseDirectionCTRL = new System.Windows.Forms.ComboBox();
            this.PopupMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SelectMI = new System.Windows.Forms.ToolStripMenuItem();
            this.SelectChildrenMI = new System.Windows.Forms.ToolStripMenuItem();
            this.ReferenceTypeCTRL = new Opc.Ua.Client.Controls.ReferenceTypeCtrl();
            this.TopPN.SuspendLayout();
            this.PopupMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // NodesTV
            // 
            this.NodesTV.ContextMenuStrip = this.PopupMenu;
            this.NodesTV.LineColor = System.Drawing.Color.Black;
            this.NodesTV.Location = new System.Drawing.Point(0, 24);
            this.NodesTV.Size = new System.Drawing.Size(325, 192);
            // 
            // TopPN
            // 
            this.TopPN.Controls.Add(this.RootBTN);
            this.TopPN.Controls.Add(this.ReferenceTypeCTRL);
            this.TopPN.Controls.Add(this.BrowseDirectionCTRL);
            this.TopPN.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPN.Location = new System.Drawing.Point(0, 0);
            this.TopPN.Name = "TopPN";
            this.TopPN.Size = new System.Drawing.Size(325, 24);
            this.TopPN.TabIndex = 3;
            // 
            // RootBTN
            // 
            this.RootBTN.Location = new System.Drawing.Point(0, 0);
            this.RootBTN.Name = "RootBTN";
            this.RootBTN.Size = new System.Drawing.Size(42, 23);
            this.RootBTN.TabIndex = 2;
            this.RootBTN.Text = "Root";
            this.RootBTN.UseVisualStyleBackColor = true;
            this.RootBTN.Click += new System.EventHandler(this.RootBTN_Click);
            // 
            // BrowseDirectionCTRL
            // 
            this.BrowseDirectionCTRL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BrowseDirectionCTRL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BrowseDirectionCTRL.FormattingEnabled = true;
            this.BrowseDirectionCTRL.Location = new System.Drawing.Point(254, 1);
            this.BrowseDirectionCTRL.Name = "BrowseDirectionCTRL";
            this.BrowseDirectionCTRL.Size = new System.Drawing.Size(71, 21);
            this.BrowseDirectionCTRL.TabIndex = 0;
            this.BrowseDirectionCTRL.SelectedIndexChanged += new System.EventHandler(this.BrowseDirectionCTRL_SelectedIndexChanged);
            // 
            // PopupMenu
            // 
            this.PopupMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SelectMI,
            this.SelectChildrenMI});
            this.PopupMenu.Name = "PopupMenu";
            this.PopupMenu.Size = new System.Drawing.Size(146, 48);
            // 
            // SelectMI
            // 
            this.SelectMI.Name = "SelectMI";
            this.SelectMI.Size = new System.Drawing.Size(145, 22);
            this.SelectMI.Text = "Select";
            this.SelectMI.Click += new System.EventHandler(this.SelectMI_Click);
            // 
            // SelectChildrenMI
            // 
            this.SelectChildrenMI.Name = "SelectChildrenMI";
            this.SelectChildrenMI.Size = new System.Drawing.Size(145, 22);
            this.SelectChildrenMI.Text = "Select Children";
            this.SelectChildrenMI.Click += new System.EventHandler(this.SelectChildrenMI_Click);
            // 
            // ReferenceTypeCTRL
            // 
            this.ReferenceTypeCTRL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ReferenceTypeCTRL.Location = new System.Drawing.Point(48, 1);
            this.ReferenceTypeCTRL.MaximumSize = new System.Drawing.Size(4096, 21);
            this.ReferenceTypeCTRL.MinimumSize = new System.Drawing.Size(200, 21);
            this.ReferenceTypeCTRL.Name = "ReferenceTypeCTRL";
            this.ReferenceTypeCTRL.SelectedTypeId = null;
            this.ReferenceTypeCTRL.Size = new System.Drawing.Size(200, 21);
            this.ReferenceTypeCTRL.TabIndex = 1;
            this.ReferenceTypeCTRL.ReferenceSelectionChanged += new System.EventHandler<Opc.Ua.Client.Controls.ReferenceTypeCtrl.ReferenceSelectedEventArgs>(this.ReferenceTypeCTRL_ReferenceSelectionChanged);
            // 
            // BrowseTreeCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.TopPN);
            this.MinimumSize = new System.Drawing.Size(325, 216);
            this.Name = "BrowseTreeCtrl";
            this.Size = new System.Drawing.Size(325, 216);
            this.Controls.SetChildIndex(this.TopPN, 0);
            this.Controls.SetChildIndex(this.NodesTV, 0);
            this.TopPN.ResumeLayout(false);
            this.PopupMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TopPN;
        private Opc.Ua.Client.Controls.ReferenceTypeCtrl ReferenceTypeCTRL;
        private System.Windows.Forms.ComboBox BrowseDirectionCTRL;
        private System.Windows.Forms.Button RootBTN;
        private System.Windows.Forms.ContextMenuStrip PopupMenu;
        private System.Windows.Forms.ToolStripMenuItem SelectMI;
        private System.Windows.Forms.ToolStripMenuItem SelectChildrenMI;
    }
}
