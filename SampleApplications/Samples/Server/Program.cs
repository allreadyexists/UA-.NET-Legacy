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
using System.ServiceModel;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Security.Cryptography.X509Certificates;
using System.IO;

using Opc.Ua.Server;
using Opc.Ua.Client.Controls;
using Opc.Ua.Sample.Controls;
using Opc.Ua.Configuration;

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

            ApplicationInstance application = new ApplicationInstance();
            application.ApplicationName = "UA Sample Server";
            application.ApplicationType   = ApplicationType.Server;
            application.ConfigSectionName = "Opc.Ua.SampleServer";

            try
            {
                // process and command line arguments.
                if (application.ProcessCommandLine())
                {
                    return;
                }

                // check if running as a service.
                if (!Environment.UserInteractive)
                {
                    application.StartAsService(new SampleServer());
                    return;
                }

                // load the application configuration.
                application.LoadApplicationConfiguration(false);

                // This call registers the certificate with HTTP.SYS 
                // It must be called once after installation if the HTTPS endpoint is enabled.
                // HttpAccessRule.SetHttpsCertificate(c.SecurityConfiguration.ApplicationCertificate.Find(true), 51212, false);

                // check the application certificate.
                application.CheckApplicationInstanceCertificate(false, 2048);

                // start the server.
                application.Start(new SampleServer());

                // run the application interactively.
                Application.Run(new ServerForm(application));
            }
            catch (Exception e)
            {
                ExceptionDlg.Show(application.ApplicationName, e);
                return;
            }
        }
    }
}
