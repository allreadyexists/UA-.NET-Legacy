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
using System.Xml;
using System.IO;
using System.Reflection;
using System.Threading;
using Opc.Ua;
using Opc.Ua.Server;

namespace MemoryBuffer
{
    public partial class MemoryTagState
    {
        #region Constructors
        /// <summary>
        /// Initializes a memory tag for a buffer.
        /// </summary>
        /// <param name="parent">The buffer that owns the tag.</param>
        /// <param name="offet">The offset of the tag address in the memory buffer.</param>
        public MemoryTagState(MemoryBufferState parent, uint offet) : base(parent)
        {
            // these objects are created an discarded during each operation. 
            // the metadata is derived from the parameters passed to constructors.
            NodeId = new NodeId(Utils.Format("{0}[{1}]", parent.SymbolicName, offet), parent.NodeId.NamespaceIndex);
            BrowseName = new QualifiedName(Utils.Format("{1:X8}", parent.SymbolicName, offet), parent.TypeDefinitionId.NamespaceIndex);
            DisplayName = BrowseName.Name;
            Description = null;
            WriteMask = AttributeWriteMask.None;
            UserWriteMask = AttributeWriteMask.None;
            ReferenceTypeId = Opc.Ua.ReferenceTypeIds.HasComponent;
            TypeDefinitionId = new NodeId(VariableTypes.MemoryTagType, parent.TypeDefinitionId.NamespaceIndex);
            ModellingRuleId = null;
            NumericId = offet;
            DataType = new NodeId((uint)parent.ElementType);
            ValueRank = ValueRanks.Scalar;
            ArrayDimensions = null;
            AccessLevel = AccessLevels.CurrentReadOrWrite;
            UserAccessLevel = AccessLevels.CurrentReadOrWrite;
            MinimumSamplingInterval = parent.MaximumScanRate;
            Historizing = false;

            // re-direct read and write operations to the parent.
            OnReadValue = parent.ReadTagValue;
            OnWriteValue = parent.WriteTagValue;

            m_offset = offet;
        }
        #endregion
        
        #region Public Properties
        /// <summary>
        /// The offset of the tag address in the memory buffer.
        /// </summary>
        public uint Offset
        {
            get { return m_offset; }
        }
        #endregion

        #region Private Fields
        private uint m_offset;
        #endregion
    }
}
