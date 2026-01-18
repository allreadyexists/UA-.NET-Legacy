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

namespace Quickstarts.PerfTestServer
{
    public static class ModelUtils
    {
        public static NodeId GetRegisterId(MemoryRegister register, ushort namespaceIndex)
        {
            return new NodeId((uint)register.Id, namespaceIndex);
        }

        public static NodeId GetRegisterVariableId(MemoryRegister register, int index, ushort namespaceIndex)
        {
            uint id = (uint)(register.Id << 24) + (uint)index;
            return new NodeId(id, namespaceIndex);
        }

        public static MemoryRegisterState GetRegister(MemoryRegister register, ushort namespaceIndex)
        {
            MemoryRegisterState node = new MemoryRegisterState(register, namespaceIndex);
            return node;
        }

        public static BaseDataVariableState GetRegisterVariable(MemoryRegister register, int index, ushort namespaceIndex)
        {
            if (index < 0 || index >= register.Size)
            {
                return null;
            }

            BaseDataVariableState<int> variable = new BaseDataVariableState<int>(null);

            variable.NodeId = GetRegisterVariableId(register, index, namespaceIndex);
            variable.BrowseName = new QualifiedName(Utils.Format("{0:000000}", index), namespaceIndex);
            variable.DisplayName = variable.BrowseName.Name;
            variable.Value = register.Read(index);
            variable.DataType = DataTypeIds.Int32;
            variable.ValueRank = ValueRanks.Scalar;
            variable.MinimumSamplingInterval = 100;
            variable.AccessLevel = AccessLevels.CurrentReadOrWrite;
            variable.UserAccessLevel = AccessLevels.CurrentReadOrWrite;
            variable.Handle = register;
            variable.NumericId = (uint)index;

            return variable;
        }
    }

    public class MemoryRegisterState : FolderState
    {
        public MemoryRegisterState(MemoryRegister register, ushort namespaceIndex) : base(null)
        {
            m_register = register;

            this.NodeId = new NodeId((uint)register.Id, namespaceIndex);
            this.BrowseName = new QualifiedName(register.Name, namespaceIndex);
            this.DisplayName = this.BrowseName.Name;

            this.AddReference(ReferenceTypeIds.Organizes, true, ObjectIds.ObjectsFolder);
        }

        public MemoryRegister Register
        {
            get { return m_register; }
        }

        public override INodeBrowser CreateBrowser(
            ISystemContext context, 
            ViewDescription view, 
            NodeId referenceType, 
            bool includeSubtypes, 
            BrowseDirection browseDirection, 
            QualifiedName browseName, 
            IEnumerable<IReference> additionalReferences, 
            bool internalOnly)
        {
            MemoryRegisterBrowser browser = new MemoryRegisterBrowser(
                context,
                view,
                referenceType,
                includeSubtypes,
                browseDirection,
                browseName,
                additionalReferences,
                internalOnly,
                this);

            return browser;
        }

        private MemoryRegister m_register;
    }

    /// <summary>
    /// Browses the children of a segment.
    /// </summary>
    public class MemoryRegisterBrowser : NodeBrowser
    {
        #region Constructors
        /// <summary>
        /// Creates a new browser object with a set of filters.
        /// </summary>
        public MemoryRegisterBrowser(
            ISystemContext context,
            ViewDescription view,
            NodeId referenceType,
            bool includeSubtypes,
            BrowseDirection browseDirection,
            QualifiedName browseName,
            IEnumerable<IReference> additionalReferences,
            bool internalOnly,
            MemoryRegisterState parent)
        :
            base(
                context,
                view,
                referenceType,
                includeSubtypes,
                browseDirection,
                browseName,
                additionalReferences,
                internalOnly)
        {
            m_parent = parent;
            m_stage = Stage.Begin;
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Returns the next reference.
        /// </summary>
        /// <returns>The next reference that meets the browse criteria.</returns>
        public override IReference Next()
        {
            UnderlyingSystem system = (UnderlyingSystem)this.SystemContext.SystemHandle;

            lock (DataLock)
            {
                IReference reference = null;

                // enumerate pre-defined references.
                // always call first to ensure any pushed-back references are returned first.
                reference = base.Next();

                if (reference != null)
                {
                    return reference;
                }

                if (m_stage == Stage.Begin)
                {
                    m_stage = Stage.Tags;
                    m_position = 0;
                }

                // don't start browsing huge number of references when only internal references are requested.
                if (InternalOnly)
                {
                    return null;
                }

                // enumerate tags.
                if (m_stage == Stage.Tags)
                {
                    if (IsRequired(ReferenceTypeIds.Organizes, false))
                    {
                        reference = NextChild();

                        if (reference != null)
                        {
                            return reference;
                        }
                    }
                }

                // all done.
                return null;
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Returns the next child.
        /// </summary>
        private IReference NextChild()
        {
            UnderlyingSystem system = (UnderlyingSystem)this.SystemContext.SystemHandle;

            NodeId targetId = null;

            // check if a specific browse name is requested.
            if (!QualifiedName.IsNull(base.BrowseName))
            {
                // browse name must be qualified by the correct namespace.
                if (m_parent.BrowseName.NamespaceIndex != base.BrowseName.NamespaceIndex)
                {
                    return null;
                }

                // parse the browse name.
                int index = 0;

                for (int ii = 0; ii < base.BrowseName.Name.Length; ii++)
                {
                    char ch = base.BrowseName.Name[ii];

                    if (!Char.IsDigit(ch))
                    {
                        return null;
                    }

                    index *= 10;
                    index += Convert.ToInt32(ch - '0');
                }

                // check for valid browse name.
                if (index < 0 || index > m_parent.Register.Size)
                {
                    return null;
                }

                // return target.
                targetId = ModelUtils.GetRegisterVariableId(m_parent.Register, index, m_parent.NodeId.NamespaceIndex);
            }

            // return the child at the next position.
            else
            {
                // look for next segment.
                if (m_position >= m_parent.Register.Size)
                {
                    return null;
                }

                // return target.
                targetId = ModelUtils.GetRegisterVariableId(m_parent.Register, m_position, m_parent.NodeId.NamespaceIndex);
                m_position++;
            }

            // create reference.
            if (targetId != null)
            {
                return new NodeStateReference(ReferenceTypeIds.Organizes, false, targetId);
            }

            return null;
        }
        #endregion

        #region Stage Enumeration
        /// <summary>
        /// The stages available in a browse operation.
        /// </summary>
        private enum Stage
        {
            Begin,
            Tags,
            Done
        }
        #endregion

        #region Private Fields
        private Stage m_stage;
        private int m_position;
        private MemoryRegisterState m_parent;
        #endregion
    }
}
