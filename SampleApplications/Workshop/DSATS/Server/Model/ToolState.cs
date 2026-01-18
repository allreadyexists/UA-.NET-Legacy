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
using System.Linq;
using System.Text;
using Opc.Ua;

namespace DsatsDemo
{
    public partial class ToolState
    {
        #region Public Methods
        /// <summary>
        /// Sets the lock for the phase.
        /// </summary>
        public void SetPhaseLock(NodeId phaseId, NodeId lockId)
        {
            if (m_mapping == null)
            {
                m_mapping = new NodeIdDictionary<NodeId>();
            }

            m_mapping[phaseId] = lockId;
        }

        /// <summary>
        /// Gets the lock for the phase.
        /// </summary>
        public NodeId GetLockForPhase(NodeId phaseId)
        {
            if (m_mapping != null)
            {
                NodeId lockId = null;

                if (m_mapping.TryGetValue(phaseId, out lockId))
                {
                    return lockId;
                }
            }

            return null;
        }
        #endregion

        #region Private Fields
        private NodeIdDictionary<NodeId> m_mapping;
        #endregion
    }
}
