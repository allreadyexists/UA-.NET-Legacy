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

#include <opcua_builtintypes.h>
#include <opcua_stream.h>
#include <opcua_messagecontext.h>
#include <opcua_encoder.h>
#include <opcua_decoder.h>

#ifndef _OpcUa_BinaryEncoder_H_
#define _OpcUa_BinaryEncoder_H_ 1

OPCUA_BEGIN_EXTERN_C

/** 
  @brief Used to create an instance of a binary encoder.
 
  @param ppEncoder [out] The encoder.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_BinaryEncoder_Create(
    OpcUa_Encoder**       ppEncoder);


/** 
  @brief Used to create an instance of a binary decoder.
 
  @param ppDecoder [out] The decoder.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_BinaryDecoder_Create(
    OpcUa_Decoder** ppDecoder);

/** 
  @brief Writes a Boolean value.
 
  @param bValue [in] The value to encode.
  @param pOstrm [in] The stream.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_Boolean_BinaryEncode(
    OpcUa_Boolean       bValue,
    OpcUa_OutputStream* pOstrm);

/** 
  @brief Reads a Boolean value.
 
  @param pValue [out] The decoded value.
  @param pIstrm [in]  The stream.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_Boolean_BinaryDecode(
    OpcUa_Boolean*     pValue,
    OpcUa_InputStream* pIstrm);

/** 
  @brief Writes a SByte value.
 
  @param nValue [in] The value to encode.
  @param pOstrm [in] The stream.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_SByte_BinaryEncode(
    OpcUa_SByte         nValue,
    OpcUa_OutputStream* pOstrm);

/** 
  @brief Reads a SByte value.
 
  @param pValue [out] The decoded value.
  @param pIstrm [in]  The stream.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_SByte_BinaryDecode(
    OpcUa_SByte*       pValue,
    OpcUa_InputStream* pIstrm);

/** 
  @brief Writes a Byte value.
 
  @param nValue [in] The value to encode.
  @param pOstrm [in] The stream.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_Byte_BinaryEncode(
    OpcUa_Byte          nValue,
    OpcUa_OutputStream* pOstrm);
/** 
  @brief Reads a Byte value.
 
  @param pValue [out] The decoded value.
  @param pIstrm [in]  The stream.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_Byte_BinaryDecode(
    OpcUa_Byte*        pValue,
    OpcUa_InputStream* pIstrm);

/** 
  @brief Writes a Int16 value.
 
  @param nValue [in] The value to encode.
  @param pOstrm [in] The stream.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_Int16_BinaryEncode(
    OpcUa_Int16         nValue,
    OpcUa_OutputStream* pOstrm);
/** 
  @brief Reads a Int16 value.
 
  @param pValue [out] The decoded value.
  @param pIstrm [in]  The stream.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_Int16_BinaryDecode(
    OpcUa_Int16*       pValue,
    OpcUa_InputStream* pIstrm);

/** 
  @brief Writes a UInt16 value.
 
  @param nValue [in] The value to encode.
  @param pOstrm [in] The stream.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_UInt16_BinaryEncode(
    OpcUa_UInt16        nValue,
    OpcUa_OutputStream* pOstrm);

/** 
  @brief Reads a UInt16 value.
 
  @param pValue [out] The decoded value.
  @param pIstrm [in]  The stream.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_UInt16_BinaryDecode(
    OpcUa_UInt16*      pValue,
    OpcUa_InputStream* pIstrm);
/** 
  @brief Writes a Int32 value.
 
  @param nValue [in] The value to encode.
  @param pOstrm [in] The stream.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_Int32_BinaryEncode(
    OpcUa_Int32         nValue,
    OpcUa_OutputStream* pOstrm);

/** 
  @brief Reads a Int32 value.
 
  @param pValue [out] The decoded value.
  @param pIstrm [in]  The stream.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_Int32_BinaryDecode(
    OpcUa_Int32*       pValue,
    OpcUa_InputStream* pIstrm);

/** 
  @brief Writes a UInt32 value.
 
  @param nValue [in] The value to encode.
  @param pOstrm [in] The stream.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_UInt32_BinaryEncode(
    OpcUa_UInt32        nValue,
    OpcUa_OutputStream* pOstrm);

/** 
  @brief Reads a UInt32 value.
 
  @param pValue [out] The decoded value.
  @param pIstrm [in]  The stream.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_UInt32_BinaryDecode(
    OpcUa_UInt32*      pValue,
    OpcUa_InputStream* pIstrm);

/** 
  @brief Writes a Int64 value.
 
  @param nValue [in] The value to encode.
  @param pOstrm [in] The stream.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_Int64_BinaryEncode(
    OpcUa_Int64         nValue,
    OpcUa_OutputStream* pOstrm);

/** 
  @brief Reads a Int64 value.
 
  @param pValue [out] The decoded value.
  @param pIstrm [in]  The stream.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_Int64_BinaryDecode(
    OpcUa_Int64*       pValue,
    OpcUa_InputStream* pIstrm);

/** 
  @brief Writes a UInt64 value.
 
  @param nValue [in] The value to encode.
  @param pOstrm [in] The stream.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_UInt64_BinaryEncode(
    OpcUa_UInt64        nValue,
    OpcUa_OutputStream* pOstrm);

/** 
  @brief Reads a UInt64 value.
 
  @param pValue [out] The decoded value.
  @param pIstrm [in]  The stream.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_UInt64_BinaryDecode(
    OpcUa_UInt64*      pValue,
    OpcUa_InputStream* pIstrm);

/** 
  @brief Writes a Float value.
 
  @param nValue [in] The value to encode.
  @param pOstrm [in] The stream.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_Float_BinaryEncode(
    OpcUa_Float         nValue,
    OpcUa_OutputStream* pOstrm);

/** 
  @brief Reads a Float value.
 
  @param pValue [out] The decoded value.
  @param pIstrm [in]  The stream.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_Float_BinaryDecode(
    OpcUa_Float*       pValue,
    OpcUa_InputStream* pIstrm);

/** 
  @brief Writes a Double value.
 
  @param nValue [in] The value to encode.
  @param pOstrm [in] The stream.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_Double_BinaryEncode(
    OpcUa_Double        nValue,
    OpcUa_OutputStream* pOstrm);

/** 
  @brief Reads a Double value.
 
  @param pValue [out] The decoded value.
  @param pIstrm [in]  The stream.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_Double_BinaryDecode(
    OpcUa_Double*      pValue,
    OpcUa_InputStream* pIstrm);

/** 
  @brief Writes a String value.
 
  @param pValue [in] The value to encode.
  @param pOstrm [in] The stream.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_String_BinaryEncode(
    OpcUa_String*       pValue,
    OpcUa_OutputStream* pOstrm);

/** 
  @brief Reads a String value.
 
  @param pValue     [out] The decoded value.
  @param nMaxLength [in]  The maximum length for the decoded string (0 means no limit).
  @param pIstrm     [in]  The stream.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_String_BinaryDecode(
    OpcUa_String*      pValue,
    OpcUa_UInt32       nMaxLength,
    OpcUa_InputStream* pIstrm);

/** 
  @brief Writes a DateTime value.
 
  @param pValue [in] The value to encode.
  @param pOstrm [in] The stream.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_DateTime_BinaryEncode(
    OpcUa_DateTime*     pValue,
    OpcUa_OutputStream* pOstrm); 

/** 
  @brief Reads a DateTime value.
 
  @param pValue [out] The decoded value.
  @param pIstrm [in]  The stream.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_DateTime_BinaryDecode(
    OpcUa_DateTime*    pValue,
    OpcUa_InputStream* pIstrm);

/** 
  @brief Writes a Guid value.
 
  @param pValue [in] The value to encode.
  @param pOstrm [in] The stream.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_Guid_BinaryEncode(
    OpcUa_Guid*         pValue,
    OpcUa_OutputStream* pOstrm);

/** 
  @brief Reads a Guid value.
 
  @param pValue [out] The decoded value.
  @param pIstrm [in]  The stream.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_Guid_BinaryDecode(
    OpcUa_Guid*        pValue,
    OpcUa_InputStream* pIstrm);

/** 
  @brief Writes a ByteString value.
 
  @param pValue [in] The value to encode.
  @param pOstrm [in] The stream.
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_ByteString_BinaryEncode(
    OpcUa_ByteString*   pValue,
    OpcUa_OutputStream* pOstrm);

/** 
  @brief Reads a ByteString value.
 
  @param pValue     [out] The decoded value.
  @param nMaxLength [in]  The maximum length for the decoded byte string (0 means no limit).
  @param pIstrm     [in]  The stream.
*/
OpcUa_StatusCode OpcUa_ByteString_BinaryDecode(
    OpcUa_ByteString*  pValue,
    OpcUa_UInt32       nMaxLength,
    OpcUa_InputStream* pIstrm);

OPCUA_END_EXTERN_C

#endif /* _OpcUa_BinaryEncoder_H_ */
