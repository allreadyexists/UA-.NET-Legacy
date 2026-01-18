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

namespace Opc.Ua.Configuration
{
    partial class UserAddControl
    {
        /// <summary> 
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナで生成されたコード

        /// <summary> 
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.CreateButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.userPasswordControl1 = new Opc.Ua.Configuration.UserPasswordControl();
            this.userNameControl1 = new Opc.Ua.Configuration.UserNameControl();
            this.SuspendLayout();
            // 
            // CreateButton
            // 
            this.CreateButton.Location = new System.Drawing.Point(230, 119);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(75, 23);
            this.CreateButton.TabIndex = 2;
            this.CreateButton.Text = "OK";
            this.CreateButton.UseVisualStyleBackColor = true;
            this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(311, 119);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 3;
            this.CloseButton.Text = "Cancel";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // userPasswordControl1
            // 
            this.userPasswordControl1.Location = new System.Drawing.Point(0, 40);
            this.userPasswordControl1.Name = "userPasswordControl1";
            this.userPasswordControl1.Password = "";
            this.userPasswordControl1.PasswordConfirm = "";
            this.userPasswordControl1.Size = new System.Drawing.Size(415, 74);
            this.userPasswordControl1.TabIndex = 1;
            // 
            // userNameControl1
            // 
            this.userNameControl1.Location = new System.Drawing.Point(0, 3);
            this.userNameControl1.Name = "userNameControl1";
            this.userNameControl1.Size = new System.Drawing.Size(415, 51);
            this.userNameControl1.TabIndex = 0;
            this.userNameControl1.UserName = "";
            // 
            // UserAddControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.CreateButton);
            this.Controls.Add(this.userPasswordControl1);
            this.Controls.Add(this.userNameControl1);
            this.Name = "UserAddControl";
            this.Size = new System.Drawing.Size(419, 152);
            this.ResumeLayout(false);

        }

        #endregion

        private UserNameControl userNameControl1;
        private UserPasswordControl userPasswordControl1;
        private System.Windows.Forms.Button CreateButton;
        private System.Windows.Forms.Button CloseButton;
    }
}
