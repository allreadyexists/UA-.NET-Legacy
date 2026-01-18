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

namespace Opc.Ua
{
	/// <summary>
	/// Defines well-known namespaces.
	/// </summary>
    public static partial class Namespaces
	{
		/// <summary>
		/// The XML Schema namespace.
		/// </summary>
        public const string XmlSchema = "http://www.w3.org/2001/XMLSchema";

		/// <summary>
		/// The XML Schema Instance namespace.
		/// </summary>
        public const string XmlSchemaInstance = "http://www.w3.org/2001/XMLSchema-instance";

		/// <summary>
		/// The WS Secuirity Extensions Namespace.
		/// </summary>
        public const string WSSecurityExtensions = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd";
        
		/// <summary>
		/// The WS Secuirity Utilities Namespace.
		/// </summary>
        public const string WSSecurityUtilities = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd";
        
		/// <summary>
		/// The URI for the UA WSDL.
		/// </summary>
        public const string OpcUaWsdl = "http://opcfoundation.org/UA/2008/02/Services.wsdl";
        
        /// <summary>
        /// The URI for the UA SecuredApplication schema.
        /// </summary>
        public const string OpcUaSecurity = "http://opcfoundation.org/UA/2011/03/SecuredApplication.xsd";

        /// <summary>
        /// The base URI for the Global Discovery Service.
        /// </summary>
        public const string OpcUaGds = "http://opcfoundation.org/UA/GDS/";
				
		/// <summary>
		/// The base URI for SDK related schemas.
		/// </summary>
        public const string OpcUaSdk = "http://opcfoundation.org/UA/SDK/";

		/// <summary>
		/// The URI for the UA SDK Configuration Schema.
		/// </summary>
        public const string OpcUaConfig = OpcUaSdk + "Configuration.xsd";
        
        /// <summary>
        /// The URI for the built-in types namespace.
        /// </summary>
        public const string OpcUaBuiltInTypes = OpcUa + "BuiltInTypes/";

        /// <summary>
        /// The URI for the OPC Binary Schema.
        /// </summary>
        public const string OpcBinarySchema = "http://opcfoundation.org/BinarySchema/";
	}
}
