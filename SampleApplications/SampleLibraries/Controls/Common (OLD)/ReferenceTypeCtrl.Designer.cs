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
    partial class ReferenceTypeCtrl
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
            this.ReferenceTypesCB = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // ReferenceTypesCB
            // 
            this.ReferenceTypesCB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ReferenceTypesCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ReferenceTypesCB.FormattingEnabled = true;
            this.ReferenceTypesCB.Location = new System.Drawing.Point(0, 0);
            this.ReferenceTypesCB.Name = "ReferenceTypesCB";
            this.ReferenceTypesCB.Size = new System.Drawing.Size(200, 21);
            this.ReferenceTypesCB.TabIndex = 0;
            this.ReferenceTypesCB.SelectedIndexChanged += new System.EventHandler(this.ReferenceTypesCB_SelectedIndexChanged);
            // 
            // ReferenceTypeCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ReferenceTypesCB);
            this.MaximumSize = new System.Drawing.Size(4096, 21);
            this.MinimumSize = new System.Drawing.Size(200, 21);
            this.Name = "ReferenceTypeCtrl";
            this.Size = new System.Drawing.Size(200, 21);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox ReferenceTypesCB;
    }
}
