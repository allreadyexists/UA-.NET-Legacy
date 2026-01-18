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

namespace Quickstarts.AlarmConditionServer
{
    /// <summary>
    /// Defines constants and methods used by all classes in the information model.
    /// </summary>
    public static class ModelUtils
    {
        /// <summary>
        /// The RootType for a Area node identfier.
        /// </summary>
        public const int Area = 0;

        /// <summary>
        /// The RootType for a Source node identfier.
        /// </summary>
        public const int Source = 1;

        /// <summary>
        /// Constructs a node identifier for a area.
        /// </summary>
        /// <param name="areaPath">The area path.</param>
        /// <param name="namespaceIndex">Index of the namespace that qualifies the identifier.</param>
        /// <returns>The new node identifier.</returns>
        public static NodeId ConstructIdForArea(string areaPath, ushort namespaceIndex)
        {
            ParsedNodeId parsedNodeId = new ParsedNodeId();

            parsedNodeId.RootId = areaPath;
            parsedNodeId.NamespaceIndex = namespaceIndex;
            parsedNodeId.RootType = 0;

            return parsedNodeId.Construct();
        }

        /// <summary>
        /// Constructs a NodeId for a source.
        /// </summary>
        /// <param name="sourceId">The source id.</param>
        /// <param name="namespaceIndex">Index of the namespace.</param>
        /// <returns>The new NodeId.</returns>
        public static NodeId ConstructIdForSource(string sourceId, ushort namespaceIndex)
        {
            ParsedNodeId parsedNodeId = new ParsedNodeId();

            parsedNodeId.RootId = sourceId;
            parsedNodeId.NamespaceIndex = namespaceIndex;
            parsedNodeId.RootType = 1;

            return parsedNodeId.Construct();
        }

        /// <summary>
        /// Constructs the node identifier for a component.
        /// </summary>
        /// <param name="component">The component.</param>
        /// <param name="namespaceIndex">Index of the namespace.</param>
        /// <returns>The node identifier for a component.</returns>
        public static NodeId ConstructIdForComponent(NodeState component, ushort namespaceIndex)
        {
            if (component == null)
            {
                return null;
            }

            // components must be instances with a parent.
            BaseInstanceState instance = component as BaseInstanceState;

            if (instance == null || instance.Parent == null)
            {
                return component.NodeId;
            }

            // parent must have a string identifier.
            string parentId = instance.Parent.NodeId.Identifier as string;
            
            if (parentId == null)
            {
                return null;
            }

            StringBuilder buffer = new StringBuilder();
            buffer.Append(parentId);
            
            // check if the parent is another component.
            int index = parentId.IndexOf('?');

            if (index < 0)
            {
                buffer.Append('?');
            }
            else
            {
                buffer.Append('/');
            }

            buffer.Append(component.SymbolicName);

            // return the node identifier.
            return new NodeId(buffer.ToString(), namespaceIndex);
        }
    }
}
