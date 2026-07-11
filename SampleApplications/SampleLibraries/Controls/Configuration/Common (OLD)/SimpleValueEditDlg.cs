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
using Opc.Ua;
using Opc.Ua.Client.Controls;


namespace Opc.Ua.Client.Controls
{
	/// <summary>
	/// 
	/// </summary>
    public partial class SimpleValueEditDlg : Form
    {
        #region Constructors
		/// <summary>
		/// Default constructor
		/// </summary>
        public SimpleValueEditDlg()
        {
            InitializeComponent();
            this.Icon = ClientUtils.GetAppIcon();
        }
        #endregion
        
        #region Private Fields
        private object m_value;
        private Type m_type;
        #endregion
        
        #region Public Interface
        /// <summary>
        /// Displays the dialog.
        /// </summary>
        public object ShowDialog(object value, Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            m_type = type;

            this.Text = Utils.Format("{0} ({1})", this.Text, type.Name);

            ValueTB.Text = Utils.Format("{0}", value);

            if (ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            return m_value;
        }
        
        /// <summary>
        /// Returns true if the dialog supports editing the type.
        /// </summary>
        public static bool IsSimpleType(Type type)
        {
            if (type == typeof(bool))     return true;
            if (type == typeof(sbyte))    return true;
            if (type == typeof(byte))     return true;
            if (type == typeof(short))    return true;
            if (type == typeof(ushort))   return true;
            if (type == typeof(int))      return true;
            if (type == typeof(uint))     return true;
            if (type == typeof(long))     return true;
            if (type == typeof(ulong))    return true;
            if (type == typeof(float))    return true;
            if (type == typeof(double))   return true;
            if (type == typeof(string))   return true;
            if (type == typeof(DateTime)) return true;
            if (type == typeof(Guid))     return true;

            return false;
        }
        #endregion
        
        private object Parse(string text)
        {
            if (m_type == typeof(bool))          return Convert.ToBoolean(text);
            if (m_type == typeof(sbyte))         return Convert.ToSByte(text);
            if (m_type == typeof(byte))          return Convert.ToByte(text);
            if (m_type == typeof(short))         return Convert.ToInt16(text);
            if (m_type == typeof(ushort))        return Convert.ToUInt16(text);
            if (m_type == typeof(int))           return Convert.ToInt32(text);
            if (m_type == typeof(uint))          return Convert.ToUInt32(text);
            if (m_type == typeof(long))          return Convert.ToInt64(text);
            if (m_type == typeof(ulong))         return Convert.ToUInt64(text);
            if (m_type == typeof(float))         return Convert.ToSingle(text);
            if (m_type == typeof(double))        return Convert.ToDouble(text);
            if (m_type == typeof(string))        return text;
            if (m_type == typeof(DateTime))      return DateTime.Parse(text);
            if (m_type == typeof(Guid))          return new Guid(text);
            if (m_type == typeof(QualifiedName)) return new QualifiedName(text);
            if (m_type == typeof(LocalizedText)) return new LocalizedText(text);

            throw new ServiceResultException(StatusCodes.BadUnexpectedError, "Cannot convert type.");
        }

        #region Event Handlers
        private void OkBTN_Click(object sender, EventArgs e)
        {        
            try
            {
                m_value = Parse(ValueTB.Text);
                DialogResult = DialogResult.OK;
            }
            catch (Exception exception)
            {
				GuiUtils.HandleException(this.Text, MethodBase.GetCurrentMethod(), exception);
            }
        }
        #endregion
    }
}
