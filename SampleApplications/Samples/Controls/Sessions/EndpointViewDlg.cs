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
using System.ServiceModel;
using System.Reflection;
using System.IdentityModel.Claims;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;
using System.ServiceModel.Channels;

using Opc.Ua.Bindings;

namespace Opc.Ua.Sample.Controls
{
    /// <summary>
    /// Prompts the user to create a new secure channel.
    /// </summary>
    public partial class EndpointViewDlg : Form
    {
        public EndpointViewDlg()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Displays the dialog.
        /// </summary>
        public bool ShowDialog(EndpointDescription endpoint)
        {
            if (endpoint == null) throw new ArgumentNullException("endpoint");
        
            EndpointTB.Text   = endpoint.EndpointUrl;
            ServerNameTB.Text = endpoint.Server.ApplicationName.Text;
            ServerUriTB.Text  = endpoint.Server.ApplicationUri;

            try
            {
                X509Certificate2 certificate = CertificateFactory.Create(endpoint.ServerCertificate, true);
                ServerCertificateTB.Text = String.Format("{0}", certificate.Subject);
            }
            catch
            {
                ServerCertificateTB.Text = "<bad certificate>";
            }
                
           
            SecurityModeTB.Text      = String.Format("{0}", endpoint.SecurityMode);;
            SecurityPolicyUriTB.Text = String.Format("{0}", endpoint.SecurityPolicyUri);
            
            UserIdentityTypeTB.Text = "";

            foreach (UserTokenPolicy policy in endpoint.UserIdentityTokens)
            {
                UserIdentityTypeTB.Text += String.Format("{0} ", policy.TokenType);
            }

            if (ShowDialog() != DialogResult.OK)
            {
                return false;
            }
                       
            return true;
        }
    }
}
