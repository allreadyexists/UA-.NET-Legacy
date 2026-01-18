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
    #region NodeTypeDescription Class
    public partial class NodeTypeDescription
    {
        #region Supporting Properties and Methods
        /// <summary>
        /// A handle assigned to the item during processing.
        /// </summary>
        public object Handle { get; set; }
        
        /// <summary>
        /// Whether the value has been processed.
        /// </summary>
        public bool Processed { get; set; }
        #endregion
                            
        #region Private Fields
        #endregion
    }
    #endregion

    #region QueryDataDescription Class
    public partial class QueryDataDescription
    {
        /// <summary>
        /// Stores the parsed form of the index range parameter.
        /// </summary>
        public NumericRange ParsedIndexRange { get; set; }
    }
    #endregion
}
