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
using System.Windows.Forms;

namespace Quickstarts.AlarmConditionClient
{
    /// <summary>
    /// Prompts the user to specify a comment to send to the server.
    /// </summary>
    public partial class AddCommentDlg : Form
    {
        #region Constructors
        /// <summary>
        /// Creates an empty form.
        /// </summary>
        public AddCommentDlg()
        {
            InitializeComponent();
        }
        #endregion
        
        #region Private Fields
        #endregion
        
        #region Public Interface
        /// <summary>
        /// Prompts the user to enter a comment.
        /// </summary>
        public string ShowDialog(string comment)
        {
            CommentTB.Text = comment;

            // display the dialog.
            if (ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            return CommentTB.Text;
        }
        #endregion
        
        #region Private Methods
        #endregion
                
        #region Event Handlers
        #endregion
    }
}
