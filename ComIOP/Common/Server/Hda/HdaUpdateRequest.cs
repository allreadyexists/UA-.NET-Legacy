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
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices;
using Opc.Ua.Client;
using OpcRcw.Hda;

namespace Opc.Ua.Com.Server
{
    /// <summary>
    /// Stores information about an HDA update request.
    /// </summary>
    public class HdaUpdateRequest
    {
        /// <summary>
        /// The handle for the requested item.
        /// </summary>
        public HdaItemHandle Handle;

        /// <summary>
        /// The client handle.
        /// </summary>
        public int ClientHandle;
        
        /// <summary>
        /// The details of the request.
        /// </summary>
        public ExtensionObject Details;

        /// <summary>
        /// Any error associated with the item.
        /// </summary>
        public int Error;
    }
}
