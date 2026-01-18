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

#ifndef _OpcUa_Timer_H_
#define _OpcUa_Timer_H_ 1

OPCUA_BEGIN_EXTERN_C

#define OPCUA_P_TIMER_CREATE  OpcUa_ProxyStub_g_PlatformLayerCalltable->TimerCreate
#define OPCUA_P_TIMER_DELETE  OpcUa_ProxyStub_g_PlatformLayerCalltable->TimerDelete
#define OPCUA_P_CLEANUPTIMERS OpcUa_ProxyStub_g_PlatformLayerCalltable->TimersCleanup

typedef OpcUa_StatusCode (OPCUA_DLLCALL OpcUa_Timer_Callback)(  OpcUa_Void*             pvCallbackData, 
                                                                OpcUa_Timer             hTimer,
                                                                OpcUa_UInt32            msecElapsed);

OPCUA_EXPORT OpcUa_StatusCode OPCUA_DLLCALL OpcUa_Timer_Create( OpcUa_Timer*            hTimer,
                                                                OpcUa_UInt32            msecInterval, 
                                                                OpcUa_Timer_Callback*   fpTimerCallback,
                                                                OpcUa_Timer_Callback*   fpKillCallback,
                                                                OpcUa_Void*             pvCallbackData);

OPCUA_EXPORT OpcUa_StatusCode OPCUA_DLLCALL OpcUa_Timer_Delete( OpcUa_Timer*            phTimer);

OPCUA_END_EXTERN_C

#endif /*_OpcUa_Timer_H_ */
