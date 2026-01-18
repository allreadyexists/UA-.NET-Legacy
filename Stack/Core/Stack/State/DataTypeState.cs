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

namespace Opc.Ua
{
    /// <summary> 
    /// The base class for all reference type nodes.
    /// </summary>
    public class DataTypeState : BaseTypeState
    {
        #region Constructors
        /// <summary>
        /// Initializes the instance with its defalt attribute values.
        /// </summary>
        public DataTypeState() : base(NodeClass.DataType)
        {
        }

        /// <summary>
        /// Constructs an instance of a node.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <returns>The new node.</returns>
        public static NodeState Construct(NodeState parent)
        {
            return new DataTypeState();
        }
        #endregion

        /// <summary>
        /// The abstract definition of the data type.
        /// </summary>
        public DataTypeDefinition Definition { get; set; }
    }

    /// <summary>
    /// Defines an abstract description of a type.
    /// </summary>
    public class DataTypeDefinition
    {
        /// <summary>
        /// The name of the type.
        /// </summary>
        public QualifiedName Name { get; set; }

        /// <summary>
        /// The symbolic name (if the name can't be used as a program symbol).
        /// </summary>
        public string SymbolicName { get; set; }

        /// <summary>
        /// The qualified name of the base type.
        /// </summary>
        public QualifiedName BaseType { get; set; }

        /// <summary>
        /// The description of the data type.
        /// </summary>
        public LocalizedText Description { get; set; }

        /// <summary>
        /// The fields in structure.
        /// </summary>
        public List<DataTypeDefinitionField> Fields { get; set; }
    }

    /// <summary>
    /// Defines a field within an abstract definition of a data type.
    /// </summary>
    public class DataTypeDefinitionField
    {
        /// <summary>
        /// The name of the field.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The symbolic name (if the name can't be used as a program symbol).
        /// </summary>
        public string SymbolicName { get; set; }

        /// <summary>
        /// The data type of the field.
        /// </summary>
        public NodeId DataType { get; set; }

        /// <summary>
        /// The value rank for the field.
        /// </summary>
        public int ValueRank { get; set; }

        /// <summary>
        /// The description of the field.
        /// </summary>
        public LocalizedText Description { get; set; }

        /// <summary>
        /// A nested description of a structured field.
        /// </summary>
        public DataTypeDefinition Definition { get; set; }

        /// <summary>
        /// The value of an enumerated field.
        /// </summary>
        public int Value { get; set; }
    }
}
