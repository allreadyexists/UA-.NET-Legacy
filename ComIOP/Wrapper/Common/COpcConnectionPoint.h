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

#ifndef _COpcConnectionPoint_H_
#define _COpcConnectionPoint_H_

#if _MSC_VER >= 1000
#pragma once
#endif // _MSC_VER >= 1000

#include "ocidl.h"

#include "OpcDefs.h"
#include "COpcComObject.h"
#include "COpcCriticalSection.h"

class COpcCPContainer;

//==============================================================================
// CLASS:   COpcConnectionPoint
// PURPOSE: Implements the IConnectionPoint interface.
// NOTES:

class OPCUTILS_API COpcConnectionPoint
: 
    public COpcComObject,
    public COpcSynchObject,
    public IConnectionPoint
{
    OPC_BEGIN_INTERFACE_TABLE(COpcConnectionPoint)
        OPC_INTERFACE_ENTRY(IConnectionPoint)
    OPC_END_INTERFACE_TABLE()

    OPC_CLASS_NEW_DELETE()

public:

    //==========================================================================
    // Operators

    // Constructor
    COpcConnectionPoint();

    // Constructor
    COpcConnectionPoint(const IID& tIid, COpcCPContainer* pContainer);

    // Destructor 
    ~COpcConnectionPoint();

    //==========================================================================
    // IConnectionPoint

    // GetConnectionInterface
    STDMETHODIMP GetConnectionInterface(IID* pIID);

    // GetConnectionPointContainer
    STDMETHODIMP GetConnectionPointContainer(IConnectionPointContainer** ppCPC);

    // Advise
    STDMETHODIMP Advise(IUnknown* pUnkSink, DWORD* pdwCookie);

    // Unadvise
    STDMETHODIMP Unadvise(DWORD dwCookie);

    // EnumConnections
    STDMETHODIMP EnumConnections(IEnumConnections** ppEnum);

    //==========================================================================
    // Public Methods

    // GetCallback
    IUnknown* GetCallback() { return m_ipCallback; }

    // GetInterface
    const IID& GetInterface() { return m_tInterface; }

    // Delete
    bool Delete();

    // IsConnected
    bool IsConnected() { return (m_dwCookie != NULL); }
    
private:

    //==========================================================================
    // Private Members

    IID              m_tInterface;
    COpcCPContainer* m_pContainer;
    IUnknown*        m_ipCallback;
    DWORD            m_dwCookie;
    bool             m_bFetched;
};

//==============================================================================
// FUNCTION: OpcConnect
// PURPOSE:  Establishes a connection to the server.

OPCUTILS_API HRESULT OpcConnect(
    IUnknown* ipSource, 
    IUnknown* ipSink, 
    REFIID    riid, 
    DWORD*    pdwConnection);

//==============================================================================
// FUNCTION: OpcDisconnect
// PURPOSE:  Closes a connection to the server.

OPCUTILS_API HRESULT OpcDisconnect(
    IUnknown* ipSource, 
    REFIID    riid, 
    DWORD     dwConnection);

#endif // _COpcConnectionPoint_H_
