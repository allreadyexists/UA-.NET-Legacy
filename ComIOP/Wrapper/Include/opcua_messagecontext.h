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

#ifndef _OpcUa_MessageContext_H_
#define _OpcUa_MessageContext_H_ 1

#include <opcua_stringtable.h>
#include <opcua_builtintypes.h>
#include <opcua_encodeableobject.h>

OPCUA_BEGIN_EXTERN_C

/** 
  @brief Stores data used to construct a message context.
*/
typedef struct _OpcUa_MessageContext
{
    /*! @brief The table of namespace URIs used by the server (memory not owned by the context). */
    OpcUa_StringTable* NamespaceUris;

    /*! @brief The table of known encodeable types. */
    OpcUa_EncodeableTypeTable* KnownTypes;

    /*! @brief Whether the encoder should always calculate the size of the encodeable objects (used for debugging) */
    OpcUa_Boolean AlwaysCheckLengths;

    /*! @brief The maximum length for any array. */
    OpcUa_UInt32 MaxArrayLength;

    /*! @brief The maximum length for any String value. */
    OpcUa_UInt32 MaxStringLength;

    /*! @brief The maximum length for any ByteString value. */
    OpcUa_UInt32 MaxByteStringLength;

    /*! @brief The maximum length for any message. */
    OpcUa_UInt32 MaxMessageLength;
}
OpcUa_MessageContext;

/** 
  @brief Puts the context into a known state.

  @param pContext [in] The context to initialize.
*/
OPCUA_EXPORT OpcUa_Void OpcUa_MessageContext_Initialize(
    OpcUa_MessageContext* pContext);

/** 
  @brief Frees all memory used by a string context.

  @param pContext [in] The context to clear.
*/
OPCUA_EXPORT OpcUa_Void OpcUa_MessageContext_Clear(
    OpcUa_MessageContext* pContext);

/** 
  @brief Adds a new encoded object position to the context.

  @param pContext [in] The context to update.
  @param nStart   [in] The stream position for the first byte of the object.
  @param nEnd     [in] The stream position immediately after the last byte of the object.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_MessageContext_SaveObjectPosition(
    OpcUa_MessageContext* pContext,
    OpcUa_UInt32          nStart,
    OpcUa_UInt32          nEnd);

/** 
  @brief Gets the length of an object at the specified position.

  @param pContext [in] The context to search.
  @param nStart   [in] The stream position for the first byte of the object.
  @param iLength  [in] The length of the encoded object.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_MessageContext_GetObjectLength(
    OpcUa_MessageContext* pContext,
    OpcUa_UInt32          nStart,
    OpcUa_Int32*          iLength);

OPCUA_END_EXTERN_C

#endif /* _OpcUa_MessageContext_H_ */
