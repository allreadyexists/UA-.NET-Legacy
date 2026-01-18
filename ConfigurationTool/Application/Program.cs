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
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.ComponentModel;

using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Client.Controls;

namespace Opc.Ua.Configuration
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
                
            // check if running in command line mode.
            string[] args = Environment.GetCommandLineArgs();
            
            try
            {
                if (args.Length > 1)
                {
                    if (ConfigUtils.ProcessCommandLine())
                    {
                        return;
                    }
                }

                ApplicationConfiguration configuration = GuiUtils.DoStartupChecks(
                    "Opc.Ua.ConfigurationTool", 
                    ApplicationType.Client,
                    "Opc.Ua.ConfigurationTool.Config.xml",
                    false);

                if (configuration != null)
                {
                    Application.Run(new MainForm(configuration));
                }
            }
            catch (Exception e)
            {
                GuiUtils.HandleException(Utils.Format(
                    "UA Certificate Tool: {0} {1}", 
                    (args.Length > 1)?args[1]:null,                    
                    (args.Length > 2)?args[2]:null), 
                    null,
                    e);
            }
        }
    }
}
