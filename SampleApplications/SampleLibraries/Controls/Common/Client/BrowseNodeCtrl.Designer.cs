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
    partial class BrowseNodeCtrl
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
            this.MainPN = new System.Windows.Forms.SplitContainer();
            this.BrowseCTRL = new Opc.Ua.Client.Controls.BrowseTreeViewCtrl();
            this.AttributesCTRL = new Opc.Ua.Client.Controls.AttributesListViewCtrl();
            this.MainPN.Panel1.SuspendLayout();
            this.MainPN.Panel2.SuspendLayout();
            this.MainPN.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainPN
            // 
            this.MainPN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPN.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.MainPN.Location = new System.Drawing.Point(0, 0);
            this.MainPN.Name = "MainPN";
            // 
            // MainPN.Panel1
            // 
            this.MainPN.Panel1.Controls.Add(this.BrowseCTRL);
            // 
            // MainPN.Panel2
            // 
            this.MainPN.Panel2.Controls.Add(this.AttributesCTRL);
            this.MainPN.Size = new System.Drawing.Size(1003, 569);
            this.MainPN.SplitterDistance = 387;
            this.MainPN.TabIndex = 11;
            // 
            // BrowseCTRL
            // 
            this.BrowseCTRL.AttributesControl = this.AttributesCTRL;
            this.BrowseCTRL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BrowseCTRL.Location = new System.Drawing.Point(0, 0);
            this.BrowseCTRL.Name = "BrowseCTRL";
            this.BrowseCTRL.Size = new System.Drawing.Size(387, 569);
            this.BrowseCTRL.TabIndex = 0;
            this.BrowseCTRL.View = null;
            // 
            // AttributesCTRL
            // 
            this.AttributesCTRL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AttributesCTRL.Location = new System.Drawing.Point(0, 0);
            this.AttributesCTRL.Name = "AttributesCTRL";
            this.AttributesCTRL.Size = new System.Drawing.Size(612, 569);
            this.AttributesCTRL.TabIndex = 0;
            this.AttributesCTRL.View = null;
            // 
            // BrowseNodeCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MainPN);
            this.Name = "BrowseNodeCtrl";
            this.Size = new System.Drawing.Size(1003, 569);
            this.MainPN.Panel1.ResumeLayout(false);
            this.MainPN.Panel2.ResumeLayout(false);
            this.MainPN.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer MainPN;

        /// <summary>
        /// The browse list control.
        /// </summary>
        public BrowseTreeViewCtrl BrowseCTRL;

        /// <summary>
        /// The attribute list control.
        /// </summary>
        public AttributesListViewCtrl AttributesCTRL;
    }
}
