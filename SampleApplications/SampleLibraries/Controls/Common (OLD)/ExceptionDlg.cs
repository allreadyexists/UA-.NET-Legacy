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
using System.Text;
using System.Windows.Forms;

namespace Opc.Ua.Client.Controls
{
    /// <summary>
    /// A dialog that displays an exception trace in an HTML page.
    /// </summary>
    public partial class ExceptionDlg : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionDlg"/> class.
        /// </summary>
        public ExceptionDlg()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Replaces all special characters in the message.
        /// </summary>
        private string ReplaceSpecialCharacters(string message)
        {
            message = message.Replace("&", "&#38;");
            message = message.Replace("<", "&lt;");
            message = message.Replace(">", "&gt;");
            message = message.Replace("\"", "&#34;");
            message = message.Replace("'", "&#39;");
            message = message.Replace("\r\n", "<br/>");

            return message;
        }

        /// <summary>
        /// Display the exception in the dialog.
        /// </summary>
        public void ShowDialog(string caption, Exception e)
        {
            Text = caption;
   
            StringBuilder buffer = new StringBuilder();

            buffer.Append("<html><body style='margin:0'>");

            while (e != null)
            {
                string message = e.Message;
                
                ServiceResultException exception = e as ServiceResultException;

                if (exception != null)
                {
                    message = exception.ToLongString();
                }
                
                message = ReplaceSpecialCharacters(message);

                if (exception != null)
                {
                    buffer.Append("<p>");
                    buffer.Append("<font style='font:9pt/12pt verdana;color:black'>");
                    buffer.Append(message);
                    buffer.Append("</font>");
                    buffer.Append("</p>");
                }
                else
                {
                    buffer.Append("<font style='font:9pt/12pt verdana;color:red'><b>");
                    buffer.Append(message);
                    buffer.Append("</b></font><br>");
                }

                message = e.StackTrace;

                if (!String.IsNullOrEmpty(message))
                {
                    message = ReplaceSpecialCharacters(message);

                    buffer.Append("<p>");
                    buffer.Append("<font style='font:9pt/12pt verdana;color:black'>");
                    buffer.Append(message);
                    buffer.Append("</font>");
                    buffer.Append("</p>");
                }

                e = e.InnerException;
            }
            
            buffer.Append("</body></html>");
            
            ExceptionBrowser.DocumentText = buffer.ToString();

            ShowDialog();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
