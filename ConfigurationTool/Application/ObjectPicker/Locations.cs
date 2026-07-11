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
    /// Indicates the scope the DirectoryObjectPickerDialog searches for objects.
    /// </summary>
    [Flags]
    public enum Locations
    {
        /// <summary>
        /// No locations.
        /// </summary>
        None = 0x0,

        /// <summary>
        /// The target computer (down-level).
        /// </summary>
        LocalComputer = 0x0001,

        /// <summary>
        /// A domain to which the target computer is joined (down-level and up-level).
        /// </summary>
        JoinedDomain = 0x0002,

        /// <summary>
        /// All Windows 2000 domains in the enterprise to which the target computer belongs (up-level).
        /// </summary>
        EnterpriseDomain = 0x0004,

        /// <summary>
        /// A scope containing objects from all domains in the enterprise (up-level). 
        /// </summary>
        GlobalCatalog = 0x0008,

        /// <summary>
        /// All domains external to the enterprise, but trusted by the domain to which the target computer 
        /// is joined (down-level and up-level).
        /// </summary>
        ExternalDomain = 0x0010,

        /// <summary>
        /// The workgroup to which the target computer is joined (down-level). 
        /// </summary>
        /// <remarks>
        /// <para>
        /// Applies only if the target computer is not 
        /// joined to a domain. The only type of object that can be selected from a workgroup is a computer.
        /// </para>
        /// </remarks>
        Workgroup = 0x0020,

        /// <summary>
        /// Enables the user to enter a scope (down-level and up-level). 
        /// </summary>
        /// <remarks>
        /// <para>
        /// If not specified, the dialog box restricts the user to the scopes in the locations drop-down list.
        /// </para>
        /// </remarks>
        UserEntered = 0x0040,

        /// <summary>
        /// All locations.
        /// </summary>
        All = 0x007F
    }
}
