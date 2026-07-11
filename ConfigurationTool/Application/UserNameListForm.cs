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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Opc.Ua;
using Opc.Ua.Com.Client;


namespace Opc.Ua.Configuration
{
    /// <summary>
    /// Manage users for form.
    /// </summary>
    public partial class UserNameListForm : Form
    {
        #region Constructors
        /// <summary>
        /// Creates an form.
        /// </summary>
        public UserNameListForm(string applicationName)
        {
            InitializeComponent();

            m_applicationName = applicationName;
            m_UserNameValidator = new UserNameCreator(applicationName);

            InitializeListView();
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Initialize the ListView.
        /// </summary>
        private void InitializeListView()
        {
            // Initialize property.
            UserNameListView.MultiSelect = false;
            UserNameListView.FullRowSelect = true;
            UserNameListView.GridLines = true;
            UserNameListView.Sorting = SortOrder.Ascending;
            UserNameListView.View = View.Details;

            // Create Column.
            ColumnHeader columnName = new ColumnHeader();
            ColumnHeader columnDescription = new ColumnHeader();
            columnName.Text = "User Name";
            columnName.Width = 150;
            ColumnHeader[] colHeaderRegValue = { columnName };
            UserNameListView.Columns.AddRange(colHeaderRegValue);

            return;
        }

        /// <summary>
        /// Initialize the ListView.
        /// </summary>
        private void InitializeListViewValue()
        {
            // Clear item.
            UserNameListView.Items.Clear();

            Dictionary<string, UserNameIdentityToken> userNameTokens = UserNameCreator.LoadUserName(m_applicationName);
            foreach (UserNameIdentityToken item in userNameTokens.Values)
            {
                // Add item.
                ListViewItem listItem = new ListViewItem(item.UserName);
                listItem.Tag = item;
                UserNameListView.Items.Add(listItem);
            }
        }
        #endregion

        #region Event Handlers>
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UserNameListForm_Shown(object sender, EventArgs e)
        {
            InitializeListViewValue();
        }

        private void ListView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e == null || e.Button != MouseButtons.Right)
            {   
                return;
            }

            // Get current item.
            m_SelectedItem = UserNameListView.GetItemAt(e.X, e.Y);

            if (m_SelectedItem != null)
            {   
                UserNameIdentityToken userNameToken = m_SelectedItem.Tag as UserNameIdentityToken;

                if (userNameToken != null)
                {   // Set current item.
                    m_SelectedTolken = userNameToken;
                }

                ContextMenuVisible_NoCreate();
            }
            else
            {
                ContextMenuVisible_Create();
            }
        }

        private void ContextMenuVisible_NoCreate()
        {
            if (listContextMenuStrip != null)
            {
                createUserCToolStripMenuItem.Enabled = false;
                changePasswordPToolStripMenuItem.Enabled = true;
                deleteDToolStripMenuItem.Enabled = true;

                // Show context menu.
                listContextMenuStrip.Show(new System.Drawing.Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }

        private void ContextMenuVisible_Create()
        {
            createUserCToolStripMenuItem.Enabled = true;
            deleteDToolStripMenuItem.Enabled = false;

            listContextMenuStrip.Show(new System.Drawing.Point(Cursor.Position.X, Cursor.Position.Y));
        }

        private void ChangePasswordMI_Click(object sender, EventArgs e)
        {
            if (m_SelectedTolken != null)
            {
                UserPasswordChangeForm userInfo = new UserPasswordChangeForm();

                userInfo.UserName = m_SelectedTolken.UserName;
                userInfo.OldPassword = m_SelectedTolken.DecryptedPassword;

                if (userInfo.ShowDialog(this) == DialogResult.OK)
                {
                    m_SelectedTolken.DecryptedPassword = userInfo.NewPassword;
                    m_UserNameValidator.Delete(m_applicationName, userInfo.UserName);
                    m_UserNameValidator.Add(m_applicationName, userInfo.UserName, userInfo.NewPassword);
                }
            }
        }

        private void DeleteMI_Click(object sender, EventArgs e)
        {
            if (m_SelectedTolken != null)
            {
                m_UserNameValidator.Delete(m_applicationName, m_SelectedTolken.UserName);

                m_SelectedTolken = null;
                m_SelectedItem = null;

                InitializeListViewValue();
            }
        }

        private void CreateUserMI_Click(object sender, EventArgs e)
        {
            UserAddForm addUser = new UserAddForm();
            if (addUser.ShowDialog(this) == DialogResult.OK)
            {
                m_UserNameValidator.Add(m_applicationName, addUser.UserName, addUser.Password);
                InitializeListViewValue();
            }
        }

        #endregion

        #region Private Fields
        private string m_applicationName;
        private UserNameCreator m_UserNameValidator;
        private UserNameIdentityToken m_SelectedTolken;
        public ListViewItem m_SelectedItem { get; set; }
        #endregion
    }
}
