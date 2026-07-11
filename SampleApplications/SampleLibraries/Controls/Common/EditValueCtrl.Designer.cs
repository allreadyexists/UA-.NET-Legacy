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
    partial class EditValueCtrl
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
            this.ValueTB = new System.Windows.Forms.TextBox();
            this.ValueBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ValueTB
            // 
            this.ValueTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ValueTB.Location = new System.Drawing.Point(0, 0);
            this.ValueTB.Margin = new System.Windows.Forms.Padding(0);
            this.ValueTB.Name = "ValueTB";
            this.ValueTB.Size = new System.Drawing.Size(96, 20);
            this.ValueTB.TabIndex = 0;
            this.ValueTB.TextChanged += new System.EventHandler(this.ValueTB_TextChanged);
            // 
            // ValueBTN
            // 
            this.ValueBTN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ValueBTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ValueBTN.Location = new System.Drawing.Point(100, 0);
            this.ValueBTN.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.ValueBTN.Name = "ValueBTN";
            this.ValueBTN.Size = new System.Drawing.Size(26, 21);
            this.ValueBTN.TabIndex = 1;
            this.ValueBTN.Text = "...";
            this.ValueBTN.UseVisualStyleBackColor = true;
            this.ValueBTN.Click += new System.EventHandler(this.ValueBTN_Click);
            // 
            // EditValueCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ValueTB);
            this.Controls.Add(this.ValueBTN);
            this.MaximumSize = new System.Drawing.Size(2048, 21);
            this.MinimumSize = new System.Drawing.Size(126, 21);
            this.Name = "EditValueCtrl";
            this.Size = new System.Drawing.Size(126, 21);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ValueTB;
        private System.Windows.Forms.Button ValueBTN;
    }
}
