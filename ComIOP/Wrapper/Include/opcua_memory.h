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

#ifndef _OpcUa_Memory_H_
#define _OpcUa_Memory_H_ 1

OPCUA_BEGIN_EXTERN_C

/** 
 * @brief Allocates a new block of memory.
 *
 * @param nSize [in] The size of the block to allocate.
*/
OPCUA_EXPORT OpcUa_Void* OPCUA_DLLCALL OpcUa_Memory_Alloc(OpcUa_UInt32 nSize);

/** 
 * @brief Reallocates a block of memory
 *
 * @param pBuffer [in] The existing memory block.
 * @param nSize   [in] The size of the block to allocate.
*/
OPCUA_EXPORT OpcUa_Void* OPCUA_DLLCALL OpcUa_Memory_ReAlloc(OpcUa_Void*  pBuffer, 
                                                            OpcUa_UInt32 nSize);

/** 
 * @brief Frees a block of memory.
 *
 * @param pvBuffer [in] The existing memory block.
*/
OPCUA_EXPORT OpcUa_Void OPCUA_DLLCALL OpcUa_Memory_Free(OpcUa_Void* pvBuffer);

/** 
 * @brief Copies a block of memory.
 *
 * @param pBuffer      [in] The destination memory block.
 * @param nSizeInBytes [in] The size of the destination memory block. 
 * @param pSource      [in] The memory block being copied.
 * @param nCount       [in] The number of bytes to copy.
 *
 * @return StatusCode:
 *   OpcUa_BadInvalidArgument if Buffer or Source equals OpcUa_Null;
 *   OpcUa_BadOutOfRange      if number of bytes to copy greater nSizeInBytes
*/
OPCUA_EXPORT OpcUa_StatusCode OPCUA_DLLCALL OpcUa_Memory_MemCpy(   OpcUa_Void*  pBuffer,
    OpcUa_UInt32 nSizeInBytes,
    OpcUa_Void*  pSource,
    OpcUa_UInt32 nCount);

OPCUA_END_EXTERN_C

#endif /* _OpcUa_Memory_H_ */
