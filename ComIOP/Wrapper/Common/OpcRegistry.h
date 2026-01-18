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

#ifndef _OpcRegistry_H_
#define _OpcRegistry_H_

#if _MSC_VER >= 1000
#pragma once
#endif // _MSC_VER >= 1000

#include "OpcDefs.h"

//==============================================================================
// FUNCTION: OpcRegGetValue
// PURPOSE:  Gets a string value from the registry.

bool OPCUTILS_API OpcRegGetValue(
	HKEY    hBaseKey,
	LPCTSTR tsSubKey,
	LPCTSTR tsValueName,
	LPTSTR* ptsValue
);

//==============================================================================
// FUNCTION: OpcRegGetValue
// PURPOSE:  Gets a DWORD value from the registry.

bool OPCUTILS_API OpcRegGetValue(
	HKEY    hBaseKey,
	LPCTSTR tsSubKey,
	LPCTSTR tsValueName,
	DWORD*  pdwValue
);

//==============================================================================
// FUNCTION: OpcRegGetValue
// PURPOSE:  Gets a DWORD value from the registry.

bool OPCUTILS_API OpcRegGetValue(
	HKEY    hBaseKey,
	LPCTSTR tsSubKey,
	LPCTSTR tsValueName,
	BYTE**  ppValue,
	DWORD*  pdwLength
);

//==============================================================================
// FUNCTION: OpcRegSetValue
// PURPOSE:  Sets a string value in the registry.

bool OPCUTILS_API OpcRegSetValue(
	HKEY    hBaseKey,
	LPCTSTR tsSubKey,
	LPCTSTR tsValueName,
	LPCTSTR tsValue
);

//==============================================================================
// FUNCTION: OpcRegSetValue
// PURPOSE:  Gets a DWORD value from the registry.

bool OPCUTILS_API OpcRegSetValue(
	HKEY    hBaseKey,
	LPCTSTR tsSubKey,
	LPCTSTR tsValueName,
	DWORD   dwValue
);

//==============================================================================
// FUNCTION: OpcRegSetValue
// PURPOSE:  Sets a string value in the registry.

bool OPCUTILS_API OpcRegSetValue(
	HKEY    hBaseKey,
	LPCTSTR tsSubKey,
	LPCTSTR tsValueName,
	BYTE*   pValue,
	DWORD   dwLength
);

//==============================================================================
// FUNCTION: OpcRegDeleteKey
// PURPOSE:  Recursively deletes a key and all sub keys.
// NOTES:

bool OPCUTILS_API OpcRegDeleteKey(
	HKEY    hBaseKey,
	LPCTSTR tsSubKey
);

#endif //ndef _OpcRegistry_H_
