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

#ifndef _OpcUa_Utilities_H_
#define _OpcUa_Utilities_H_ 1

#include <opcua_platformdefs.h>

OPCUA_BEGIN_EXTERN_C

enum _OpcUa_ProtocolType
{
    OpcUa_ProtocolType_Invalid,
    OpcUa_ProtocolType_Http,
    OpcUa_ProtocolType_Tcp
};
typedef enum _OpcUa_ProtocolType OpcUa_ProtocolType;


/** 
 * @brief Sorts an array.
 *
 * @param pElements     [in] The array of elements to sort.
 * @param nElementCount [in] The number of elements in the array.
 * @param nElementSize  [in] The size a single element in the array.
 * @param pfnCompare    [in] The function used to compare elements.
 * @param pContext      [in] A context that is passed to the compare function.
 */
OPCUA_EXPORT 
OpcUa_StatusCode OpcUa_QSort(   OpcUa_Void*       pElements, 
                                OpcUa_UInt32      nElementCount, 
                                OpcUa_UInt32      nElementSize, 
                                OpcUa_PfnCompare* pfnCompare, 
                                OpcUa_Void*       pContext);

/** 
 * @brief Searches a sorted array.
 *
 * @param pKey          [in] The element to find.
 * @param pElements     [in] The array of elements to sort.
 * @param nElementCount [in] The number of elements in the array.
 * @param nElementSize  [in] The size a single element in the array.
 * @param pfnCompare    [in] The function used to compare elements.
 * @param pContext      [in] A context that is passed to the compare function.
 */
OPCUA_EXPORT 
OpcUa_Void* OpcUa_BSearch(  OpcUa_Void*       pKey,
                            OpcUa_Void*       pElements, 
                            OpcUa_UInt32      nElementCount, 
                            OpcUa_UInt32      nElementSize, 
                            OpcUa_PfnCompare* pfnCompare, 
                            OpcUa_Void*       pContext);

/** 
 * @brief Returns the CRT errno constant.
 */
OPCUA_EXPORT 
OpcUa_UInt32 OpcUa_GetLastError();

/** 
 * @brief Returns the number of milliseconds since the system or process was started.
 */
OPCUA_EXPORT 
OpcUa_UInt32 OpcUa_GetTickCount();

/** 
 * @brief Convert string to integer.
 */
#define OpcUa_CharAToInt(xChar) OpcUa_ProxyStub_g_PlatformLayerCalltable->CharToInt(xChar)

OPCUA_END_EXTERN_C

#endif /* _OpcUa_Utilities_H_ */
