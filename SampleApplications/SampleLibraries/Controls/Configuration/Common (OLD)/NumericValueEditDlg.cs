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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Opc.Ua.Client.Controls
{
    /// <summary>
    /// A dialog to edit a numeric value.
    /// </summary>
    public partial class NumericValueEditDlg : Form
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="NumericValueEditDlg"/> class.
        /// </summary>
        public NumericValueEditDlg()
        {
            InitializeComponent();
            this.Icon = ClientUtils.GetAppIcon();
        }
        #endregion
        
        #region Public Interface
        /// <summary>
        /// Displays the dialog.
        /// </summary>
        public object ShowDialog(object value, Type type)
        {
            if ((type == null || type == typeof(Variant)) && value != null)
            {
                type = value.GetType();
            }

            if (type == typeof(Variant))
            {
                type = typeof(double);
            }

            SetLimits(type);

            ValueCTRL.Value = Convert.ToDecimal(value);

            if (ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            return Convert.ChangeType(ValueCTRL.Value, type);
        }
        #endregion
        
        #region Private Methods
        /// <summary>
        /// Sets the limits according to the data type.
        /// </summary>
        private void SetLimits(Type type)
        {
            if (type == typeof(sbyte))
            {
                ValueCTRL.Minimum = SByte.MinValue;
                ValueCTRL.Maximum = SByte.MaxValue;
                ValueCTRL.DecimalPlaces = 0;
            }

            if (type == typeof(byte))
            {
                ValueCTRL.Minimum = Byte.MinValue;
                ValueCTRL.Maximum = Byte.MaxValue;
                ValueCTRL.DecimalPlaces = 0;
            }

            if (type == typeof(short))
            {
                ValueCTRL.Minimum = Int16.MinValue;
                ValueCTRL.Maximum = Int16.MaxValue;
                ValueCTRL.DecimalPlaces = 0;
            }

            if (type == typeof(ushort))
            {
                ValueCTRL.Minimum = UInt16.MinValue;
                ValueCTRL.Maximum = UInt16.MaxValue;
                ValueCTRL.DecimalPlaces = 0;
            }

            if (type == typeof(int))
            {
                ValueCTRL.Minimum = Int32.MinValue;
                ValueCTRL.Maximum = Int32.MaxValue;
                ValueCTRL.DecimalPlaces = 0;
            }

            if (type == typeof(uint))
            {
                ValueCTRL.Minimum = UInt32.MinValue;
                ValueCTRL.Maximum = UInt32.MaxValue;
                ValueCTRL.DecimalPlaces = 0;
            }

            if (type == typeof(long))
            {
                ValueCTRL.Minimum = Int64.MinValue;
                ValueCTRL.Maximum = Int64.MaxValue;
                ValueCTRL.DecimalPlaces = 0;
            }

            if (type == typeof(ulong))
            {
                ValueCTRL.Minimum = UInt64.MinValue;
                ValueCTRL.Maximum = UInt64.MaxValue;
                ValueCTRL.DecimalPlaces = 0;
            }

            if (type == typeof(float))
            {
                ValueCTRL.Minimum = Decimal.MinValue;
                ValueCTRL.Maximum = Decimal.MaxValue;
                ValueCTRL.DecimalPlaces = 6;
            }

            if (type == typeof(double))
            {
                ValueCTRL.Minimum = Decimal.MinValue;
                ValueCTRL.Maximum = Decimal.MaxValue;
                ValueCTRL.DecimalPlaces = 15;
            }
        }
        #endregion
    }
}
