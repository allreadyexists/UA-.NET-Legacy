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
    /// Stores information about an HDA aggregate.
    /// </summary>
    public class HdaAggregate
    {
        /// <summary>
        /// The UA aggregate id.
        /// </summary>
        public NodeId RemoteId;

        /// <summary>
        /// The proxy assigned identifier for the aggregate,
        /// </summary>
        public uint LocalId;

        /// <summary>
        /// The name of the aggregate.
        /// </summary>
        public string Name;

        /// <summary>
        /// The descriptions of the aggregate.
        /// </summary>
        public string Description;
    }
}
