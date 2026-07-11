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
using System.IO;
using System.Reflection;
using Opc.Ua;

namespace TestData
{
    public partial class UserArrayValueObjectState
    {
        #region Initialization
        /// <summary>
        /// Initializes the object as a collection of counters which change value on read.
        /// </summary>
        protected override void OnAfterCreate(ISystemContext context, NodeState node)
        {
            base.OnAfterCreate(context, node);

            InitializeVariable(context, BooleanValue, TestData.Variables.UserArrayValueObjectType_BooleanValue);
            InitializeVariable(context, SByteValue, TestData.Variables.UserArrayValueObjectType_SByteValue);
            InitializeVariable(context, ByteValue, TestData.Variables.UserArrayValueObjectType_ByteValue);
            InitializeVariable(context, Int16Value, TestData.Variables.UserArrayValueObjectType_Int16Value);
            InitializeVariable(context, UInt16Value, TestData.Variables.UserArrayValueObjectType_UInt16Value);
            InitializeVariable(context, Int32Value, TestData.Variables.UserArrayValueObjectType_Int32Value);
            InitializeVariable(context, UInt32Value, TestData.Variables.UserArrayValueObjectType_UInt32Value);
            InitializeVariable(context, Int64Value, TestData.Variables.UserArrayValueObjectType_Int64Value);
            InitializeVariable(context, UInt64Value, TestData.Variables.UserArrayValueObjectType_UInt64Value);
            InitializeVariable(context, FloatValue, TestData.Variables.UserArrayValueObjectType_FloatValue);
            InitializeVariable(context, DoubleValue, TestData.Variables.UserArrayValueObjectType_DoubleValue);
            InitializeVariable(context, StringValue, TestData.Variables.UserArrayValueObjectType_StringValue);
            InitializeVariable(context, DateTimeValue, TestData.Variables.UserArrayValueObjectType_DateTimeValue);
            InitializeVariable(context, GuidValue, TestData.Variables.UserArrayValueObjectType_GuidValue);
            InitializeVariable(context, ByteStringValue, TestData.Variables.UserArrayValueObjectType_ByteStringValue);
            InitializeVariable(context, XmlElementValue, TestData.Variables.UserArrayValueObjectType_XmlElementValue);
            InitializeVariable(context, NodeIdValue, TestData.Variables.UserArrayValueObjectType_NodeIdValue);
            InitializeVariable(context, ExpandedNodeIdValue, TestData.Variables.UserArrayValueObjectType_ExpandedNodeIdValue);
            InitializeVariable(context, QualifiedNameValue, TestData.Variables.UserArrayValueObjectType_QualifiedNameValue);
            InitializeVariable(context, LocalizedTextValue, TestData.Variables.UserArrayValueObjectType_LocalizedTextValue);
            InitializeVariable(context, StatusCodeValue, TestData.Variables.UserArrayValueObjectType_StatusCodeValue);
            InitializeVariable(context, VariantValue, TestData.Variables.UserArrayValueObjectType_VariantValue);
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Handles the generate values method.
        /// </summary>
        protected override ServiceResult OnGenerateValues(
            ISystemContext context,
            MethodState method,
            NodeId objectId,
            uint count)
        {
            TestDataSystem system = context.SystemHandle as TestDataSystem;

            if (system == null)
            {
                return StatusCodes.BadOutOfService;
            }

            GenerateValue(system, BooleanValue);
            GenerateValue(system, SByteValue);
            GenerateValue(system, ByteValue);
            GenerateValue(system, Int16Value);
            GenerateValue(system, UInt16Value);
            GenerateValue(system, Int32Value);
            GenerateValue(system, UInt32Value);
            GenerateValue(system, UInt32Value);
            GenerateValue(system, Int64Value);
            GenerateValue(system, UInt64Value);
            GenerateValue(system, FloatValue);
            GenerateValue(system, DoubleValue);
            GenerateValue(system, StringValue);
            GenerateValue(system, DateTimeValue);
            GenerateValue(system, GuidValue);
            GenerateValue(system, ByteStringValue);
            GenerateValue(system, XmlElementValue);
            GenerateValue(system, NodeIdValue);
            GenerateValue(system, ExpandedNodeIdValue);
            GenerateValue(system, QualifiedNameValue);
            GenerateValue(system, LocalizedTextValue);
            GenerateValue(system, StatusCodeValue);
            GenerateValue(system, VariantValue);

            return base.OnGenerateValues(context, method, objectId, count);
        }    
        #endregion
    }
}
