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

#ifndef _OpcUa_Base64_H_
#define _OpcUa_Base64_H_ 1

#ifdef OPCUA_HAVE_BASE64

OPCUA_BEGIN_EXTERN_C

OpcUa_StatusCode OpcUa_Base64_Encode(
    OpcUa_Byte*     a_pBytes,
    OpcUa_Int32     a_iByteCount,
    OpcUa_StringA*  a_psString);

OpcUa_StatusCode OpcUa_Base64_Decode(
    OpcUa_StringA   a_sString,
    OpcUa_Int32*    a_piByteCount,
    OpcUa_Byte**    a_ppBytes);

OPCUA_END_EXTERN_C

#endif /* OPCUA_HAVE_BASE64 */
#endif /* _OpcUa_Base64_H_ */
