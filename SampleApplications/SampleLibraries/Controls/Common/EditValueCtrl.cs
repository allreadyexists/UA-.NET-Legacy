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
using Opc.Ua;

namespace Opc.Ua.Client.Controls
{
    /// <summary>
    /// A control which is used to edit a value.
    /// </summary>
    public partial class EditValueCtrl : UserControl
    {
        /// <summary>
        /// Initializes the object.
        /// </summary>
        public EditValueCtrl()
        {
            InitializeComponent();
        }

        private Variant m_value;
        private bool m_textChanged;

        /// <summary>
        /// The data type of the value to edit.
        /// </summary>
        public TypeInfo TargetType { get; set; }

        /// <summary>
        /// The value being edited in the control.
        /// </summary>
        public Variant Value 
        {
            get
            {
                return GetValue();
            }

            set
            {
                SetValue(value);
            }
        }

        /// <summary>
        /// Returns the value shown in the control.
        /// </summary>
        private Variant GetValue()
        {
            TypeInfo sourceType = m_value.TypeInfo;

            // check if the value needs to be updated.
            if (m_textChanged)
            {
                object value = TypeInfo.Cast(ValueTB.Text, TypeInfo.Scalars.String, sourceType.BuiltInType);
                m_value = new Variant(value, sourceType);
            }

            return m_value;
        }

        /// <summary>
        /// Sets the value shown in the control.
        /// </summary>
        private void SetValue(Variant value)
        {
            // check for null.
            if (Variant.Null == value)
            {
                ValueTB.Text = String.Empty;
                ValueTB.Enabled = true;
                m_value = Variant.Null;
                return;
            }

            // get the source type.
            TypeInfo sourceType = value.TypeInfo;

            if (sourceType == null)
            {
                sourceType = TypeInfo.Construct(value.Value);
            }

            // convert to target type.
            if (TargetType != null && TargetType.BuiltInType != sourceType.BuiltInType)
            {
                m_value = new Variant(TypeInfo.Cast(value.Value, sourceType, TargetType.BuiltInType), TargetType);
                sourceType = TargetType;
            }
            else
            {
                m_value = new Variant(value.Value, sourceType);
            }

            m_textChanged = false;

            // display arrays and structures as read only strings.
            if (sourceType.ValueRank >= 0 || sourceType.BuiltInType == BuiltInType.ExtensionObject)
            {
                ValueTB.Text = m_value.ToString();
                ValueTB.Enabled = false;
                return;
            }

            // display as editable text.
            ValueTB.Text = (string)TypeInfo.Cast(m_value.Value, sourceType, BuiltInType.String);
            ValueTB.Enabled = true;
        }

        private void ValueTB_TextChanged(object sender, EventArgs e)
        {
            m_textChanged = true;
        }

        private void ValueBTN_Click(object sender, EventArgs e)
        {

        }
    }
}
