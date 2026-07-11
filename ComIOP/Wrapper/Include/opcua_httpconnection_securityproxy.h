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

#ifndef _OpcUa_HttpConnection_SecyrityProxy_H_
#define _OpcUa_HttpConnection_SecyrityProxy_H_ 1

#include <opcua_connection.h>

OPCUA_BEGIN_EXTERN_C

/** 
  @brief Creates a new security proxy object.
 
  @param a_ppConnection [out] The new connection.
*/
OpcUa_StatusCode OpcUa_HttpConnection_SecurityProxy_Create(
    OpcUa_Connection** a_ppConnection);

OPCUA_END_EXTERN_C

#endif /* _OpcUa_HttpConnection_SecyrityProxy_H_ */
