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
using System.Windows.Forms;
using System.Text;
using Opc.Ua;
using Opc.Ua.Client;

namespace Opc.Ua.Client.Controls
{
    /// <summary>
    /// Prompts the user to select an area to use as an event filter.
    /// </summary>
    public partial class SetTypeDlg : Form
    {
        #region Constructors
        /// <summary>
        /// Creates an empty form.
        /// </summary>
        public SetTypeDlg()
        {
            InitializeComponent();
            this.Icon = ClientUtils.GetAppIcon();

            ErrorHandlingCB.Items.Add("Use Default Value");
            ErrorHandlingCB.Items.Add("Throw Exception");

            StructureTypeBTN.RootId = Opc.Ua.DataTypeIds.Structure;
            StructureTypeBTN.ReferenceTypeIds = new NodeId[] { Opc.Ua.ReferenceTypeIds.HasSubtype };
        }
        #endregion
        
        #region Private Fields
        private SetTypeResult m_result;
        private TypeInfo m_typeInfo;
        #endregion

        #region SetTypeResult Class
        /// <summary>
        /// The values updated by the dialog.
        /// </summary>
        public class SetTypeResult
        {
            /// <summary>
            /// The new type info.
            /// </summary>
            public TypeInfo TypeInfo { get; set; }

            /// <summary>
            /// The data type id for structured types.
            /// </summary>
            public NodeId DataTypeId { get; set; }

            /// <summary>
            /// The dimensions for array types.
            /// </summary>
            public int[] ArrayDimensions { get; set; }

            /// <summary>
            /// If true then the default value will be used if a conversion error occurs.
            /// </summary>
            public bool UseDefaultOnError { get; set; }
        }
        #endregion

        #region Public Interface
        /// <summary>
        /// Displays the available areas in a tree view.
        /// </summary>
        public SetTypeResult ShowDialog(TypeInfo typeInfo, int[] dimensions)
        {
            m_typeInfo = typeInfo;

            StructureTypeLB.Visible = false;
            StructureTypeTB.Visible = false;
            StructureTypeBTN.Visible = false;
            ArrayDimensionsLB.Visible = dimensions != null;
            ArrayDimensionsTB.Visible = dimensions != null;

            ErrorHandlingCB.SelectedIndex = 0;
                        
            StringBuilder builder = new StringBuilder();

            // display the current dimensions.
            if (typeInfo.ValueRank >= 0 && dimensions != null)
            {
                for (int ii = 0; ii < dimensions.Length; ii++)
                {
                    if (builder.Length > 0)
                    {
                        builder.Append(", ");
                    }

                    builder.Append(dimensions[ii]);
                }
            }

            ArrayDimensionsTB.Text = builder.ToString();

            // display the dialog.
            if (base.ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            return m_result;
        }
        #endregion
        
        #region Private Methods
        #endregion

        #region Event Handlers
        private void OkBTN_Click(object sender, EventArgs e)
        {
            try
            {
                // parse the array dimensions.
                string text = ArrayDimensionsTB.Text.Trim();
                List<int> dimensions = new List<int>();

                if (!String.IsNullOrEmpty(text))
                {
                    int dimension = 0;
                    const string digits = "0123456789";

                    for (int ii = 0; ii < text.Length; ii++)
                    {
                        if (Char.IsWhiteSpace(text, ii))
                        {
                            continue;
                        }

                        if (text[ii] == ',')
                        {
                            dimensions.Add(dimension);
                            dimension = 0;
                            continue;
                        }

                        if (!Char.IsDigit(text, ii))
                        {
                            throw new FormatException("Invalid character in array index. Use numbers seperated by commas.");
                        }

                        dimension *= 10;
                        dimension += digits.IndexOf(text[ii]);
                    }

                    dimensions.Add(dimension);
                }
                
                // save the result.
                int valueRank = (dimensions.Count < 1) ? ValueRanks.Scalar : dimensions.Count;

                m_result = new SetTypeResult();
                m_result.TypeInfo = new TypeInfo(m_typeInfo.BuiltInType, valueRank);
                m_result.ArrayDimensions = dimensions.ToArray();
                m_result.UseDefaultOnError = ErrorHandlingCB.SelectedIndex == 0;

                if (m_typeInfo.BuiltInType == BuiltInType.ExtensionObject)
                {
                    m_result.DataTypeId = StructureTypeBTN.SelectedNode;
                }

                DialogResult = DialogResult.OK;
            }
            catch (Exception exception)
            {
                ClientUtils.HandleException(this.Text, exception);
            }
        }
        #endregion
    }
}
