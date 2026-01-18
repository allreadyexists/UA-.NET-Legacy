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
	/// The description of a value to write.
	/// </summary>
    public partial class WriteValue
    {
        #region Supporting Properties and Methods
        /// <summary>
        /// A handle assigned to the item during processing.
        /// </summary>
        public object Handle
        {
            get { return m_handle;  }
            set { m_handle = value; }
        }

        /// <summary>
        /// Whether the value has been processed.
        /// </summary>
        public bool Processed
        {
            get { return m_processed;  }
            set { m_processed = value; }
        }

        /// <summary>
        /// Stores the parsed form of the index range parameter.
        /// </summary>
        public NumericRange ParsedIndexRange
        {
            get { return m_parsedIndexRange;  }
            set { m_parsedIndexRange = value; }
        }
        
        /// <summary>
        /// Validates a write value parameter.
        /// </summary>
        public static ServiceResult Validate(WriteValue value)
        {
            // check for null structure.
            if (value == null)
            {
                return StatusCodes.BadStructureMissing;
            }

            // null node ids are always invalid.
            if (value.NodeId == null)
            {
                return StatusCodes.BadNodeIdInvalid;
            }
            
            // must be a legimate attribute value.
            if (!Attributes.IsValid(value.AttributeId))
            {
                return StatusCodes.BadAttributeIdInvalid;
            }

            // initialize as empty.
            value.ParsedIndexRange = NumericRange.Empty;

            // parse the index range if specified.
            if (!String.IsNullOrEmpty(value.IndexRange))
            {
                try
                {
                    value.ParsedIndexRange = NumericRange.Parse(value.IndexRange);
                }
                catch (Exception e)
                {
                    return ServiceResult.Create(e, StatusCodes.BadIndexRangeInvalid, String.Empty);
                }
                
                // check that value provided is actually an array.
                Array array = value.Value.Value as Array;

                if (array == null)
                {
                    return StatusCodes.BadTypeMismatch;
                }
                
                NumericRange range = value.ParsedIndexRange;

                // check that the number of elements to write matches the index range.
                if (range.End >= 0 && (range.End - range.Begin != array.Length-1))
                {
                    return StatusCodes.BadIndexRangeNoData;
                }

                // check for single element.
                if (range.End < 0 && array.Length != 1)
                {
                    return StatusCodes.BadIndexRangeInvalid;
                }
            }
            else
            {
                value.ParsedIndexRange = NumericRange.Empty;
            }

            // passed basic validation.
            return null;
        }        
        #endregion
        
        #region Private Fields
        private object m_handle;
        private bool m_processed;
        private NumericRange m_parsedIndexRange = NumericRange.Empty;
        #endregion
    }
}
