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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Opc.Ua.Client;
using Opc.Ua.Client.Controls;

namespace Opc.Ua.Sample
{
    public partial class HistoryReadDetails : UserControl
    {
        public HistoryReadDetails()
        {
            InitializeComponent();
            
            QueryTypeCB.Items.Clear();
            QueryTypeCB.Items.Add("Read Raw or Modified");
        }

        private Session m_session;
        private ReadRawModifiedDetails m_details;
        
        #region Private Methods
        /// <summary>
        /// Initializes the control
        /// </summary>
        /// <param name="session"></param>
        /// <param name="details"></param>
        /// <param name="nodes"></param>
        public void Initialize(
            Session session,
            ReadRawModifiedDetails details,
            IList<ILocalNode> nodes)
        {
            m_session = session;
            m_details = details;

            StartTimeCTRL.Value = ToControlDateTime(details.StartTime);
            StartTimeSpecifiedCHK.Checked = details.StartTime != DateTime.MinValue;
            EndTimeCTRL.Value = ToControlDateTime(details.EndTime);
            EndTimeSpecifiedCHK.Checked = details.EndTime != DateTime.MinValue;
            MaxValuesCTRL.Value = details.NumValuesPerNode;
            IncludeBoundsCHK.Checked = details.ReturnBounds;
            IsModifiedCHK.Checked = details.IsReadModified;
        }
        #endregion
        
        #region Private Methods
        private DateTime ToControlDateTime(DateTime value)
        {
            if (value < new DateTime(1900,1,1))
            {
                return new DateTime(1900,1,1);
            }

            if (value > new DateTime(2100,1,1))
            {
                return new DateTime(2100,1,1);
            }

            return value;
        }

        private DateTime FromControlDateTime(DateTime value)
        {
            if (value <= new DateTime(1900,1,1))
            {
                return DateTime.MinValue;
            }

            if (value >= new DateTime(2100,1,1))
            {
                return DateTime.MaxValue;
            }

            return value;
        }
        #endregion
    }
}
