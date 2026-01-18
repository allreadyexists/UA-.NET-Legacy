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

#ifndef _COpcEnumStringWrapper_H_
#define _COpcEnumStringWrapper_H_

#if _MSC_VER >= 1000
#pragma once
#endif // _MSC_VER >= 1000

//============================================================================
// CLASS:   COpcEnumStringWrapper
// PURPOSE: A class to implement the IEnumString interface.
// NOTES:

class COpcEnumStringWrapper :
    public COpcComObject,
    public IEnumString,
	public COpcSynchObject
{     
    OPC_CLASS_NEW_DELETE()

    OPC_BEGIN_INTERFACE_TABLE(COpcEnumStringWrapper)
        OPC_INTERFACE_ENTRY(IEnumString)
    OPC_END_INTERFACE_TABLE()

public:

    //========================================================================
    // Operators

    // Constructor
    COpcEnumStringWrapper();

    // Constructor
    COpcEnumStringWrapper(IUnknown* ipUnknown);

    // Destructor 
    ~COpcEnumStringWrapper();

    //========================================================================
    // IEnumString

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

    //=========================================================================
    // Private Members

	IUnknown* m_ipUnknown;
};

#endif // _COpcEnumStringWrapper_H_
