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

#ifndef _OpcUa_Guid_H_
#define _OpcUa_Guid_H_ 1

OPCUA_BEGIN_EXTERN_C

/*============================================================================
 * Defines
 *===========================================================================*/

#define OPCUA_GUID_LEXICAL_LENGTH 38 /* length of the lexical representation without trailing limiter! */

/*============================================================================
 * Types
 *===========================================================================*/

/**
  @brief An empty GUID.
*/
OPCUA_IMEXPORT extern OpcUa_Guid OpcUa_Guid_Null;

/**
  @brief Creates a new GUID.

  @param pGuid [bi] The buffer to store the new GUID in.
*/
OPCUA_EXPORT OpcUa_Guid* OpcUa_Guid_Create(OpcUa_Guid* pGuid);

/**
  @brief Converts a UTF-8 string representation of a GUID to a binary representation.

  @param szText  [in] The string representation.
  @param pGuid  [out] The binary representation.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_Guid_FromString(     
    OpcUa_CharA* szText, 
    OpcUa_Guid*  pGuid);

/**
  @brief Converts a binary representation of a GUID to a UTF-8 representation.

  @param pGuid  [in] The binary representation.
  @param szText [bi] The string representation.
*/
OPCUA_EXPORT OpcUa_CharA* OpcUa_Guid_ToStringA( 
    OpcUa_Guid*  pGuid, 
    OpcUa_CharA* szText);

/**
  @brief Converts a binary representation of a GUID to a newly created OpcUa_String.

  @param pGuid   [in]  The binary representation.
  @param pszText [out] The string representation.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_Guid_ToString(   
    OpcUa_Guid*     pGuid, 
    OpcUa_String**  pszText);

/**
  @brief Returns true if the two guids are equal.

  @param pGuid1 [in] The first guid to compare.
  @param pGuid2 [in] The second guid to compare.
*/
OPCUA_EXPORT OpcUa_Boolean OpcUa_Guid_IsEqual(
    OpcUa_Guid* pGuid1, 
    OpcUa_Guid* pGuid2);

/**
  @brief Returns true if the guid is a null guid.

  @param pGuid [in] The guid to test.
*/
OPCUA_EXPORT OpcUa_Boolean OpcUa_Guid_IsNull(OpcUa_Guid* pGuid);

/**
  @brief Copies a guid.

  @param pDstGuid [bi] The guid to change.
  @param pSrcGuid [in] The guid to copy.
*/
OPCUA_EXPORT OpcUa_Void OpcUa_Guid_Copy(
    OpcUa_Guid* pDstGuid, 
    OpcUa_Guid* pSrcGuid);


OPCUA_END_EXTERN_C

#endif /* _OpcUa_Guid_H_ */
