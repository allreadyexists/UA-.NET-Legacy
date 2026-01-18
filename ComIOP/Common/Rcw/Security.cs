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
using System.Runtime.InteropServices;

#pragma warning disable 1591

namespace OpcRcw.Security
{       
    /// <exclude />
	[ComImport]
	[GuidAttribute("7AA83A01-6C77-11d3-84F9-00008630A38B")]
	[InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)] 
    public interface IOPCSecurityNT
    {
	    void IsAvailableNT(
		    [Out][MarshalAs(UnmanagedType.I4)]
		    out int pbAvailable);

	    void QueryMinImpersonationLevel(
		    [Out][MarshalAs(UnmanagedType.I4)]
		    out int pdwMinImpLevel);

	    void ChangeUser();
    };

    /// <exclude />
	[ComImport]
	[GuidAttribute("7AA83A02-6C77-11d3-84F9-00008630A38B")]
	[InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)] 
    public interface IOPCSecurityPrivate
    {
        void IsAvailablePriv(
		    [Out][MarshalAs(UnmanagedType.I4)]
		    out int pbAvailable);

        void Logon(
			[MarshalAs(UnmanagedType.LPWStr)]
		    string szUserID, 
			[MarshalAs(UnmanagedType.LPWStr)]
		    string szPassword);

        void Logoff();
    };
}
