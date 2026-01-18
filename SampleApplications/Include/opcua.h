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

#ifndef _OpcUa_H_
#define _OpcUa_H_

/******************************************************************/
/* STACK INTERNAL FILE - NOT MEANT TO BE INCLUDED BY APPLICATIONS */
/******************************************************************/

/* Collection of includes needed everywhere or at least very often.   */
/* All other includes MUST be included in the source file!            */
#include <opcua_platformdefs.h> /* includes typemapping of primitives */
#include <opcua_proxystub.h>
#include <opcua_statuscodes.h>
#include <opcua_stackstatuscodes.h>
#include <opcua_errorhandling.h>

#include <opcua_string.h>
#include <opcua_memory.h>
#include <opcua_trace.h>

/* platform interface and referenced stack files */
#include <opcua_types.h>       /* needed for some security related files in p_interface */
#include <opcua_crypto.h>      /* needed for some security related files in p_interface */
#include <opcua_pki.h>
#include <opcua_p_interface.h> /* standalone file */

/* Do not include headers in headers if not absolutely necessary.   */
/* This creates a cascade of includes up to the libraries API level.*/


OPCUA_BEGIN_EXTERN_C

/* import  */
extern OpcUa_Port_CallTable*            OpcUa_ProxyStub_g_PlatformLayerCalltable;
extern OpcUa_ProxyStubConfiguration     OpcUa_ProxyStub_g_Configuration;

/*============================================================================
 * OpcUa_InitializeArray
 *===========================================================================*/
#define OpcUa_InitializeArray(xArray, xLength, xType) \
{ \
    int ii; \
    \
    for (ii = 0; ii < xLength; ii++) \
    { \
        Initialize_##xType(&((xArray)[ii])); \
    } \
}

/*============================================================================
 * OpcUa_ClearArray
 *===========================================================================*/
#define OpcUa_ClearArray(xArray, xLength, xType) \
{ \
    int ii; \
    \
    for (ii = 0; ii < xLength; ii++) \
    { \
        Clear_##xType(&((xArray)[ii])); \
    } \
}

/*============================================================================
 * OpcUa_ProxyStub_RegisterChannel
 *===========================================================================*/
OpcUa_Void OpcUa_ProxyStub_RegisterChannel();

/*============================================================================
 * OpcUa_ProxyStub_RegisterEndpoint
 *===========================================================================*/
OpcUa_Void OpcUa_ProxyStub_RegisterEndpoint();

/*============================================================================
 * OpcUa_ProxyStub_DeRegisterChannel
 *===========================================================================*/
OpcUa_Void OpcUa_ProxyStub_DeRegisterChannel();

/*============================================================================
 * OpcUa_ProxyStub_DeRegisterEndpoint
 *===========================================================================*/
OpcUa_Void OpcUa_ProxyStub_DeRegisterEndpoint();

OPCUA_END_EXTERN_C
#endif /* _OpcUa_H_ */
