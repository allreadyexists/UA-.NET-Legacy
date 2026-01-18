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
    public partial class ScalarValueObjectState
    {
        #region Initialization
        /// <summary>
        /// Initializes the object as a collection of counters which change value on read.
        /// </summary>
        protected override void OnAfterCreate(ISystemContext context, NodeState node)
        {
            base.OnAfterCreate(context, node);
            
            InitializeVariable(context, BooleanValue, TestData.Variables.ScalarValueObjectType_BooleanValue);
            InitializeVariable(context, SByteValue, TestData.Variables.ScalarValueObjectType_SByteValue);
            InitializeVariable(context, ByteValue, TestData.Variables.ScalarValueObjectType_ByteValue);
            InitializeVariable(context, Int16Value, TestData.Variables.ScalarValueObjectType_Int16Value);
            InitializeVariable(context, UInt16Value, TestData.Variables.ScalarValueObjectType_UInt16Value);
            InitializeVariable(context, Int32Value, TestData.Variables.ScalarValueObjectType_Int32Value);
            InitializeVariable(context, UInt32Value, TestData.Variables.ScalarValueObjectType_UInt32Value);
            InitializeVariable(context, Int64Value, TestData.Variables.ScalarValueObjectType_Int64Value);
            InitializeVariable(context, UInt64Value, TestData.Variables.ScalarValueObjectType_UInt64Value);
            InitializeVariable(context, FloatValue, TestData.Variables.ScalarValueObjectType_FloatValue);
            InitializeVariable(context, DoubleValue, TestData.Variables.ScalarValueObjectType_DoubleValue);
            InitializeVariable(context, StringValue, TestData.Variables.ScalarValueObjectType_StringValue);
            InitializeVariable(context, DateTimeValue, TestData.Variables.ScalarValueObjectType_DateTimeValue);
            InitializeVariable(context, GuidValue, TestData.Variables.ScalarValueObjectType_GuidValue);
            InitializeVariable(context, ByteStringValue, TestData.Variables.ScalarValueObjectType_ByteStringValue);
            InitializeVariable(context, XmlElementValue, TestData.Variables.ScalarValueObjectType_XmlElementValue);
            InitializeVariable(context, NodeIdValue, TestData.Variables.ScalarValueObjectType_NodeIdValue);
            InitializeVariable(context, ExpandedNodeIdValue, TestData.Variables.ScalarValueObjectType_ExpandedNodeIdValue);
            InitializeVariable(context, QualifiedNameValue, TestData.Variables.ScalarValueObjectType_QualifiedNameValue);
            InitializeVariable(context, LocalizedTextValue, TestData.Variables.ScalarValueObjectType_LocalizedTextValue);
            InitializeVariable(context, StatusCodeValue, TestData.Variables.ScalarValueObjectType_StatusCodeValue);
            InitializeVariable(context, VariantValue, TestData.Variables.ScalarValueObjectType_VariantValue);
            InitializeVariable(context, EnumerationValue, TestData.Variables.ScalarValueObjectType_EnumerationValue);
            InitializeVariable(context, StructureValue, TestData.Variables.ScalarValueObjectType_StructureValue);
            InitializeVariable(context, NumberValue, TestData.Variables.ScalarValueObjectType_NumberValue);
            InitializeVariable(context, IntegerValue, TestData.Variables.ScalarValueObjectType_IntegerValue);
            InitializeVariable(context, UIntegerValue, TestData.Variables.ScalarValueObjectType_UIntegerValue);
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
            GenerateValue(system, EnumerationValue);
            GenerateValue(system, StructureValue);
            GenerateValue(system, NumberValue);
            GenerateValue(system, IntegerValue);
            GenerateValue(system, UIntegerValue);

            return base.OnGenerateValues(context, method, objectId, count);
        }  
        #endregion
    }
}
