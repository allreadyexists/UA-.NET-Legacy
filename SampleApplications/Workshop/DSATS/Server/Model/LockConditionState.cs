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
using System.Security.Cryptography.X509Certificates;
using Opc.Ua;

namespace DsatsDemo
{
    public partial class LockConditionState
    {
        #region Public Methods
        /// <summary>
        /// Requests the lock.
        /// </summary>
        public void RequestLock(ISystemContext context)
        {
            TranslationInfo state = new TranslationInfo(
                "LockConditionStateWaitingForApproval",
                "en-US",
                BrowseNames.WaitingForApproval);

            if (this.LockState.CurrentState != null)
            {
                this.LockState.CurrentState.Value = new LocalizedText(state);
                this.LockState.CurrentState.Id.Value = ExpandedNodeId.ToNodeId(ObjectIds.LockStateMachineType_WaitingForApproval, context.NamespaceUris);
            }

            this.LockStateAsString.Value = state.Text;

            UpdateEffectiveState(context);
        }

        /// <summary>
        /// Grants the lock.
        /// </summary>
        public void SetLock(ISystemContext context)
        {
            TranslationInfo state = new TranslationInfo(
                "LockConditionStateLocked",
                "en-US",
                BrowseNames.Locked);

            if (this.LockState.CurrentState != null)
            {
                this.LockState.CurrentState.Value = new LocalizedText(state);
                this.LockState.CurrentState.Id.Value = ExpandedNodeId.ToNodeId(ObjectIds.LockStateMachineType_Locked, context.NamespaceUris);
            }

            this.LockStateAsString.Value = state.Text;

            UpdateEffectiveState(context);
        }

        /// <summary>
        /// Releases the lock.
        /// </summary>
        public void Unlock(ISystemContext context)
        {
            TranslationInfo state = new TranslationInfo(
                "LockConditionStateUnlocked",
                "en-US",
                BrowseNames.Unlocked);

            if (this.LockState.CurrentState != null)
            {
                this.LockState.CurrentState.Value = new LocalizedText(state);
                this.LockState.CurrentState.Id.Value = ExpandedNodeId.ToNodeId(ObjectIds.LockStateMachineType_Unlocked, context.NamespaceUris);
            }

            this.LockStateAsString.Value = state.Text;

            UpdateEffectiveState(context);
        }

        /// <summary>
        /// Specifies the thumbprint of a certificate that has access to the lock.
        /// </summary>
        public void SetPermission(string thumbprint)
        {
            if (m_thumbprints == null)
            {
                m_thumbprints = new List<string>();
            }

            m_thumbprints.Add(thumbprint);
        }

        /// <summary>
        /// Checks if the certificate has access to the lock.
        /// </summary>
        public bool HasPermission(X509Certificate2 certificate)
        {
            if (m_thumbprints != null)
            {
                return m_thumbprints.Contains(certificate.Thumbprint);
            }

            return false;
        }
        #endregion

        #region Public Methods
        private List<string> m_thumbprints;
        #endregion
    }
}
