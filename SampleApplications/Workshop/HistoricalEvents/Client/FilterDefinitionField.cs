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
    /// <summary>
    /// Stores information about an event field used in a subscription.
    /// </summary>
    public class FilterDefinitionField
    {
        /// <summary>
        /// A display name for the field.
        /// </summary>
        public string DisplayName;

        /// <summary>
        /// A description for the field.
        /// </summary>
        public string Description;

        /// <summary>
        /// The field data type.
        /// </summary>
        public NodeId DataType;

        /// <summary>
        /// The field value rank.
        /// </summary>
        public int ValueRank;

        /// <summary>
        /// A display name for the field data type.
        /// </summary>
        public string DataTypeDisplayName;

        /// <summary>
        /// The built in type represnted by the data type.
        /// </summary>
        public BuiltInType BuiltInType;

        /// <summary>
        /// The operand that describes the field.
        /// </summary>
        public SimpleAttributeOperand Operand;

        /// <summary>
        /// Any filter criteria applied to the field.
        /// </summary>
        public FilterOperator FilterOperator;

        /// <summary>
        /// Any filter criteria applied to the field.
        /// </summary>
        public Variant FilterValue;

        /// <summary>
        /// Whether the field appears in the list view.
        /// </summary>
        public bool ShowColumn;
    }
}
