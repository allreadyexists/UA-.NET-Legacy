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

using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Opc.Ua;
using Opc.Ua.Client;

namespace Quickstarts.AlarmConditionClient
{
    /// <summary>
    /// Displays all fields associated with an event notification.
    /// </summary>
    public partial class ViewEventDetailsDlg : Form
    {
        #region Constructors
        /// <summary>
        /// Creates an empty form.
        /// </summary>
        public ViewEventDetailsDlg()
        {
            InitializeComponent();
        }
        #endregion
        
        #region Private Fields
        #endregion
        
        #region Public Interface
        /// <summary>
        /// Shows all fields for the current condition.
        /// </summary>
        public bool ShowDialog(MonitoredItem monitoredItem, EventFieldList eventFields)
        {
            // build a sorted list of non-null fields.
            List<string> fieldNames = new List<string>();
            List<Variant> fieldValues = new List<Variant>();

            // use the filter from the monitored item to determine what is in each field.
            EventFilter filter = monitoredItem.Status.Filter as EventFilter;

            if (filter != null)
            {
                if (eventFields.EventFields[0].Value != null)
                {
                    fieldNames.Add("ConditionId");
                    fieldValues.Add(eventFields.EventFields[0]);
                }

                for (int ii = 1; ii < filter.SelectClauses.Count; ii++)
                {
                    object fieldValue = eventFields.EventFields[ii].Value;

                    if (fieldValue == null)
                    {
                        continue;
                    }

                    StringBuilder displayName = new StringBuilder();

                    for (int jj = 0; jj < filter.SelectClauses[ii].BrowsePath.Count; jj++)
                    {
                        if (displayName.Length > 0)
                        {
                            displayName.Append('/');
                        }

                        displayName.Append(filter.SelectClauses[ii].BrowsePath[jj].Name);
                    }

                    fieldNames.Add(displayName.ToString());
                    fieldValues.Add(eventFields.EventFields[ii]);
                }
            }

            // populate lists.
            for (int ii = 0; ii < fieldNames.Count; ii++)
            {
                ListViewItem item = new ListViewItem(fieldNames[ii]);

                item.SubItems.Add(Utils.Format("{0}", fieldValues[ii].Value));
                item.SubItems.Add(Utils.Format("{0}", fieldValues[ii].Value.GetType().Name));

                FieldsLV.Items.Add(item);
            }

            // adjust columns.
            for (int ii = 0; ii < FieldsLV.Columns.Count; ii++)
            {
                FieldsLV.Columns[ii].Width = -2;
            }

            // display the dialog.
            if (ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
