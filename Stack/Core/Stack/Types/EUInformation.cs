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
    public partial class EUInformation
    {
        /// <summary>
        /// Initializes the object with the unitName and namespaceUri.
        /// </summary>
        public EUInformation(string unitName, string namespaceUri)
        {
            Initialize();

            m_displayName  = new LocalizedText(unitName);
            m_description  = new LocalizedText(unitName);
            m_namespaceUri = namespaceUri;
        }

        /// <summary>
        /// Initializes the object with the unitName and namespaceUri.
        /// </summary>
        public EUInformation(string shortName, string longName, string namespaceUri)
        {
            Initialize();

            m_displayName  = new LocalizedText(shortName);
            m_description  = new LocalizedText(longName);
            m_namespaceUri = namespaceUri;
        }
    }
}
