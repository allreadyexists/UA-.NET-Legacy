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
using Opc.Ua;
using Opc.Ua.Server;
using Opc.Ua.Com;

namespace Opc.Ua.Com.Client
{
    /// <summary>
    /// A class that builds NodeIds used by the DataAccess NodeManager
    /// </summary>
    public static class AeModelUtils
    {
        /// <summary>
        /// The RootType for a AE Simple Event Type node.
        /// </summary>
        public const int AeSimpleEventType = OpcRcw.Ae.Constants.SIMPLE_EVENT;

        /// <summary>
        /// The RootType for a AE Tracking Event Type node.
        /// </summary>
        public const int AeTrackingEventType = OpcRcw.Ae.Constants.TRACKING_EVENT;

        /// <summary>
        /// The RootType for a AE Condition Event Type node.
        /// </summary>
        public const int AeConditionEventType = OpcRcw.Ae.Constants.CONDITION_EVENT;

        /// <summary>
        /// The RootType for a AE Area
        /// </summary>
        public const int AeArea = 5;

        /// <summary>
        /// The RootType for an AE Source
        /// </summary>
        public const int AeSource = 6;

        /// <summary>
        /// The RootType for an AE Condition
        /// </summary>
        public const int AeCondition = 7;

        /// <summary>
        /// The RootType for a node defined by the UA server.
        /// </summary>
        public const int InternalNode = 8;

        /// <summary>
        /// The RootType for an EventType defined by the AE server.
        /// </summary>
        public const int AeEventTypeMapping = 9;

        /// <summary>
        /// The RootType for a ConditionClass defined by the AE server.
        /// </summary>
        public const int AeConditionClassMapping = 10;

        /// <summary>
        /// Constructs a NodeId from the BrowseName of an internal node.
        /// </summary>
        /// <param name="browseName">The browse name.</param>
        /// <param name="namespaceIndex">Index of the namespace.</param>
        /// <returns>The node id.</returns>
        public static NodeId ConstructIdForInternalNode(QualifiedName browseName, ushort namespaceIndex)
        {
            ParsedNodeId parsedNodeId = new ParsedNodeId();

            parsedNodeId.RootId = browseName.Name;
            parsedNodeId.NamespaceIndex = namespaceIndex;
            parsedNodeId.RootType = InternalNode;

            return parsedNodeId.Construct();
        }

        /// <summary>
        /// Constructs the id for an area.
        /// </summary>
        /// <param name="areaId">The area id.</param>
        /// <param name="namespaceIndex">Index of the namespace.</param>
        /// <returns></returns>
        public static NodeId ConstructIdForArea(string areaId, ushort namespaceIndex)
        {
            ParsedNodeId parsedNodeId = new ParsedNodeId();

            parsedNodeId.RootId = areaId;
            parsedNodeId.NamespaceIndex = namespaceIndex;
            parsedNodeId.RootType = AeArea;

            return parsedNodeId.Construct();
        }

        /// <summary>
        /// Constructs the id for a source.
        /// </summary>
        /// <param name="areaId">The area id.</param>
        /// <param name="sourceName">Name of the source.</param>
        /// <param name="namespaceIndex">Index of the namespace.</param>
        /// <returns></returns>
        public static NodeId ConstructIdForSource(string areaId, string sourceName, ushort namespaceIndex)
        {
            ParsedNodeId parsedNodeId = new ParsedNodeId();

            parsedNodeId.RootType = AeSource;
            parsedNodeId.RootId = areaId;
            parsedNodeId.NamespaceIndex = namespaceIndex;
            parsedNodeId.ComponentPath = sourceName;

            return parsedNodeId.Construct();
        }
    }
}
