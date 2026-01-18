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

using System.Security.Cryptography.X509Certificates;

namespace Opc.Ua
{
    /// <summary>
    /// An abstract interface to the certificate validator.
    /// </summary>
    public interface ICertificateValidator
    {
        /// <summary>
        /// Validates a certificate.
        /// </summary>
        void Validate(X509Certificate2 certificate);
    }
}
