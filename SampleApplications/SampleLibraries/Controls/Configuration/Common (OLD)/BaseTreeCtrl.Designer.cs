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
    partial class BaseTreeCtrl
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
            this.NodesTV = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // NodesTV
            // 
            this.NodesTV.AllowDrop = true;
            this.NodesTV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NodesTV.Location = new System.Drawing.Point(0, 0);
            this.NodesTV.Name = "NodesTV";
            this.NodesTV.Size = new System.Drawing.Size(489, 397);
            this.NodesTV.TabIndex = 1;
            this.NodesTV.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.NodesTV_GiveFeedback);
            this.NodesTV.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.NodesTV_BeforeExpand);
            this.NodesTV.DoubleClick += new System.EventHandler(this.NodesTV_DoubleClick);
            this.NodesTV.DragDrop += new System.Windows.Forms.DragEventHandler(this.NodesTV_DragDrop);
            this.NodesTV.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.NodesTV_AfterSelect);
            this.NodesTV.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NodesTV_MouseDown);
            this.NodesTV.DragEnter += new System.Windows.Forms.DragEventHandler(this.NodesTV_DragEnter);
            this.NodesTV.DragOver += new System.Windows.Forms.DragEventHandler(this.NodesTV_DragOver);
            // 
            // BaseTreeCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.NodesTV);
            this.Name = "BaseTreeCtrl";
            this.Size = new System.Drawing.Size(489, 397);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
