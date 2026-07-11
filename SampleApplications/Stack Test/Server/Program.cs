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
using System.ComponentModel;

using Opc.Ua.Server;
using Opc.Ua.Client.Controls;
using Opc.Ua.Sample.Controls;

namespace Opc.Ua.StackTest
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

            List<TestServer> servers = new List<TestServer>();

            try
            {
                servers.Add(CreateHttpServer(10000, 1024, false));
                servers.Add(CreateTcpServer(11000, 1024, false));
                servers.Add(CreateTcpServer(11001, 2048, false));
                servers.Add(CreateTcpServer(12000, 1024, true));
                servers.Add(CreateTcpServer(12001, 2048, true));

                Application.Run(new MainForm(servers[0]));
            }
            catch (Exception e)
            {
                GuiUtils.HandleException("UA Test Server", null, e);
            }
            finally
            {
                for (int ii = 0; ii < servers.Count; ii++)
                {
                    servers[ii].Stop();
                }
            }
        }

        /// <summary>
        /// Creates and starts a HTTP server.
        /// </summary>
        private static TestServer CreateHttpServer(ushort port, ushort keySize, bool useAnsiCStack)
        {
            TestServer server = new TestServer(port);

            ApplicationConfiguration configuration = ApplicationConfiguration.Load("Opc.Ua.Server", ApplicationType.Server);

            configuration.ApplicationName = Utils.Format("UA StackTest Server (Http-{0})", keySize);
            configuration.ApplicationUri = Utils.Format("http://{0}/{1}", System.Net.Dns.GetHostName(), configuration.ApplicationName);

            string subjectName = Utils.Format("CN={1}/DC={0}", System.Net.Dns.GetHostName(), configuration.ApplicationName);

            configuration.SecurityConfiguration.ApplicationCertificate.RawData = null;
            configuration.SecurityConfiguration.ApplicationCertificate.Thumbprint = null;
            configuration.SecurityConfiguration.ApplicationCertificate.SubjectName = subjectName;

            // set base addresses.
            string url = Utils.Format("http://{0}:{1}/StackTestServer/{2}", System.Net.Dns.GetHostName(), port, keySize);

            configuration.ServerConfiguration.BaseAddresses.Clear();
            configuration.ServerConfiguration.BaseAddresses.Add(url);

            GuiUtils.CheckApplicationInstanceCertificate(configuration, keySize, false, false);

            // start server.
            server.Start(configuration);
            TestUtils.InitializeContexts(server.MessageContext);

            return server;
        }

        /// <summary>
        /// Creates and starts a TCP server.
        /// </summary>
        private static TestServer CreateTcpServer(ushort port, ushort keySize, bool useAnsiCStack)
        {
            TestServer server = new TestServer(port);

            ApplicationConfiguration configuration = ApplicationConfiguration.Load("Opc.Ua.Server", ApplicationType.Server);

            configuration.ApplicationName = Utils.Format("UA StackTest Server ({0}-{1})", (useAnsiCStack)?"AnsiC":"C#", keySize);
            configuration.ApplicationUri = Utils.Format("http://{0}/{1}", System.Net.Dns.GetHostName(), configuration.ApplicationName);

            string subjectName = Utils.Format("CN={1}/DC={0}", System.Net.Dns.GetHostName(), configuration.ApplicationName);

            configuration.SecurityConfiguration.ApplicationCertificate.RawData = null;
            configuration.SecurityConfiguration.ApplicationCertificate.Thumbprint = null;
            configuration.SecurityConfiguration.ApplicationCertificate.SubjectName = subjectName;

            // check if UA TCP configuration included.
            configuration.UseNativeStack = useAnsiCStack;

            // set base addresses.
            string url = Utils.Format("opc.tcp://{0}:{1}/StackTestServer/{2}/{3}", System.Net.Dns.GetHostName(), port, (useAnsiCStack) ? "AnsiC" : "DotNet", keySize);
            
            configuration.ServerConfiguration.BaseAddresses.Clear();
            configuration.ServerConfiguration.BaseAddresses.Add(url);

            GuiUtils.CheckApplicationInstanceCertificate(configuration, keySize, false, false);

            // start server.
            server.Start(configuration);
            TestUtils.InitializeContexts(server.MessageContext);

            return server;
        }
    }
}
