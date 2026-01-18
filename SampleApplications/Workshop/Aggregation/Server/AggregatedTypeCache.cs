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
using Opc.Ua;
using Opc.Ua.Server;

namespace AggregationServer
{
    /// <summary>
    /// Stores the type information provided by the AE server.
    /// </summary>
    internal class AggregatedTypeCache
    {
        /// <summary>
        /// A table of node reprenting the AE event catgories and condtions.
        /// </summary>
        public NodeIdDictionary<ReferenceDescription> TypeNodes { get; set; }

        /// <summary>
        /// Fetches the event type information from the AE server.
        /// </summary>
        public void LoadTypes(Opc.Ua.Client.Session client, IServerInternal server, NamespaceMapper mapper)
        {
            TypeNodes = new NodeIdDictionary<ReferenceDescription>();
            LoadTypes(client, server, mapper, Opc.Ua.ObjectTypeIds.BaseObjectType);
            LoadTypes(client, server, mapper, Opc.Ua.VariableTypeIds.BaseVariableType);
            LoadTypes(client, server, mapper, Opc.Ua.DataTypeIds.BaseDataType);
            LoadTypes(client, server, mapper, Opc.Ua.ReferenceTypeIds.References);
        }

        /// <summary>
        /// Fetches the event categories for the specified event type.
        /// </summary>
        private void LoadTypes(Opc.Ua.Client.Session client, IServerInternal server, NamespaceMapper mapper, NodeId parentId)
        {
            List<ReferenceDescription> references = null;

            // find references to subtypes.
            try
            {
                references = BrowseSubTypes(client, parentId);
            }
            catch (Exception e)
            {
                Utils.Trace("Could not browse subtypes of {0}. {1}", parentId, e.Message);
                return;
            }

            for (int ii = 0; ii < references.Count; ii++)
            {
                ReferenceDescription reference = references[ii];

                // ignore absolute references.
                if (reference.NodeId == null || reference.NodeId.IsAbsolute)
                {
                    continue;
                }

                // recursively browse until a non-UA node is found.
                if (reference.NodeId.NamespaceIndex == 0)
                {
                    LoadTypes(client, server, mapper, (NodeId)reference.NodeId);
                    continue;
                }

                // map the node id and browse name to local indexes.
                NodeId targetId = mapper.ToLocalId((NodeId)reference.NodeId);

                reference.NodeId = targetId;
                reference.BrowseName = mapper.ToLocalName(reference.BrowseName);

                // add non-UA node to the table.
                TypeNodes[targetId] = reference;
                server.TypeTree.AddSubtype(targetId, parentId);
            }
        }

        /// <summary>
        /// Fetches the subtypes for the node.
        /// </summary>
        private List<ReferenceDescription> BrowseSubTypes(Opc.Ua.Client.Session client, NodeId nodeId)
        {
            List<ReferenceDescription> references = new List<ReferenceDescription>();

            // specify the references to follow and the fields to return.
            BrowseDescription nodeToBrowse = new BrowseDescription();

            nodeToBrowse.NodeId = nodeId;
            nodeToBrowse.ReferenceTypeId = ReferenceTypeIds.HasSubtype;
            nodeToBrowse.IncludeSubtypes = true;
            nodeToBrowse.BrowseDirection = BrowseDirection.Forward;
            nodeToBrowse.NodeClassMask = 0;
            nodeToBrowse.ResultMask = (uint)BrowseResultMask.All;

            BrowseDescriptionCollection nodesToBrowse = new BrowseDescriptionCollection();
            nodesToBrowse.Add(nodeToBrowse);

            // start the browse operation.
            BrowseResultCollection results = null;
            DiagnosticInfoCollection diagnosticInfos = null;

            ResponseHeader responseHeader = client.Browse(
                null,
                null,
                0,
                nodesToBrowse,
                out results,
                out diagnosticInfos);

            // these do sanity checks on the result - make sure response matched the request.
            ClientBase.ValidateResponse(results, nodesToBrowse);
            ClientBase.ValidateDiagnosticInfos(diagnosticInfos, nodesToBrowse);

            // check status.
            if (StatusCode.IsBad(results[0].StatusCode))
            {
                // embed the diagnostic information in a exception.
                throw ServiceResultException.Create(results[0].StatusCode, 0, diagnosticInfos, responseHeader.StringTable);
            }

            // add first batch.
            references.AddRange(results[0].References);

            // check if server limited the results.
            while (results[0].ContinuationPoint != null && results[0].ContinuationPoint.Length > 0)
            {
                ByteStringCollection continuationPoints = new ByteStringCollection();
                continuationPoints.Add(results[0].ContinuationPoint);

                // continue browse operation.
                responseHeader = client.BrowseNext(
                    null,
                    false,
                    continuationPoints,
                    out results,
                    out diagnosticInfos);

                ClientBase.ValidateResponse(results, continuationPoints);
                ClientBase.ValidateDiagnosticInfos(diagnosticInfos, continuationPoints);

                // check status.
                if (StatusCode.IsBad(results[0].StatusCode))
                {
                    // embed the diagnostic information in a exception.
                    throw ServiceResultException.Create(results[0].StatusCode, 0, diagnosticInfos, responseHeader.StringTable);
                }

                // add next batch.
                references.AddRange(results[0].References);
            }

            return references;
        }
    }
}
