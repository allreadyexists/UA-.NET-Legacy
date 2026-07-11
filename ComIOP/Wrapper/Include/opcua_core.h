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

/* Core functionalities, exported to applications. */

#ifndef _OpcUa_Core_H_
#define _OpcUa_Core_H_ 1

#include <opcua_datetime.h>
#include <opcua_guid.h>
#include <opcua_memory.h>
#include <opcua_thread.h>
#include <opcua_string.h>
#include <opcua_timer.h>
#include <opcua_trace.h>
#include <opcua_utilities.h>

OPCUA_BEGIN_EXTERN_C

/* mutex */
OPCUA_EXPORT OpcUa_StatusCode OPCUA_DLLCALL OpcUa_Mutex_Create   (      OpcUa_Mutex* phNewMutex);   
OPCUA_EXPORT OpcUa_Void       OPCUA_DLLCALL OpcUa_Mutex_Delete   (      OpcUa_Mutex* phMutex);   
OPCUA_EXPORT OpcUa_Void       OPCUA_DLLCALL OpcUa_Mutex_Lock     (      OpcUa_Mutex  hMutex);   
OPCUA_EXPORT OpcUa_Void       OPCUA_DLLCALL OpcUa_Mutex_Unlock   (      OpcUa_Mutex  hMutex);  

/* utils */
OPCUA_EXPORT OpcUa_DateTime   OPCUA_DLLCALL OpcUa_DateTime_UtcNow();
OPCUA_EXPORT OpcUa_UInt32     OPCUA_DLLCALL OpcUa_Utility_GetTickCount();
                                   
/* Semaphore */                                     
OPCUA_EXPORT OpcUa_StatusCode OPCUA_DLLCALL OpcUa_Semaphore_Create   (  OpcUa_Semaphore*    phNewSemaphore, 
                                                                        OpcUa_UInt32        uInitalValue,
                                                                        OpcUa_UInt32        uMaxRange);
OPCUA_EXPORT OpcUa_Void       OPCUA_DLLCALL OpcUa_Semaphore_Delete   (  OpcUa_Semaphore*    phSemaphore);
OPCUA_EXPORT OpcUa_StatusCode OPCUA_DLLCALL OpcUa_Semaphore_Wait     (  OpcUa_Semaphore     hSemaphore);
OPCUA_EXPORT OpcUa_StatusCode OPCUA_DLLCALL OpcUa_Semaphore_TimedWait(  OpcUa_Semaphore     hSemaphore, 
                                                                        OpcUa_UInt32        msecTimeout);
OPCUA_EXPORT OpcUa_StatusCode OPCUA_DLLCALL OpcUa_Semaphore_Post     (  OpcUa_Semaphore     hSemaphore,
                                                                        OpcUa_UInt32        uReleaseCount);


OPCUA_EXPORT OpcUa_StatusCode OPCUA_DLLCALL OpcUa_SocketManager_Create( OpcUa_SocketManager*  ppSocketManager, 
                                                                        OpcUa_UInt32          nSockets, 
                                                                        OpcUa_UInt32          uintFlags);



OPCUA_EXPORT OpcUa_Void OPCUA_DLLCALL OpcUa_SocketManager_Delete(       OpcUa_SocketManager* pSocketManager);



OPCUA_EXPORT OpcUa_StatusCode OPCUA_DLLCALL OpcUa_SocketManager_CreateServer(   OpcUa_SocketManager         pSocketManager,
                                                                                OpcUa_StringA               LocalAdress,
                                                                                OpcUa_Socket_EventCallback  pfnSocketCallBack,
                                                                                OpcUa_Void*                 pCookie,
                                                                                OpcUa_Socket*               ppSocket);


OPCUA_EXPORT OpcUa_StatusCode OPCUA_DLLCALL OpcUa_SocketManager_CreateClient(   OpcUa_SocketManager         pSocketManager,
                                                                                OpcUa_StringA               RemoteAdress,
                                                                                OpcUa_UInt16                LocalPort,
                                                                                OpcUa_Socket_EventCallback  pfnSocketCallBack,
                                                                                OpcUa_Void*                 pCookie,
                                                                                OpcUa_Socket*               ppSocket);

OPCUA_EXPORT OpcUa_StatusCode OPCUA_DLLCALL OpcUa_Socket_Close(         OpcUa_Socket                pSocket);

OPCUA_EXPORT OpcUa_StatusCode OPCUA_DLLCALL OpcUa_Socket_Read(          OpcUa_Socket                pSocket,
                                                                        OpcUa_Byte*                 pBuffer,
                                                                        OpcUa_UInt32                BufferSize,
                                                                        OpcUa_UInt32*               puintBytesRead);

OPCUA_EXPORT OpcUa_Int32      OPCUA_DLLCALL OpcUa_Socket_Write(         OpcUa_Socket                pSocket,
                                                                        OpcUa_Byte*                 pBuffer,
                                                                        OpcUa_UInt32                BufferSize,
                                                                        OpcUa_Boolean               bBlock);

OPCUA_EXPORT OpcUa_StatusCode OPCUA_DLLCALL OpcUa_SocketManager_Loop(   OpcUa_SocketManager pSocketManager, 
                                                                        OpcUa_UInt32        msecTimeout,
                                                                        OpcUa_Boolean       bRunOnce);

OPCUA_EXPORT OpcUa_StatusCode OPCUA_DLLCALL OpcUa_SocketManager_SignalEvent(OpcUa_SocketManager pSocketManager,
                                                                        OpcUa_UInt32        uintEvent,
                                                                        OpcUa_Boolean       bAllLists); 

/* PKI */
OPCUA_EXPORT OpcUa_StatusCode OPCUA_DLLCALL OpcUa_PKIProvider_Create(   OpcUa_Void*         pCertificateStoreConfig,
                                                                        OpcUa_PKIProvider*  pProvider);

OPCUA_EXPORT OpcUa_StatusCode OPCUA_DLLCALL OpcUa_PKIProvider_Delete(   OpcUa_PKIProvider*  pProvider);

/* Crypto */
OPCUA_EXPORT OpcUa_StatusCode OPCUA_DLLCALL OpcUa_CryptoProvider_Create(OpcUa_StringA           psSecurityProfileUri,
                                                                        OpcUa_CryptoProvider*   pProvider);

OPCUA_EXPORT OpcUa_StatusCode OPCUA_DLLCALL OpcUa_CryptoProvider_Delete(OpcUa_CryptoProvider*   pProvider);

/* StringA */
OPCUA_EXPORT OpcUa_Int32      OPCUA_DLLCALL OpcUa_StringA_vsnprintf(    OpcUa_StringA               sDest, 
                                                                        OpcUa_UInt32                uCount, 
                                                                        const OpcUa_CharA*          sFormat, 
                                                                        OpcUa_P_VA_List             argptr);


OPCUA_EXPORT OpcUa_Int32      OPCUA_DLLCALL OpcUa_StringA_snprintf(     OpcUa_StringA               sDest, 
                                                                        OpcUa_UInt32                uCount, 
                                                                        const OpcUa_CharA*          sFormat, 
                                                                        ...);


OPCUA_END_EXTERN_C

#endif /* _OpcUa_Core_H_ */
