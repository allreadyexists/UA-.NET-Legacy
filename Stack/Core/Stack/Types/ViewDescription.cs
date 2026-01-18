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
    #region ViewDescription Class
    /// <summary>
    /// Describes a view to browse or query.
    /// </summary>
    public partial class ViewDescription
    {
        /// <summary>
        /// Returns true if the view description represents the default (null) view.
        /// </summary>
        public static bool IsDefault(ViewDescription view)
        {
            if (view == null)
            {
                return true;
            }

            if (NodeId.IsNull(view.m_viewId) && view.m_viewVersion == 0 && view.m_timestamp == DateTime.MinValue)
            {
                return true;
            }

            return false;
        }
    }
    #endregion
}
