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

#ifndef _OpcUa_ServiceTable_H_
#define _OpcUa_ServiceTable_H_ 1
#if OPCUA_HAVE_SERVERAPI

#include <opcua_encodeableobject.h>
#include <opcua_types.h>


struct _OpcUa_OutputStream;

OPCUA_BEGIN_EXTERN_C


/** 
  @brief Describes a service supported by an endpoint.
*/
/*typedef struct _OpcUa_ServiceType OpcUa_ServiceType;*/

/** 
  @brief Stores a table of services supported by an endpoint.
*/
typedef struct _OpcUa_ServiceTable
{
    /*! @brief The number of services. */
    OpcUa_UInt32 Count;

    /*! @brief An array of all supported services. */
    OpcUa_ServiceType* Entries;
}
OpcUa_ServiceTable;

/** 
  @brief Populates a service table from a null terminated list of service types.

  This functions sorts the entries in the service table to optimize look ups with 
  the FindService function.

  @param pTable [in] The table to initialize.
  @param pTypes [in] A null terminated list of service types.
*/
OpcUa_StatusCode OpcUa_ServiceTable_AddTypes(
    OpcUa_ServiceTable* pTable,
    OpcUa_ServiceType** pTypes);

/** 
  @brief Frees all memory owned by a service table.

  @param pTable [in] The table to clear.
*/
OpcUa_Void OpcUa_ServiceTable_Clear(OpcUa_ServiceTable* pTable);

/** 
  @brief Finds a service type in a table.

  This table should have been sorted with a call to Initialize first.

  @param pTable  [in]     The table to search.
  @param nTypeId [in]     The identifier for the service.
  @param pType   [in/out] The service type.
*/
OpcUa_StatusCode OpcUa_ServiceTable_FindService(
    OpcUa_ServiceTable* pTable,
    OpcUa_UInt32        nTypeId,
    OpcUa_ServiceType*  pType);

/** 
  @brief Creates a fault response for a service.

  The diagnostic and string information is stored in the fault object.

  @param pRequestHeader      [in]     The header associated with the request.
  @param uServiceResult      [in]     The service result.
  @param pServiceDiagnostics [in/out] The service diagnostics.
  @param nNoOfStringTable    [in/out] The number of string in the table.
  @param pStringTable        [in/out] A table of diagnostic strings.
  @param ppFault             [out]    The fault object.
  @param ppFaultType         [out]    The type of fault.
*/
OPCUA_EXPORT 
OpcUa_StatusCode OpcUa_ServerApi_CreateFault(
    OpcUa_RequestHeader*   pRequestHeader,
    OpcUa_StatusCode       uServiceResult,
    OpcUa_DiagnosticInfo*  pServiceDiagnostics,
    OpcUa_Int32*           pNoOfStringTable,
    OpcUa_String**         ppStringTable,
    OpcUa_Void**           ppFault,
    OpcUa_EncodeableType** ppFaultType);

OPCUA_END_EXTERN_C

#endif /* OPCUA_HAVE_SERVERAPI */
#endif /* _OpcUa_ServiceTable_H_ */
