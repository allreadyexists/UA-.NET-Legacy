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
    /// Stores a type declaration retrieved from a server.
    /// </summary>
    public class AeEventCategory
    {
        /// <summary>
        /// The AE event type that the category belongs to.
        /// </summary>
        public int EventType { get; set; }

        /// <summary>
        /// The proxy assigned category id for the event type.
        /// </summary>
        public uint LocalId;

        /// <summary>
        /// The UA event type node id for the category.
        /// </summary>
        public NodeId TypeId { get; set; }

        /// <summary>
        /// The UA event type node id for the supertype. 
        /// </summary>
        public NodeId SuperTypeId { get; set; }

        /// <summary>
        /// A description for the event type.
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// The fully inhierited list of instance declarations for the UA event type.
        /// </summary>
        public List<AeEventAttribute> Attributes { get; set; }
    }

}
