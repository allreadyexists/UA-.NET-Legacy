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

#ifndef _COpcUaHdaProxyBrowser_H_
#define _COpcUaHdaProxyBrowser_H_

#if _MSC_VER >= 1000
#pragma once
#endif // _MSC_VER >= 1000

#include "COpcUaHdaProxyBrowser.h"

using namespace Opc::Ua;
using namespace Opc::Ua::Com;
using namespace Opc::Ua::Com::Server;

class COpcUaHdaProxyBrowser :
    public COpcComObject,
    public IOPCHDA_Browser,
    public COpcSynchObject
{
    OPC_CLASS_NEW_DELETE()

    OPC_BEGIN_INTERFACE_TABLE(COpcUaHdaProxyBrowser)
        OPC_INTERFACE_ENTRY(IOPCHDA_Browser)
    OPC_END_INTERFACE_TABLE()

public:

	//=========================================================================
    // Operators

    // Constructor
    COpcUaHdaProxyBrowser();
    COpcUaHdaProxyBrowser(ComHdaBrowser^ browser);

    // Destructor 
    ~COpcUaHdaProxyBrowser();

    //=========================================================================
    // IOPCHDA_Browser

	STDMETHODIMP GetEnum(
		OPCHDA_BROWSETYPE dwBrowseType,
		LPENUMSTRING*     ppIEnumString
	);

	STDMETHODIMP ChangeBrowsePosition(
		OPCHDA_BROWSEDIRECTION dwBrowseDirection,
		LPCWSTR                szString
	);

	STDMETHODIMP GetItemID(
		LPCWSTR szNode,
		LPWSTR* pszItemID
	);


	STDMETHODIMP GetBranchPosition(
		LPWSTR* pszBranchPos
	);

private:

	// GetInnerBrowser
	ComHdaBrowser^ GetInnerBrowser();

	void* m_pInnerBrowser;

};

#endif // _COpcUaHdaProxyBrowser_H_
