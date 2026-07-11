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

namespace TutorialClient
{
    partial class SubscribeDataDlg
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
            this.MainPN = new System.Windows.Forms.Panel();
            this.ResultsDV = new System.Windows.Forms.DataGridView();
            this.MainPN.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResultsDV)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPN
            // 
            this.MainPN.Controls.Add(this.ResultsDV);
            this.MainPN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPN.Location = new System.Drawing.Point(0, 0);
            this.MainPN.Name = "MainPN";
            this.MainPN.Size = new System.Drawing.Size(803, 418);
            this.MainPN.TabIndex = 10;
            // 
            // ResultsDV
            // 
            this.ResultsDV.AllowUserToAddRows = false;
            this.ResultsDV.AllowUserToDeleteRows = false;
            this.ResultsDV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ResultsDV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultsDV.Location = new System.Drawing.Point(0, 0);
            this.ResultsDV.Name = "ResultsDV";
            this.ResultsDV.ReadOnly = true;
            this.ResultsDV.Size = new System.Drawing.Size(803, 418);
            this.ResultsDV.TabIndex = 0;
            // 
            // SubscribeDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 418);
            this.Controls.Add(this.MainPN);
            this.MaximumSize = new System.Drawing.Size(1200, 1200);
            this.MinimumSize = new System.Drawing.Size(400, 91);
            this.Name = "SubscribeDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Subscribe";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SubscribeDlg_FormClosing);
            this.MainPN.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ResultsDV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MainPN;
        private System.Windows.Forms.DataGridView ResultsDV;
    }
}
