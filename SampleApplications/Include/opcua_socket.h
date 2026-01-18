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

#ifndef _OpcUa_Socket_H_
#define _OpcUa_Socket_H_ 1

OPCUA_BEGIN_EXTERN_C

#define OPCUA_P_SOCKETMANAGER_CREATE        OpcUa_ProxyStub_g_PlatformLayerCalltable->SocketManagerCreate
#define OPCUA_P_SOCKETMANAGER_DELETE        OpcUa_ProxyStub_g_PlatformLayerCalltable->SocketManagerDelete
#define OPCUA_P_SOCKETMANAGER_CREATESERVER  OpcUa_ProxyStub_g_PlatformLayerCalltable->SocketManagerCreateServer
#define OPCUA_P_SOCKETMANAGER_CREATECLIENT  OpcUa_ProxyStub_g_PlatformLayerCalltable->SocketManagerCreateClient
#define OPCUA_P_SOCKETMANAGER_SIGNALEVENT   OpcUa_ProxyStub_g_PlatformLayerCalltable->SocketManagerSignalEvent
#define OPCUA_P_SOCKETMANAGER_SERVELOOP     OpcUa_ProxyStub_g_PlatformLayerCalltable->SocketManagerServeLoop

#define OPCUA_P_SOCKET_READ                 OpcUa_ProxyStub_g_PlatformLayerCalltable->SocketRead
#define OPCUA_P_SOCKET_WRITE                OpcUa_ProxyStub_g_PlatformLayerCalltable->SocketWrite
#define OPCUA_P_SOCKET_CLOSE                OpcUa_ProxyStub_g_PlatformLayerCalltable->SocketClose
#define OPCUA_P_SOCKET_GETPEERINFO          OpcUa_ProxyStub_g_PlatformLayerCalltable->SocketGetPeerInfo
#define OPCUA_P_SOCKET_CHANGEEVENTLIST      /* Todo */
#define OPCUA_P_SOCKET_GETLASTERROR         OpcUa_ProxyStub_g_PlatformLayerCalltable->SocketGetLastError

#define OPCUA_P_INITIALIZENETWORK           OpcUa_ProxyStub_g_PlatformLayerCalltable->NetworkInitialize
#define OPCUA_P_CLEANUPNETWORK              OpcUa_ProxyStub_g_PlatformLayerCalltable->NetworkCleanup

OPCUA_END_EXTERN_C

#endif /* _OpcUa_Socket_H_ */
