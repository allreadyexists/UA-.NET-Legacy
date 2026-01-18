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

#ifndef _COpcUaDaProxyEnumItem_H_
#define _COpcUaDaProxyEnumItem_H_

#if _MSC_VER >= 1000
#pragma once
#endif // _MSC_VER >= 1000

//============================================================================
// CLASS:   COpcUaDaProxyEnumItem
// PURPOSE: A class to implement the IEnumString interface.
// NOTES:

class COpcUaDaProxyEnumItem :
    public COpcComObject,
    public IEnumOPCItemAttributes,
	public COpcSynchObject
{     
    OPC_CLASS_NEW_DELETE()

    OPC_BEGIN_INTERFACE_TABLE(COpcUaDaProxyEnumItem)
        OPC_INTERFACE_ENTRY(IEnumOPCItemAttributes)
    OPC_END_INTERFACE_TABLE()

public:

    //========================================================================
    // Operators

    // Constructor
    COpcUaDaProxyEnumItem();

    // Constructor
    COpcUaDaProxyEnumItem(UINT uCount, OPCITEMATTRIBUTES* pAttibutes);

    // Destructor 
    ~COpcUaDaProxyEnumItem();

    //========================================================================
    // IEnumOPCItemAttributes

    // Next
	STDMETHODIMP Next( 
		ULONG               celt,
		OPCITEMATTRIBUTES** ppItemArray,
		ULONG*              pceltFetched 
	);

    // Skip
	STDMETHODIMP Skip(ULONG celt);

    // Reset
	STDMETHODIMP Reset();

    // Clone
	STDMETHODIMP Clone(IEnumOPCItemAttributes** ppEnumGroupAttributes);

private:

	//=========================================================================
    // Private Methods

	// Init
	void Init(OPCITEMATTRIBUTES& cAttributes);

	// Clear
	void Clear(OPCITEMATTRIBUTES& cAttributes);

	// Copy
	void Copy(OPCITEMATTRIBUTES& cDst, OPCITEMATTRIBUTES& cSrc);

    //=========================================================================
    // Private Members

    UINT			   m_uIndex;
    UINT			   m_uCount;
    OPCITEMATTRIBUTES* m_pItems;
};

#endif // _COpcUaDaProxyEnumItem_H_
