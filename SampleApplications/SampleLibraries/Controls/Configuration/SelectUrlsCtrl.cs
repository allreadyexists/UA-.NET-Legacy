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
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Opc.Ua.Client.Controls
{
    /// <summary>
    /// A control with button that displays edit array dialog.
    /// </summary>
    public partial class SelectUrlsCtrl : UserControl
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of the control.
        /// </summary>
        public SelectUrlsCtrl()
        {
            InitializeComponent();
        }
        #endregion
        
        #region Private Fields
        private event EventHandler m_UrlsChanged;
        private List<Uri> m_urls;
        #endregion
        
        #region Public Interface
        /// <summary>
        /// The list of urls.
        /// </summary>
        public List<Uri> Urls 
        {
            get 
            {
                return m_urls; 
            }

            set
            {
                if (CurrentUrlsControl != null)
                {
                    StringBuilder builder = new StringBuilder();

                    if (value != null)
                    {
                        for (int ii = 0; ii < value.Count; ii++)
                        {
                            if (builder.Length > 0)
                            {
                                builder.Append(", ");
                            }

                            builder.Append(value[ii].Scheme);

                            if (value[ii].Port > 0)
                            {
                                builder.Append(":");
                                builder.Append(value[ii].Port);
                            }
                        }
                    }

                    CurrentUrlsControl.Text = builder.ToString();
                }

                m_urls = value;
            }
        }
        
        /// <summary>
        /// Gets or sets the control that is stores with the current file path.
        /// </summary>
        public Control CurrentUrlsControl { get; set; }

        /// <summary>
        /// Raised when the profiles are changed.
        /// </summary>
        public event EventHandler UrlsChanged
        {
            add { m_UrlsChanged += value; }
            remove { m_UrlsChanged -= value; }
        }
        #endregion

        #region Event Handlers
        private void BrowseBTN_Click(object sender, EventArgs e)
        {
            if (CurrentUrlsControl == null)
            {
                return;
            }

            string[] strings = null;

            if (m_urls != null)
            {
                strings = new string[m_urls.Count];

                for (int ii = 0; ii < m_urls.Count; ii++)
                {
                    strings[ii] = m_urls[ii].ToString();
                }
            }

            strings = new EditArrayDlg().ShowDialog(strings, BuiltInType.String, false, null) as string[];

            if (strings == null)
            {
                return;
            }

            List<Uri> urls = new List<Uri>();

            for (int ii = 0; ii < strings.Length; ii++)
            {
                Uri url = Utils.ParseUri(strings[ii]);

                if (url != null)
                {
                    urls.Add(url);
                }
            }

            Urls = urls;

            if (m_UrlsChanged != null)
            {
                m_UrlsChanged(this, e);
            }
        }
        #endregion
    }
}
