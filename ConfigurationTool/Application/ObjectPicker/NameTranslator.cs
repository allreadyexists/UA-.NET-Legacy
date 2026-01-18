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

namespace CubicOrange.Windows.Forms.ActiveDirectory
{
    /// <summary>
    /// Active Directory name translation.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Translates names between Active Directory formats, e.g. from down-level NT4 
    /// style names ("ACME\alice") to User Principal Name ("alice@acme.com").
    /// </para>
    /// <para>
    /// This utility class encapsulates the ActiveDs.dll COM library.
    /// </para>
    /// </remarks>
    public static class NameTranslator
    {
        const int NameTypeUpn = (int)ActiveDs.ADS_NAME_TYPE_ENUM.ADS_NAME_TYPE_USER_PRINCIPAL_NAME;
        const int NameTypeNt4 = (int)ActiveDs.ADS_NAME_TYPE_ENUM.ADS_NAME_TYPE_NT4;
        const int NameTypeDn = (int)ActiveDs.ADS_NAME_TYPE_ENUM.ADS_NAME_TYPE_1779;

        /// <summary>
        /// Convert from a down-level NT4 style name to an Active Directory User Principal Name (UPN).
        /// </summary>
        public static string TranslateDownLevelToUpn(string downLevelNt4Name)
        {
            string userPrincipalName;
            ActiveDs.NameTranslate nt = new ActiveDs.NameTranslate();
            nt.Set(NameTypeNt4, downLevelNt4Name);
            userPrincipalName = nt.Get(NameTypeUpn);
            return userPrincipalName;
        }

        /// <summary>
        /// Convert from an Active Directory User Principal Name (UPN) to a down-level NT4 style name.
        /// </summary>
        public static string TranslateUpnToDownLevel(string userPrincipalName)
        {
            string downLevelName;
            ActiveDs.NameTranslate nt = new ActiveDs.NameTranslate();
            nt.Set(NameTypeUpn, userPrincipalName);
            downLevelName = nt.Get(NameTypeNt4);
            return downLevelName;
        }
    }
}
