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
    /// Indicates the type of objects the DirectoryObjectPickerDialog searches for.
    /// </summary>
    [Flags]
    public enum ObjectTypes
    {
        /// <summary>
        /// No object types.
        /// </summary>
        None = 0x0,

        /// <summary>
        /// Includes user objects.
        /// </summary>
        Users = 0x0001, 

        /// <summary>
        /// Includes security groups with universal scope. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// In an up-level scope, this includes distribution and security groups, with universal, global and domain local scope.
        /// </para>
        /// <para>
        /// In a down-level scope, this includes local and global groups.
        /// </para>
        /// </remarks>
        Groups = 0x0002, 
        
        /// <summary>
        /// Includes computer objects.
        /// </summary>
        Computers = 0x0004, 

        /// <summary>
        /// Includes contact objects.
        /// </summary>
        Contacts = 0x0008, 

        /// <summary>
        /// Includes built-in group objects.
        /// </summary>
        /// <summary>
        /// <para>
        /// In an up-level scope, this includes group objects with the built-in groupType flags.
        /// </para>
        /// <para>
        /// In a down-level scope, not setting this object type excludes local built-in groups.
        /// </para>
        /// </summary>
        BuiltInGroups = 0x0010, 

        /// <summary>
        /// Includes all well-known security principals. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// In an up-level scope, this includes the contents of the Well Known Security Principals container.
        /// </para>
        /// <para>
        /// In a down-level scope, this includes all well-known SIDs.
        /// </para>
        /// </remarks>
        WellKnownPrincipals = 0x0020, 

        /// <summary>
        /// All object types.
        /// </summary>
        All = 0x003F
    }
}
