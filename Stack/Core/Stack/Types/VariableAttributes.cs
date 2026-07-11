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
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;

namespace Opc.Ua
{
    /// <summary>
    /// Stores information about engineering units.
    /// </summary>
    public partial class VariableAttributes
    {
        /// <summary>
        /// Initializes the object with the unitName and namespaceUri.
        /// </summary>
        public VariableAttributes(object value, byte accessLevel)
        {
            Initialize();
            
            Value                   = new Variant(value);
            AccessLevel             = accessLevel;
            UserAccessLevel         = accessLevel;
            MinimumSamplingInterval = MinimumSamplingIntervals.Indeterminate;
            Historizing             = false;

            if (value == null)
            {
                DataType  = DataTypes.BaseDataType;
                ValueRank = ValueRanks.Any;
            }
            else
            {
                DataType  = TypeInfo.GetDataTypeId(value);
                ValueRank = TypeInfo.GetValueRank(value);
            }
        }
    }
}
