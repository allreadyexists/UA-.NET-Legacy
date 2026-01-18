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

namespace Opc.Ua.StackTest
{
    /// <summary>
    /// A class which displays the test cases configured.
    /// </summary>
    partial class TestCasesCtrl
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
            this.IncludeTestMI = new System.Windows.Forms.ToolStripMenuItem();
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
            this.IncludeTestMI});
            this.PopupMenu.Name = "PopupMenu";
            this.PopupMenu.Size = new System.Drawing.Size(134, 26);
            // 
            // SkipTestMI
            // 
            this.IncludeTestMI.Checked = true;
            this.IncludeTestMI.CheckOnClick = true;
            this.IncludeTestMI.CheckState = System.Windows.Forms.CheckState.Checked;
            this.IncludeTestMI.Name = "SkipTestMI";
            this.IncludeTestMI.Size = new System.Drawing.Size(133, 22);
            this.IncludeTestMI.Text = "Include Test";
            this.IncludeTestMI.CheckedChanged += new System.EventHandler(this.IncludeTestMI_CheckedChanged);
            // 
            // TestCasesCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "TestCasesCtrl";
            this.Controls.SetChildIndex(this.ItemsLV, 0);
            this.PopupMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip PopupMenu;
        private System.Windows.Forms.ToolStripMenuItem IncludeTestMI;
    }
}
