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
    partial class YesNoDlg
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
            this.ButtonsPN = new System.Windows.Forms.Panel();
            this.YesToAllBTN = new System.Windows.Forms.Button();
            this.YesBTN = new System.Windows.Forms.Button();
            this.NoBTN = new System.Windows.Forms.Button();
            this.MainPN = new System.Windows.Forms.Panel();
            this.MessageLB = new System.Windows.Forms.Label();
            this.ButtonsPN.SuspendLayout();
            this.MainPN.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonsPN
            // 
            this.ButtonsPN.Controls.Add(this.YesToAllBTN);
            this.ButtonsPN.Controls.Add(this.YesBTN);
            this.ButtonsPN.Controls.Add(this.NoBTN);
            this.ButtonsPN.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ButtonsPN.Location = new System.Drawing.Point(0, 21);
            this.ButtonsPN.Name = "ButtonsPN";
            this.ButtonsPN.Size = new System.Drawing.Size(242, 31);
            this.ButtonsPN.TabIndex = 0;
            // 
            // YesToAllBTN
            // 
            this.YesToAllBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.YesToAllBTN.DialogResult = System.Windows.Forms.DialogResult.Retry;
            this.YesToAllBTN.Location = new System.Drawing.Point(84, 4);
            this.YesToAllBTN.Name = "YesToAllBTN";
            this.YesToAllBTN.Size = new System.Drawing.Size(75, 23);
            this.YesToAllBTN.TabIndex = 2;
            this.YesToAllBTN.Text = "Yes To All";
            this.YesToAllBTN.UseVisualStyleBackColor = true;
            // 
            // YesBTN
            // 
            this.YesBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.YesBTN.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.YesBTN.Location = new System.Drawing.Point(4, 4);
            this.YesBTN.Name = "YesBTN";
            this.YesBTN.Size = new System.Drawing.Size(75, 23);
            this.YesBTN.TabIndex = 0;
            this.YesBTN.Text = "Yes";
            this.YesBTN.UseVisualStyleBackColor = true;
            // 
            // NoBTN
            // 
            this.NoBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.NoBTN.DialogResult = System.Windows.Forms.DialogResult.No;
            this.NoBTN.Location = new System.Drawing.Point(163, 4);
            this.NoBTN.Name = "NoBTN";
            this.NoBTN.Size = new System.Drawing.Size(75, 23);
            this.NoBTN.TabIndex = 1;
            this.NoBTN.Text = "No";
            this.NoBTN.UseVisualStyleBackColor = true;
            // 
            // MainPN
            // 
            this.MainPN.AutoSize = true;
            this.MainPN.Controls.Add(this.MessageLB);
            this.MainPN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPN.Location = new System.Drawing.Point(0, 0);
            this.MainPN.Name = "MainPN";
            this.MainPN.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.MainPN.Size = new System.Drawing.Size(242, 21);
            this.MainPN.TabIndex = 1;
            // 
            // MessageLB
            // 
            this.MessageLB.AutoSize = true;
            this.MessageLB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MessageLB.Location = new System.Drawing.Point(3, 3);
            this.MessageLB.Name = "MessageLB";
            this.MessageLB.Size = new System.Drawing.Size(35, 13);
            this.MessageLB.TabIndex = 0;
            this.MessageLB.Text = "label1";
            // 
            // YesNoDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.NoBTN;
            this.ClientSize = new System.Drawing.Size(242, 52);
            this.Controls.Add(this.MainPN);
            this.Controls.Add(this.ButtonsPN);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "YesNoDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Caption";
            this.ButtonsPN.ResumeLayout(false);
            this.MainPN.ResumeLayout(false);
            this.MainPN.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel ButtonsPN;
        private System.Windows.Forms.Button YesBTN;
        private System.Windows.Forms.Button NoBTN;
        private System.Windows.Forms.Panel MainPN;
        private System.Windows.Forms.Label MessageLB;
        private System.Windows.Forms.Button YesToAllBTN;
    }
}
