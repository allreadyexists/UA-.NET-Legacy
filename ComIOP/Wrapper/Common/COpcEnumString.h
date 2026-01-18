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

#ifndef _COpcEnumStrings_H_
#define _COpcEnumStrings_H_

#if _MSC_VER >= 1000
#pragma once
#endif // _MSC_VER >= 1000

#include "OpcDefs.h"
#include "COpcComObject.h"
#include "COpcList.h"
#include "COpcString.h"

//==============================================================================
// CLASS:   COpcEnumString
// PURPOSE: A class to implement the IEnumString interface.
// NOTES:

class OPCUTILS_API COpcEnumString 
:
    public COpcComObject,
    public IEnumString
{     
    OPC_BEGIN_INTERFACE_TABLE(COpcEnumString)
        OPC_INTERFACE_ENTRY(IEnumString)
    OPC_END_INTERFACE_TABLE()

    OPC_CLASS_NEW_DELETE()

public:

    //==========================================================================
    // Operators

    // Constructor
    COpcEnumString();

    // Constructor
    COpcEnumString(UINT uCount, LPWSTR*& pStrings);

    // Destructor 
    ~COpcEnumString();

    //==========================================================================
    // IEnumConnectionPoints
       
    // Next
    STDMETHODIMP Next(
        ULONG     celt,
        LPOLESTR* rgelt,
        ULONG*    pceltFetched);

    // Skip
    STDMETHODIMP Skip(ULONG celt);

    // Reset
    STDMETHODIMP Reset();

    // Clone
    STDMETHODIMP Clone(IEnumString** ppEnum);

private:

    //==========================================================================
    // Private Members

    UINT    m_uIndex;
    UINT    m_uCount;
    LPWSTR* m_pStrings;
};

#endif // _COpcEnumStrings_H_
