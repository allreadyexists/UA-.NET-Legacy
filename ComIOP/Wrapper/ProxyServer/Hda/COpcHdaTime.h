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

#ifndef _COpcHdaTime_H_
#define _COpcHdaTime_H_

#if _MSC_VER >= 1000
#pragma once
#endif // _MSC_VER >= 1000

//==============================================================================
// FUNCTION: OpcHdaResolveTime
// PURPOSE:  Converts a relative time to an absolute UTC time.

LONGLONG OpcHdaResolveTime(OPCHDA_TIME& cTime);

#endif // _COpcHdaTime_H_
