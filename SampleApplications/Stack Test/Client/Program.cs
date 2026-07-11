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
using System.Text;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.ComponentModel;
using System.Xml;
using System.Runtime.Serialization;

using Opc.Ua.Client.Controls;
using Opc.Ua.Sample.Controls;

namespace Opc.Ua.StackTest
{
    static class Program
    {
        static byte[] Encode(TestStackRequest request1, ServiceMessageContext context1, bool binary)
        {
            if (binary)
            {
                return BinaryEncoder.EncodeMessage(request1, context1);
            }
            else
            {
                ServiceMessageContext.ThreadContext = context1;

                MemoryStream ostrm = new MemoryStream();

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Encoding = System.Text.Encoding.UTF8;

                using (XmlWriter writer = XmlWriter.Create(ostrm, settings))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(TestStackRequest));
                    serializer.WriteObject(writer, request1);
                }

                return ostrm.ToArray();
            }
        }

        static TestStackRequest Decode(byte[] message, ServiceMessageContext context1, bool binary)
        {
            if (binary)
            {
                return (TestStackRequest)BinaryDecoder.DecodeMessage(message, null, context1);
            }
            else
            {
                ServiceMessageContext.ThreadContext = context1;

                MemoryStream istrm = new MemoryStream(message);
                XmlReaderSettings settings = new XmlReaderSettings();

                using (XmlReader reader = XmlReader.Create(istrm, settings))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(TestStackRequest));
                    return (TestStackRequest)serializer.ReadObject(reader);
                }
            }
        }

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
                Application.Run(new MainForm());
            }
            catch (Exception exception)
            {
				GuiUtils.HandleException("UA StackTest Client", MethodBase.GetCurrentMethod(), exception);
            }     
        }
    }
}
