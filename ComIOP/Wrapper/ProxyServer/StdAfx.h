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

#ifndef _StdAfx_H
#define _StdAfx_H

#if _MSC_VER >= 1000
#pragma once
#endif // _MSC_VER >= 1000

// Insert your headers here
#define WIN32_LEAN_AND_MEAN // Exclude rarely-used stuff from Windows headers

#include <windows.h>
#include <stdio.h>
#include <tchar.h>
#include <objbase.h>
#include <olectl.h>
#include <comcat.h>

//#include "opccomn.h"
#include "opcda.h"
#include "opc_ae.h"
#include "opchda.h"
#include "opcerror.h"
#include "opcae_er.h"
#include "OpcHda_Error.h"

#include "OpcUtils.h"

#endif // _StdAfx_H
