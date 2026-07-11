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

#ifndef _OpcUa_DateTime_H_
#define _OpcUa_DateTime_H_ 1

OPCUA_BEGIN_EXTERN_C

/*============================================================================
 * Functions for OpcUa_DateTime
 *===========================================================================*/

/**
  @brief Returns the UTC time in OpcUa_DateTime format.

  @return the UTC time
*/
#define OPCUA_P_DATETIME_UTCNOW OpcUa_ProxyStub_g_PlatformLayerCalltable->UtcNow

/**
  @brief Convert a string to a date-time

  @return OpcUa_BadInvalidArgument if a_pchDateTimeString is null
  @return OpcUa_BadInvalidArgument if the string is incorrectly formatted
  @return OpcUa_Bad for other failures
  @return OpcUa_Good on success

  @param szDateTimeString  [in] String to convert
  @param pDateTime        [out] Location to store the date-time
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_DateTime_GetDateTimeFromString( OpcUa_CharA*    szDateTimeString,
                                                                    OpcUa_DateTime* pDateTime);

/**
  @brief Convert a date-time to a string

  @return OpcUa_BadInvalidArgument if buffer is null
  @return OpcUa_BadInvalidArgument if the buffer is too short
  @return OpcUa_Good on success

  @param DateTime   [in] Date-time to convert.
  @param pchBuffer  [bi] Byte buffer to store the result (at last 25 bytes long).
  @param uLength    [in] Length of the given buffer in bytes (at least 25).
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_DateTime_GetStringFromDateTime( OpcUa_DateTime  DateTime, 
                                                                    OpcUa_CharA*    pchBuffer,
                                                                    OpcUa_UInt32    uLength);

/*============================================================================
 * Functions for OpcUa_TimeVal
 *===========================================================================*/


/**
  @brief Get the time in OpcUa_TimeVal format

  @return OpcUa_BadInvalidArgument if pValue is null
  @return OpcUa_Good on success

  @param pValue     [out]   Location of an OpcUa_TimeVal to store the time of day
*/
OPCUA_EXPORT OpcUa_StatusCode OpcUa_DateTime_GetTimeOfDay(OpcUa_TimeVal* pValue);


OPCUA_END_EXTERN_C

#endif /* _OpcUa_DateTime_H_ */
