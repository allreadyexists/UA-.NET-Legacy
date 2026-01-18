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

using System.IdentityModel.Tokens;
using System.Xml;

namespace Opc.Ua
{    
    /// <summary>
    /// An interface to an object with stores the identity of a user.
    /// </summary>
    public interface IUserIdentity
    {
        /// <summary>
        /// A display name that identifies the user.
        /// </summary>
        /// <value>The display name.</value>
        string DisplayName { get; }

        /// <summary>
        /// The type of identity token used.
        /// </summary>
        /// <value>The type of the token.</value>
        UserTokenType TokenType { get; }

        /// <summary>
        /// The type of issued token.
        /// </summary>
        /// <value>The type of the issued token.</value>
        XmlQualifiedName IssuedTokenType { get; }
        
        /// <summary>
        /// Whether the object can create signatures to prove possession of the user's credentials.
        /// </summary>
        /// <value><c>true</c> if signatures are supported; otherwise, <c>false</c>.</value>
        bool SupportsSignatures { get; }

        /// <summary>
        /// Returns a .NET security token containing the user information.
        /// </summary>
        /// <returns>.NET security token containing the user information.</returns>
        SecurityToken GetSecurityToken();

        /// <summary>
        /// Returns a UA user identity token containing the user information.
        /// </summary>
        /// <returns>UA user identity token containing the user information.</returns>
        UserIdentityToken GetIdentityToken();
    }
}
