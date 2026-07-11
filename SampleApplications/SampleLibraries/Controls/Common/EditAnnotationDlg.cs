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
using System.Drawing;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Opc.Ua;
using Opc.Ua.Client;

namespace Opc.Ua.Client.Controls
{
    /// <summary>
    /// Prompts the user to edit a value.
    /// </summary>
    public partial class EditAnnotationDlg : Form
    {
        #region Constructors
        /// <summary>
        /// Creates an empty form.
        /// </summary>
        public EditAnnotationDlg()
        {
            InitializeComponent();
        }
        #endregion

        #region Private Fields
        private Session m_session;
        #endregion

        #region Public Interface
        /// <summary>
        /// Prompts the user to edit an annotation.
        /// </summary>
        public Annotation ShowDialog(Session session, Annotation annotation, string caption)
        {
            if (caption != null)
            {
                this.Text = caption;
            }

            m_session = session;

            if (annotation == null)
            {
                annotation = new Annotation();
                annotation.AnnotationTime = DateTime.UtcNow;
                annotation.UserName = Environment.GetEnvironmentVariable("USERNAME");
                annotation.Message = "<insert your message here>";
            }

            AnnotationTimeDP.Value = annotation.AnnotationTime;
            UserNameTB.Text = annotation.UserName;
            CommentTB.Text = annotation.Message;

            if (ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            annotation = new Annotation();
            annotation.AnnotationTime = AnnotationTimeDP.Value;
            annotation.UserName = UserNameTB.Text;
            annotation.Message = CommentTB.Text;

            return annotation;
        }
        #endregion

        #region Event Handlers
        private void OkBTN_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.OK;
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException(this.Text, exception);
            }
        }
        #endregion
    }
}
