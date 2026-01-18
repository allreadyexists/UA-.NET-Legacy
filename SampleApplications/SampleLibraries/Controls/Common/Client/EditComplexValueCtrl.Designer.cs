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

namespace Opc.Ua.Client.Controls.Common
{
    partial class EditComplexValueCtrl
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
            this.ValuesDV = new System.Windows.Forms.DataGridView();
            this.Icon = new System.Windows.Forms.DataGridViewImageColumn();
            this.NameCH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataTypeCH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValueCH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NavigationMENU = new System.Windows.Forms.MenuStrip();
            this.ImageList = new System.Windows.Forms.ImageList(this.components);
            this.TextValueTB = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ValuesDV)).BeginInit();
            this.SuspendLayout();
            // 
            // ValuesDV
            // 
            this.ValuesDV.AllowUserToAddRows = false;
            this.ValuesDV.AllowUserToDeleteRows = false;
            this.ValuesDV.AllowUserToResizeRows = false;
            this.ValuesDV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.ValuesDV.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.ValuesDV.BackgroundColor = System.Drawing.SystemColors.Window;
            this.ValuesDV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ValuesDV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Icon,
            this.NameCH,
            this.DataTypeCH,
            this.ValueCH});
            this.ValuesDV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ValuesDV.Location = new System.Drawing.Point(0, 24);
            this.ValuesDV.MultiSelect = false;
            this.ValuesDV.Name = "ValuesDV";
            this.ValuesDV.RowHeadersVisible = false;
            this.ValuesDV.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.ValuesDV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.ValuesDV.Size = new System.Drawing.Size(512, 214);
            this.ValuesDV.TabIndex = 0;
            this.ValuesDV.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.ValuesDV_CellValueChanged);
            this.ValuesDV.DoubleClick += new System.EventHandler(this.ValuesDV_DoubleClick);
            this.ValuesDV.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.ValuesDV_CellValidating);
            // 
            // Icon
            // 
            this.Icon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Icon.DataPropertyName = "Icon";
            this.Icon.HeaderText = "";
            this.Icon.Name = "Icon";
            this.Icon.ReadOnly = true;
            this.Icon.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Icon.Width = 5;
            // 
            // NameCH
            // 
            this.NameCH.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.NameCH.DataPropertyName = "Name";
            this.NameCH.HeaderText = "Name";
            this.NameCH.Name = "NameCH";
            this.NameCH.ReadOnly = true;
            this.NameCH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.NameCH.Width = 60;
            // 
            // DataTypeCH
            // 
            this.DataTypeCH.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DataTypeCH.DataPropertyName = "DataType";
            this.DataTypeCH.HeaderText = "Data Type";
            this.DataTypeCH.Name = "DataTypeCH";
            this.DataTypeCH.ReadOnly = true;
            this.DataTypeCH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.DataTypeCH.Width = 82;
            // 
            // ValueCH
            // 
            this.ValueCH.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ValueCH.DataPropertyName = "Value";
            this.ValueCH.HeaderText = "Value";
            this.ValueCH.Name = "ValueCH";
            this.ValueCH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // NavigationMENU
            // 
            this.NavigationMENU.Location = new System.Drawing.Point(0, 0);
            this.NavigationMENU.Name = "NavigationMENU";
            this.NavigationMENU.Size = new System.Drawing.Size(512, 24);
            this.NavigationMENU.TabIndex = 1;
            this.NavigationMENU.Text = "menuStrip1";
            // 
            // ImageList
            // 
            this.ImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.ImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.ImageList.TransparentColor = System.Drawing.Color.White;
            // 
            // TextValueTB
            // 
            this.TextValueTB.AcceptsReturn = true;
            this.TextValueTB.AcceptsTab = true;
            this.TextValueTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextValueTB.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextValueTB.Location = new System.Drawing.Point(0, 24);
            this.TextValueTB.Multiline = true;
            this.TextValueTB.Name = "TextValueTB";
            this.TextValueTB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TextValueTB.Size = new System.Drawing.Size(512, 214);
            this.TextValueTB.TabIndex = 2;
            this.TextValueTB.WordWrap = false;
            // 
            // EditComplexValueCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ValuesDV);
            this.Controls.Add(this.TextValueTB);
            this.Controls.Add(this.NavigationMENU);
            this.Name = "EditComplexValueCtrl";
            this.Size = new System.Drawing.Size(512, 238);
            ((System.ComponentModel.ISupportInitialize)(this.ValuesDV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ValuesDV;
        private System.Windows.Forms.MenuStrip NavigationMENU;
        private System.Windows.Forms.ImageList ImageList;
        private System.Windows.Forms.TextBox TextValueTB;
        private System.Windows.Forms.DataGridViewImageColumn Icon;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameCH;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataTypeCH;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValueCH;
    }
}
