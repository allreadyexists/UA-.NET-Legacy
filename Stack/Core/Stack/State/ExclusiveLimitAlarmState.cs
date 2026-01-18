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
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading;
using Opc.Ua;

namespace Opc.Ua
{
    public partial class ExclusiveLimitAlarmState
    {
        #region Public Methods
        /// <summary>
        /// Sets the active state of the condition.
        /// </summary>
        /// <param name="context">The system context.</param>
        /// <param name="active">if set to <c>true</c> the condition is active.</param>
        public override void SetActiveState(
            ISystemContext context,
            bool active)
        {
            // set it inactive.
            if (!active)
            {
                SetLimitState(context, LimitAlarmStates.Inactive);
                return;
            }

            // check if the level state machine needs an initial state.
            if (this.LimitState.CurrentState.Id.Value != null)
            {
                base.SetActiveState(context, true);
                return;
            }

            // assume a high if the high limit is specified.
            if (this.HighLimit != null)
            {
                SetLimitState(context, LimitAlarmStates.High);
            }
            else
            {
                SetLimitState(context, LimitAlarmStates.Low);
            }
        }

        /// <summary>
        /// Sets the limit state of the condition.
        /// </summary>
        /// <param name="context">The system context.</param>
        /// <param name="limit">The bit masks specifying the current state.</param>
        public virtual void SetLimitState(
            ISystemContext context,
            LimitAlarmStates limit)
        {
            switch (limit)
            {
                case LimitAlarmStates.HighHigh:
                {
                    this.LimitState.SetState(context, Objects.ExclusiveLimitStateMachineType_HighHigh);
                    break;
                }

                case LimitAlarmStates.High:
                {
                    this.LimitState.SetState(context, Objects.ExclusiveLimitStateMachineType_High);
                    break;
                }

                case LimitAlarmStates.Low:
                {
                    this.LimitState.SetState(context, Objects.ExclusiveLimitStateMachineType_Low);
                    break;
                }

                case LimitAlarmStates.LowLow:
                {
                    this.LimitState.SetState(context, Objects.ExclusiveLimitStateMachineType_LowLow);
                    break;
                }

                default:
                {
                    this.LimitState.SetState(context, 0);
                    break;
                }
           }

           SetActiveEffectiveSubState(context, this.LimitState.CurrentState.Value, DateTime.UtcNow);
           base.SetActiveState(context, limit != LimitAlarmStates.Inactive);
        }
        #endregion
    }
}
