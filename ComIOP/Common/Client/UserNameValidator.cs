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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using Opc.Ua.Configuration;


namespace Opc.Ua.Com.Client
{
    /// <summary>
    /// Validates UserName.
    /// </summary>
    public class UserNameValidator
    {
        /// <summary>
        /// Triple DES Key
        /// </summary>
        private const string strKey = "h13h6m9F";

        /// <summary>
        /// Triple DES initialization vector
        /// </summary>
        private const string strIV = "Zse5";

        #region Constructors
        /// <summary>
        /// The default constructor.
        /// </summary>
        public UserNameValidator(string applicationName)
        {
            m_UserNameIdentityTokens = UserNameCreator.LoadUserName(applicationName);
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Validates a User.
        /// </summary>
        /// <param name="token">UserNameIdentityToken.</param>
        /// <returns>True if the list contains a valid item.</returns>
        public bool Validate(UserNameIdentityToken token)
        {
            return Validate(token.UserName, token.DecryptedPassword);
        }

        /// <summary>
        /// Validates a User.
        /// </summary>
        /// <param name="name">user name.</param>
        /// <param name="password">password.</param>
        /// <returns>True if the list contains a valid item.</returns>
        public bool Validate(string name, string password)
        {
            lock (m_lock)
            {
                if (!m_UserNameIdentityTokens.ContainsKey(name))
                {
                    return false;
                }

                return (m_UserNameIdentityTokens[name].DecryptedPassword == password);
            }
        }

        #endregion

        #region Private Fields
        private object m_lock = new object();
        private Dictionary<string, UserNameIdentityToken> m_UserNameIdentityTokens = new Dictionary<string, UserNameIdentityToken>();
        #endregion
    }
}
