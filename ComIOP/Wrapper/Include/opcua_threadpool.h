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

#ifndef _OpcUa_ThreadPool_H_
#define _OpcUa_ThreadPool_H_ 1

#if OPCUA_HAVE_THREADPOOL

/** 
 * @brief Threadpool Handle.
 */
typedef OpcUa_Void* OpcUa_ThreadPool;

OPCUA_BEGIN_EXTERN_C

/** 
 * @brief Create a thread pool with uMinThreads static threads and uMaxThreads - uMinThreads dynamic threads.
 */
OPCUA_EXPORT OpcUa_StatusCode   OPCUA_DLLCALL OpcUa_ThreadPool_Create(  OpcUa_ThreadPool*       phThreadPool,
                                                                        OpcUa_UInt32            uMinThreads,
                                                                        OpcUa_UInt32            uMaxThreads,
                                                                        OpcUa_UInt32            uMaxJobs,
                                                                        OpcUa_Boolean           bBlockIfFull,
                                                                        OpcUa_UInt32            uTimeout);

/** 
 * @brief Destroy a thread pool.
 */
OPCUA_EXPORT OpcUa_Void         OPCUA_DLLCALL OpcUa_ThreadPool_Delete(  OpcUa_ThreadPool*       phThreadPool);

/** 
 * @brief Assing a job to a thread pool. The job may be queued for later execution.
 */
OPCUA_EXPORT OpcUa_StatusCode   OPCUA_DLLCALL OpcUa_ThreadPool_AddJob(  OpcUa_ThreadPool        hThreadPool,
                                                                        OpcUa_PfnThreadMain*    pFunction,
                                                                        OpcUa_Void*             pArgument);

OPCUA_END_EXTERN_C

#endif /* OPCUA_HAVE_THREADPOOL */

#endif /* _OpcUa_ThreadPool_H_ */
