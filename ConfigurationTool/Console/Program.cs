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
using System.Text;
using System.IO;

namespace Opc.Ua.Configuration
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // check the arguments
                if (args.Length < 2)
                {
                    Console.WriteLine("Not enough command line arguments provided.");
                    return;
                }

                // process command line.
                ConfigUtils.ProcessCommandLine(args);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected error: {0}", e.Message);
            }
            finally
            {
                // Console.ReadLine();
            }
        }

    }
}
