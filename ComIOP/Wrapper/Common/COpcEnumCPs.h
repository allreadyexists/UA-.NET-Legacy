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

#ifndef _COpcEnumCPs_H_
#define _COpcEnumCPs_H_

#if _MSC_VER >= 1000
#pragma once
#endif // _MSC_VER >= 1000

#include "OpcDefs.h"
#include "COpcComObject.h"
#include "COpcCPContainer.h"

//==============================================================================
// CLASS:   COpcEnumCPs
// PURPOSE: Implements the IEnumConnectionPoints interface.
// NOTES:

class OPCUTILS_API COpcEnumCPs 
:
    public COpcComObject,
    public IEnumConnectionPoints
{     
    OPC_BEGIN_INTERFACE_TABLE(COpcEnumCPs)
        OPC_INTERFACE_ENTRY(IEnumConnectionPoints)
    OPC_END_INTERFACE_TABLE()

    OPC_CLASS_NEW_DELETE()

public:

    //==========================================================================
    // Operators

    // Constructor
    COpcEnumCPs();
    
    // Constructor
    COpcEnumCPs(const COpcList<COpcConnectionPoint*>& cCPs);

    // Destructor 
    ~COpcEnumCPs();

    //==========================================================================
    // IEnumConnectionPoints

    // Next
    STDMETHODIMP Next(
        ULONG              cConnections,
        LPCONNECTIONPOINT* ppCP,
        ULONG*             pcFetched
    );

    // Skip
    STDMETHODIMP Skip(ULONG cConnections);

    // Reset
    STDMETHODIMP Reset();

    // Clone
    STDMETHODIMP Clone(IEnumConnectionPoints** ppEnum);

private:

    //==========================================================================
    // Private Members

    OPC_POS                 m_pos;
    COpcConnectionPointList m_cCPs;
};

#endif // _COpcEnumCPs_H_
