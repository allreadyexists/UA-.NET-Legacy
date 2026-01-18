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
using System.Linq;
using System.Windows.Forms;
using System.ServiceModel;
using System.Security.Cryptography.X509Certificates;

namespace Opc.Ua.Sample
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                // check that the certificate exists (doubling clicking on the file in explorer will bring up the certificate wizard).
                if (SecurityUtils.InitializeCertificate(StoreName.My, StoreLocation.CurrentUser, "My Server Name") == null)
                {
                    MessageBox.Show("Please import the 'My Server Certificate.pfx' file into the CurrentUser/Personal certificate store (password is 'password')");
                    return;
                }

                // create a self-hosted service.
                using (ServiceHost serviceHost = new ServiceHost(typeof(MyService)))
                {
                    serviceHost.Open();
                    Application.Run(new ServerForm());
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
