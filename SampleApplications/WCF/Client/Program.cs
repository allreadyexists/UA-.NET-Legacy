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
using System.Security.Cryptography.X509Certificates;

using Opc.Ua;

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
                if (SecurityUtils.InitializeCertificate(StoreName.My, StoreLocation.CurrentUser, "My Client Name") == null)
                {
                    MessageBox.Show("Please import the 'My Client Certificate.pfx' file into the CurrentUser/Personal certificate store (password is 'password')");
                    return;
                }

                Application.Run(new ClientForm());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
