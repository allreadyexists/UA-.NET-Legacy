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
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.Serialization;
using System.Xml;
using System.IO;
using System.Diagnostics;

namespace Opc.Ua.Bindings
{
    /// <summary>
    /// A writer used to serializing the body of a InvokeService request or response.
    /// </summary>
    public class InvokeServiceBodyWriter : BodyWriter
    {
        /// <summary>
        /// Stores the buffer for writing.
        /// </summary>
        public InvokeServiceBodyWriter(byte[] data, bool isRequest) : base(true)
        {
            m_data = data;
            m_isRequest = isRequest;
        }

        /// <summary>
        /// Writes the body to the stream.
        /// </summary>
        protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
        {
            if (m_isRequest)
            {
                writer.WriteStartElement("InvokeServiceRequest", Namespaces.OpcUaXsd);
            }
            else
            {
                writer.WriteStartElement("InvokeServiceResponse", Namespaces.OpcUaXsd);
            }

            if (m_data != null)
            {
                writer.WriteBase64(m_data, 0, m_data.Length);
            }

            writer.WriteEndElement();
        }

        #region Private Fields
        private byte[] m_data = null;
        private bool m_isRequest = false;
        #endregion
    }

    #region Class MessageProperties
    /// <summary>
    /// String constants using for message properties.
    /// </summary>
    public static class MessageProperties
    {
        /// <summary>
        /// The body of the request message.
        /// </summary>
        public const string RequestBody = "RB";

        /// <summary>
        /// The encoded message body.
        /// </summary>
        public const string EncodedBody = "EB";

        /// <summary>
        /// The unencoded message body.
        /// </summary>
        public const string UnencodedBody = "UB";
    }
    #endregion
}
