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

namespace AlarmConditionClient
{
    /// <summary>
    /// Stores information about the event fields used in a subscription.
    /// </summary>
    public class EventFieldDefinition
    {
        /// <summary>
        /// A display name for the field.
        /// </summary>
        public string DisplayName;

        /// <summary>
        /// The instance declartion for the field in the server's address space.
        /// </summary>
        public ReferenceDescription DeclarationNode;

        /// <summary>
        /// The operand used in the select clause.
        /// </summary>
        public SimpleAttributeOperand Operand;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventFieldDefinition"/> class.
        /// </summary>
        /// <param name="parentField">The parent field.</param>
        /// <param name="reference">The reference to the instance declaration node.</param>
        public EventFieldDefinition(EventFieldDefinition parentField, ReferenceDescription reference)
        {
            DeclarationNode = reference;

            Operand = new SimpleAttributeOperand();

            // setting the typedefinition id to null ignores the event type when evaluating the operand.
            Operand.TypeDefinitionId = null;

            // event filters only support the NodeId attribute for objects and the Value attribute for Variables.
            Operand.AttributeId = (reference.NodeClass == NodeClass.Variable) ? Attributes.Value : Attributes.NodeId;

            // prefix the browse path with the parent browse path.
            if (parentField != null)
            {
                Operand.BrowsePath = new QualifiedNameCollection(parentField.Operand.BrowsePath);
            }

            // add the child browse name.
            Operand.BrowsePath.Add(reference.BrowseName);

            // may select sub-sets of array values. Not used in this sample.
            Operand.IndexRange = null;

            // construct the display name.
            StringBuilder buffer = new StringBuilder();

            for (int ii = 0; ii < Operand.BrowsePath.Count; ii++)
            {
                if (buffer.Length > 0)
                {
                    buffer.Append('/');
                }

                buffer.Append(Operand.BrowsePath[ii].Name);
            }

            DisplayName = buffer.ToString();
        }
    }
}
