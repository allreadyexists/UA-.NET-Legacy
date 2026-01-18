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

namespace Opc.Ua
{
    partial class ExclusiveLimitStateMachineState
    {
        #region Initialization
        /// <summary>
        /// Initializes the object as a collection of counters which change value on read.
        /// </summary>
        protected override void OnAfterCreate(ISystemContext context, NodeState node)
        {
            base.OnAfterCreate(context, node);

            UpdateStateVariable(context, Objects.ExclusiveLimitStateMachineType_High, CurrentState);
            UpdateTransitionVariable(context, 0, LastTransition);
        }
        #endregion

        #region Overriden Members
        /// <summary>
        /// The table of states belonging to the state machine.
        /// </summary>
        protected override ElementInfo[] StateTable
        {
            get { return s_StateTable; }
        }

        /// <summary>
        /// A table of valid states.
        /// </summary>
        private ElementInfo[] s_StateTable = new ElementInfo[]
        {
            new ElementInfo(Objects.ExclusiveLimitStateMachineType_HighHigh, BrowseNames.HighHigh, 1),
            new ElementInfo(Objects.ExclusiveLimitStateMachineType_High, BrowseNames.High, 2),
            new ElementInfo(Objects.ExclusiveLimitStateMachineType_Low, BrowseNames.Low, 3),
            new ElementInfo(Objects.ExclusiveLimitStateMachineType_LowLow, BrowseNames.LowLow, 4)
        };

        /// <summary>
        /// The table of transitions belonging to the state machine.
        /// </summary>
        protected override ElementInfo[] TransitionTable
        {
            get { return s_TransitionTable; }
        }

        /// <summary>
        /// A table of valid transitions.
        /// </summary>
        private ElementInfo[] s_TransitionTable = new ElementInfo[]
        {
            new ElementInfo(Objects.ExclusiveLimitStateMachineType_HighHighToHigh, BrowseNames.HighHighToHigh, 1),
            new ElementInfo(Objects.ExclusiveLimitStateMachineType_HighToHighHigh, BrowseNames.HighToHighHigh, 2),
            new ElementInfo(Objects.ExclusiveLimitStateMachineType_LowLowToLow, BrowseNames.LowLowToLow, 3),
            new ElementInfo(Objects.ExclusiveLimitStateMachineType_LowToLowLow, BrowseNames.LowToLowLow, 4)
        };

        /// <summary>
        /// The mapping between transitions and their from and to states.
        /// </summary>
        protected override uint[,] TransitionMappings
        {
            get { return s_TransitionMappings; }
        }

        /// <summary>
        /// A table of the to and from states for the transitions.
        /// </summary>
        private uint[,] s_TransitionMappings = new uint[,]
        {
            { Objects.ExclusiveLimitStateMachineType_HighHighToHigh, Objects.ExclusiveLimitStateMachineType_HighHigh, Objects.ExclusiveLimitStateMachineType_High, 0 },
            { Objects.ExclusiveLimitStateMachineType_HighToHighHigh, Objects.ExclusiveLimitStateMachineType_High, Objects.ExclusiveLimitStateMachineType_HighHigh, 0 },
            { Objects.ExclusiveLimitStateMachineType_LowLowToLow, Objects.ExclusiveLimitStateMachineType_LowLow, Objects.ExclusiveLimitStateMachineType_Low, 0 },
            { Objects.ExclusiveLimitStateMachineType_LowToLowLow, Objects.ExclusiveLimitStateMachineType_Low, Objects.ExclusiveLimitStateMachineType_LowLow, 0 }
        };
        #endregion
    }
}
