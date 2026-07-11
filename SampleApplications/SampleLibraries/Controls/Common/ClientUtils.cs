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
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Opc.Ua.Configuration;

namespace Opc.Ua.Client.Controls
{
    /// <summary>
    /// A class that provide various common utility functions and shared resources.
    /// </summary>
    public partial class ClientUtils : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientUtils"/> class.
        /// </summary>
        public ClientUtils()
        {
            InitializeComponent();
        }

        private const int Attribute = 0;
        private const int Property = 1;
        private const int Variable = 2;
        private const int Method = 3;
        private const int Object = 4;
        private const int OpenFolder = 5;
        private const int ClosedFolder = 6;
        private const int ObjectType = 7;
        private const int View = 8;
        private const int Reference = 9;
        private const int NumberValue = 10;
        private const int StringValue = 11;
        private const int ByteStringValue = 12;
        private const int StructureValue = 13;
        private const int ArrayValue = 14;
        private const int InputArgument = 15;
        private const int OutputArgument = 16;
        
        /// <summary>
        /// Returns an image index for the specified method argument.
        /// </summary>
        public static int GetImageIndex(bool isOutputArgument, object value)
        {
            if (isOutputArgument)
            {
                return OutputArgument;
            }

            return InputArgument;
        }

        /// <summary>
        /// Returns an image index for the specified attribute.
        /// </summary>
        public static int GetImageIndex(uint attributeId, object value)
        {
            if (attributeId == Attributes.Value)
            {
                TypeInfo typeInfo = TypeInfo.Construct(value);

                if (typeInfo.ValueRank >= 0)
                {
                    return ClientUtils.ArrayValue;
                }

                if (typeInfo.BuiltInType == BuiltInType.Variant)
                {
                    typeInfo = ((Variant)value).TypeInfo;

                    if (typeInfo == null)
                    {
                        typeInfo = TypeInfo.Construct(((Variant)value).Value);
                    }
                }

                switch (typeInfo.BuiltInType)
                {
                    case BuiltInType.Number:
                    case BuiltInType.SByte:
                    case BuiltInType.Byte:
                    case BuiltInType.Int16:
                    case BuiltInType.UInt16:
                    case BuiltInType.Int32:
                    case BuiltInType.UInt32:
                    case BuiltInType.Int64:
                    case BuiltInType.UInt64:
                    case BuiltInType.Float:
                    case BuiltInType.Double:
                    case BuiltInType.Enumeration:
                    case BuiltInType.UInteger:
                    case BuiltInType.Integer:
                    case BuiltInType.Boolean:
                    {
                        return ClientUtils.NumberValue;
                    }

                    case BuiltInType.ByteString:
                    {
                        return ClientUtils.ByteStringValue;
                    }

                    case BuiltInType.ExtensionObject:
                    case BuiltInType.DiagnosticInfo:
                    case BuiltInType.DataValue:
                    {
                        return ClientUtils.StructureValue;
                    }
                }

                return ClientUtils.StringValue;
            }

            return ClientUtils.Attribute;
        }

        /// <summary>
        /// Returns an image index for the specified attribute.
        /// </summary>
        public static int GetImageIndex(Session session, NodeClass nodeClass, ExpandedNodeId typeDefinitionId, bool selected)
        {
            if (nodeClass == NodeClass.Variable)
            {
                if (session.NodeCache.IsTypeOf(typeDefinitionId, Opc.Ua.VariableTypeIds.PropertyType))
                {
                    return ClientUtils.Property;
                }

                return ClientUtils.Variable;
            }

            if (nodeClass == NodeClass.Object)
            {
                if (session.NodeCache.IsTypeOf(typeDefinitionId, Opc.Ua.ObjectTypeIds.FolderType))
                {
                    if (selected)
                    {
                        return ClientUtils.OpenFolder;
                    }
                    else
                    {
                        return ClientUtils.ClosedFolder;
                    }
                }

                return ClientUtils.Object;
            }

            if (nodeClass == NodeClass.Method)
            {
                return ClientUtils.Method;
            }

            if (nodeClass == NodeClass.View)
            {
                return ClientUtils.View;
            }

            return ClientUtils.ObjectType;
        }
    }
}
