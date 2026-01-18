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
using System.Reflection;

using Opc.Ua.Client;
using Opc.Ua.Client.Controls;

namespace Opc.Ua.Sample.Controls
{
    /// <summary>
    /// A class that provide various common utility functions and shared resources.
    /// </summary>
    public partial class GuiUtils2 : Form
    {
        /// <summary>
        /// Displays a dialog that allows a use to edit a value.
        /// </summary>
        public static object EditValue(Session session, object value)
        {
            TypeInfo typeInfo = TypeInfo.Construct(value);

            if (typeInfo != null)
            {
                return EditValue(session, value, (uint)typeInfo.BuiltInType, typeInfo.ValueRank);
            }

            return null;
        }

        /// <summary>
        /// Displays a dialog that allows a use to edit a value.
        /// </summary>
        public static object EditValue(Session session, object value, NodeId datatypeId, int valueRank)
        {
            if (value == null)
            {
                value = GuiUtils.GetDefaultValue(datatypeId, valueRank);
            }

            if (valueRank >= 0)
            {
                return new ComplexValueEditDlg().ShowDialog(value);
            }

            BuiltInType builtinType = TypeInfo.GetBuiltInType(datatypeId, session.TypeTree);

            switch (builtinType)
            {
                case BuiltInType.Boolean:
                case BuiltInType.Byte:
                case BuiltInType.SByte:
                case BuiltInType.Int16:
                case BuiltInType.UInt16:
                case BuiltInType.Int32:
                case BuiltInType.UInt32:
                case BuiltInType.Int64:
                case BuiltInType.UInt64:
                case BuiltInType.Float:
                case BuiltInType.Double:
                case BuiltInType.Enumeration:
                {
                    return new NumericValueEditDlg().ShowDialog(value, TypeInfo.GetSystemType(builtinType, valueRank));
                }
                    
                case BuiltInType.Number:
                {
                    return new NumericValueEditDlg().ShowDialog(value, TypeInfo.GetSystemType(BuiltInType.Double, valueRank));
                }

                case BuiltInType.Integer:
                {
                    return new NumericValueEditDlg().ShowDialog(value, TypeInfo.GetSystemType(BuiltInType.Int64, valueRank));
                }

                case BuiltInType.UInteger:
                {
                    return new NumericValueEditDlg().ShowDialog(value, TypeInfo.GetSystemType(BuiltInType.UInt64, valueRank));
                }

                case BuiltInType.NodeId:
                {
                    return new NodeIdValueEditDlg().ShowDialog(session, (NodeId)value);
                }

                case BuiltInType.ExpandedNodeId:
                {
                    return new NodeIdValueEditDlg().ShowDialog(session, (ExpandedNodeId)value);
                }

                case BuiltInType.DateTime:
                {
                    DateTime datetime = (DateTime)value;

                    if (new DateTimeValueEditDlg().ShowDialog(ref datetime))
                    {
                        return datetime;
                    }

                    return null;
                }

                case BuiltInType.QualifiedName:
                {
                    QualifiedName qname = (QualifiedName)value;

                    string name = new StringValueEditDlg().ShowDialog(qname.Name);

                    if (name != null)
                    {
                        return new QualifiedName(name, qname.NamespaceIndex);
                    }

                    return null;
                }
                    
                case BuiltInType.String:
                {
                    return new StringValueEditDlg().ShowDialog((string)value);
                }

                case BuiltInType.LocalizedText:
                {
                    LocalizedText ltext = (LocalizedText)value;

                    string text = new StringValueEditDlg().ShowDialog(ltext.Text);

                    if (text != null)
                    {
                        return new LocalizedText(ltext.Locale, text);
                    }

                    return null;
                }
            }

            return new ComplexValueEditDlg().ShowDialog(value);
        }
        
        /// <summary>
        /// Returns to display icon for the target of a reference.
        /// </summary>
        public static string GetTargetIcon(Session session, ReferenceDescription reference)
        {
            return GetTargetIcon(session, reference.NodeClass, reference.TypeDefinition);
        }

        /// <summary>
        /// Returns to display icon for the target of a reference.
        /// </summary>
        public static string GetTargetIcon(Session session, NodeClass nodeClass, ExpandedNodeId typeDefinitionId)
        { 
            // make sure the type definition is in the cache.
            INode typeDefinition = session.NodeCache.Find(typeDefinitionId);

            switch (nodeClass)
            {
                case NodeClass.Object:
                {                    
                    if (session.TypeTree.IsTypeOf(typeDefinitionId, ObjectTypes.FolderType))
                    {
                        return "Folder";
                    }

                    return "Object";
                }
                    
                case NodeClass.Variable:
                {                    
                    if (session.TypeTree.IsTypeOf(typeDefinitionId, VariableTypes.PropertyType))
                    {
                        return "Property";
                    }

                    return "Variable";
                }                   
            }

            return nodeClass.ToString();
        }
    }
}
