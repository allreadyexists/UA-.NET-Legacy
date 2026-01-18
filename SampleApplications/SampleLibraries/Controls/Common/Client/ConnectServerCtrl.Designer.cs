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
    partial class ConnectServerCtrl
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
            this.ConnectBTN = new System.Windows.Forms.Button();
            this.UseSecurityCK = new System.Windows.Forms.CheckBox();
            this.UrlCB = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // ConnectBTN
            // 
            this.ConnectBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConnectBTN.Location = new System.Drawing.Point(425, 0);
            this.ConnectBTN.Name = "ConnectBTN";
            this.ConnectBTN.Size = new System.Drawing.Size(75, 23);
            this.ConnectBTN.TabIndex = 2;
            this.ConnectBTN.Text = "Connect";
            this.ConnectBTN.UseVisualStyleBackColor = true;
            this.ConnectBTN.Click += new System.EventHandler(this.Server_ConnectMI_Click);
            // 
            // UseSecurityCK
            // 
            this.UseSecurityCK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UseSecurityCK.AutoSize = true;
            this.UseSecurityCK.Checked = true;
            this.UseSecurityCK.CheckState = System.Windows.Forms.CheckState.Checked;
            this.UseSecurityCK.Location = new System.Drawing.Point(335, 3);
            this.UseSecurityCK.Name = "UseSecurityCK";
            this.UseSecurityCK.Size = new System.Drawing.Size(86, 17);
            this.UseSecurityCK.TabIndex = 1;
            this.UseSecurityCK.Text = "Use Security";
            this.UseSecurityCK.UseVisualStyleBackColor = true;
            // 
            // UrlCB
            // 
            this.UrlCB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.UrlCB.FormattingEnabled = true;
            this.UrlCB.Location = new System.Drawing.Point(0, 1);
            this.UrlCB.Name = "UrlCB";
            this.UrlCB.Size = new System.Drawing.Size(327, 21);
            this.UrlCB.TabIndex = 0;
            // 
            // ConnectServerCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ConnectBTN);
            this.Controls.Add(this.UseSecurityCK);
            this.Controls.Add(this.UrlCB);
            this.MaximumSize = new System.Drawing.Size(2048, 23);
            this.MinimumSize = new System.Drawing.Size(500, 23);
            this.Name = "ConnectServerCtrl";
            this.Size = new System.Drawing.Size(500, 23);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ConnectBTN;
        private System.Windows.Forms.CheckBox UseSecurityCK;
        private System.Windows.Forms.ComboBox UrlCB;
    }
}
