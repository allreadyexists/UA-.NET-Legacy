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
using System.Text;
using Opc.Ua;

namespace Quickstarts.HistoricalEvents.Client
{
    public class FilterDefinition
    {
        /// <summary>
        /// The type of event.
        /// </summary>
        public NodeId EventTypeId;

        /// <summary>
        /// The fields belonging to the event.
        /// </summary>
        public List<FilterDefinitionField> Fields;
        
        /// <summary>
        /// Sets a default filter.
        /// </summary>
        public void SetDefault()
        {
            EventTypeId = Opc.Ua.ObjectTypeIds.BaseEventType;
            Fields = new List<FilterDefinitionField>();

            FilterDefinitionField field = new FilterDefinitionField();
            field.DisplayName = Opc.Ua.BrowseNames.EventType;
            field.DataType = Opc.Ua.DataTypeIds.NodeId;
            field.ValueRank = ValueRanks.Scalar;
            field.BuiltInType = BuiltInType.NodeId;
            field.DataTypeDisplayName = field.BuiltInType.ToString();
            field.Operand = new SimpleAttributeOperand();
            field.Operand.TypeDefinitionId = Opc.Ua.ObjectTypeIds.BaseEventType;
            field.Operand.BrowsePath.Add(Opc.Ua.BrowseNames.EventType);
            field.Operand.AttributeId = Attributes.Value;
            field.ShowColumn = true;
            Fields.Add(field);

            field = new FilterDefinitionField();
            field.DisplayName = Opc.Ua.BrowseNames.SourceNode;
            field.DataType = Opc.Ua.DataTypeIds.NodeId;
            field.ValueRank = ValueRanks.Scalar;
            field.BuiltInType = BuiltInType.NodeId;
            field.DataTypeDisplayName = field.BuiltInType.ToString();
            field.Operand = new SimpleAttributeOperand();
            field.Operand.TypeDefinitionId = Opc.Ua.ObjectTypeIds.BaseEventType;
            field.Operand.BrowsePath.Add(Opc.Ua.BrowseNames.SourceNode);
            field.Operand.AttributeId = Attributes.Value;
            field.ShowColumn = true;
            Fields.Add(field);

            field = new FilterDefinitionField();
            field.DisplayName = Opc.Ua.BrowseNames.Time;
            field.DataType = Opc.Ua.DataTypeIds.DateTime;
            field.ValueRank = ValueRanks.Scalar;
            field.BuiltInType = BuiltInType.DateTime;
            field.DataTypeDisplayName = field.BuiltInType.ToString();
            field.Operand = new SimpleAttributeOperand();
            field.Operand.TypeDefinitionId = Opc.Ua.ObjectTypeIds.BaseEventType;
            field.Operand.BrowsePath.Add(Opc.Ua.BrowseNames.Time);
            field.Operand.AttributeId = Attributes.Value;
            field.ShowColumn = true;
            Fields.Add(field);

            field = new FilterDefinitionField();
            field.DisplayName = Opc.Ua.BrowseNames.EventId;
            field.DataType = Opc.Ua.DataTypeIds.ByteString;
            field.ValueRank = ValueRanks.Scalar;
            field.BuiltInType = BuiltInType.ByteString;
            field.DataTypeDisplayName = field.BuiltInType.ToString();
            field.Operand = new SimpleAttributeOperand();
            field.Operand.TypeDefinitionId = Opc.Ua.ObjectTypeIds.BaseEventType;
            field.Operand.BrowsePath.Add(Opc.Ua.BrowseNames.EventId);
            field.Operand.AttributeId = Attributes.Value;
            field.ShowColumn = false;
            Fields.Add(field);

            field = new FilterDefinitionField();
            field.DisplayName = Opc.Ua.BrowseNames.Message;
            field.DataType = Opc.Ua.DataTypeIds.LocalizedText;
            field.ValueRank = ValueRanks.Scalar;
            field.BuiltInType = BuiltInType.LocalizedText;
            field.DataTypeDisplayName = field.BuiltInType.ToString();
            field.Operand = new SimpleAttributeOperand();
            field.Operand.TypeDefinitionId = Opc.Ua.ObjectTypeIds.BaseEventType;
            field.Operand.BrowsePath.Add(Opc.Ua.BrowseNames.Message);
            field.Operand.AttributeId = Attributes.Value;
            field.ShowColumn = true;
            Fields.Add(field);
        }

        /// <summary>
        /// Returns the subscription filter to use.
        /// </summary>
        public EventFilter GetFilter()
        {
            ContentFilter whereClause = new ContentFilter();
            ContentFilterElement element1 = whereClause.Push(FilterOperator.OfType, EventTypeId);

            EventFilter filter = new EventFilter();

            for (int ii = 0; ii < Fields.Count; ii++)
            {
                filter.SelectClauses.Add(Fields[ii].Operand);

                if (Fields[ii].FilterValue != Variant.Null)
                {
                    LiteralOperand operand = new LiteralOperand();
                    operand.Value = Fields[ii].FilterValue;
                    ContentFilterElement element2 = whereClause.Push(Fields[ii].FilterOperator, Fields[ii].Operand, operand);
                    element1 = whereClause.Push(FilterOperator.And, element1, element2);
                }
            }

            filter.WhereClause = whereClause;

            return filter;
        }
    }
}
