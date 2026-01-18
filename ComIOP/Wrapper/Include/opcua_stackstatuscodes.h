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

#ifndef _OpcUa_StackStatusCodes_H_
#define _OpcUa_StackStatusCodes_H_ 1

OPCUA_BEGIN_EXTERN_C

/*============================================================================
 * Begin of status codes internal to the stack.
 *===========================================================================*/
#define OpcUa_StartOfStackStatusCodes 0x81000000

/*============================================================================
 * The message signature is invalid.
 *===========================================================================*/
#define OpcUa_BadSignatureInvalid 0x81010000

/*============================================================================
 * The extensible parameter provided is not a valid for the service.
 *===========================================================================*/
#define OpcUa_BadExtensibleParameterInvalid 0x81040000

/*============================================================================
 * The extensible parameter provided is valid but the server does not support it.
 *===========================================================================*/
#define OpcUa_BadExtensibleParameterUnsupported 0x81050000

/*============================================================================
 * The hostname could not be resolved.
 *===========================================================================*/
#define OpcUa_BadHostUnknown 0x81060000

/*============================================================================
 * Too many posts were made to a semaphore.
 *===========================================================================*/
#define OpcUa_BadTooManyPosts 0x81070000

/*============================================================================
 * The security configuration is not valid.
 *===========================================================================*/
#define OpcUa_BadSecurityConfig 0x81080000

/*============================================================================
 * Invalid file name specified.
 *===========================================================================*/
#define OpcUa_BadFileNotFound 0x81090000

OPCUA_END_EXTERN_C

#endif /* _OpcUa_StackStatusCodes_H_ */
