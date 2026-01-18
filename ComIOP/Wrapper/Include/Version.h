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

/*  Copyright (C) OPC Foundation, 2009.               All rights reserved.  */
/****************************************************************************/
 
#define SDK_COPYRIGHT_NOTICE "Copyright (c) 2004-2011 OPC Foundation, Inc"

//----------------------------------------------------------------------------
//	Product name and version.
//
#define SDK_MAJOR     1
#define SDK_MINOR     01
#define SDK_BUILD     333
#define SDK_REVISION  0
#define SDK_PATCH     0

// Convert numbers to strings:
#define chSTR(x) #x
#define chSTR2(x) chSTR(x)

#define SDK_VERSION SDK_MAJOR,SDK_MINOR,SDK_BUILD,SDK_PATCH
#define SDK_VERSION_STR chSTR2(SDK_MAJOR) "." chSTR2(SDK_MINOR)  "." chSTR2(SDK_BUILD) "." chSTR2(SDK_REVISION)

#define SDK_FILEVERSION SDK_MAJOR,SDK_MINOR,SDK_BUILD,SDK_PATCH
#define SDK_FILEVERSION_STR chSTR2(SDK_MAJOR) "." chSTR2(SDK_MINOR)  "." chSTR2(SDK_BUILD) "." chSTR2(SDK_PATCH)
//----------------------------------------------------------------------------

//----------------------------------------------------------------------------
//	OpcUaComServerHost 
//
#define COMHOST_FILEVERSION SDK_FILEVERSION
#define COMHOST_FILEVERSION_STR SDK_FILEVERSION_STR
//----------------------------------------------------------------------------

//----------------------------------------------------------------------------
//	StackTest
//
#define STACKTEST_FILEVERSION SDK_FILEVERSION
#define STACKTEST_FILEVERSION_STR SDK_FILEVERSION_STR
//----------------------------------------------------------------------------
