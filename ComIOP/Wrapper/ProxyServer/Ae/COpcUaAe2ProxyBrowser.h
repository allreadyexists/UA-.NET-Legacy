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

#ifndef _COpcUaAe2ProxyBrowser_H_
#define _COpcUaAe2ProxyBrowser_H_

#if _MSC_VER >= 1000
#pragma once
#endif // _MSC_VER >= 1000

using namespace Opc::Ua;
using namespace Opc::Ua::Com;
using namespace Opc::Ua::Com::Server;

class COpcUaAe2ProxyBrowser :
    public COpcComObject,
    public IOPCEventAreaBrowser,
    public COpcSynchObject
{
    OPC_CLASS_NEW_DELETE()

    OPC_BEGIN_INTERFACE_TABLE(COpcUaAe2ProxyBrowser)
        OPC_INTERFACE_ENTRY(IOPCEventAreaBrowser)
    OPC_END_INTERFACE_TABLE()

public:

	//=========================================================================
    // Operators

    // Constructor
    COpcUaAe2ProxyBrowser();
    COpcUaAe2ProxyBrowser(ComAe2Browser^ browser);

    // Destructor 
    ~COpcUaAe2ProxyBrowser();

	//=========================================================================
	// IOPCEventAreaBrowser

	STDMETHODIMP ChangeBrowsePosition( 
		/* [in] */ OPCAEBROWSEDIRECTION dwBrowseDirection,
		/* [string][in] */ LPCWSTR szString);

	STDMETHODIMP BrowseOPCAreas( 
		/* [in] */ OPCAEBROWSETYPE dwBrowseFilterType,
		/* [string][in] */ LPCWSTR szFilterCriteria,
		/* [out] */ LPENUMSTRING __RPC_FAR *ppIEnumString);

	STDMETHODIMP GetQualifiedAreaName( 
		/* [in] */ LPCWSTR szAreaName,
		/* [string][out] */ LPWSTR __RPC_FAR *pszQualifiedAreaName);

	STDMETHODIMP GetQualifiedSourceName( 
		/* [in] */ LPCWSTR szSourceName,
		/* [string][out] */ LPWSTR __RPC_FAR *pszQualifiedSourceName);

private:

	// GetInnerBrowser
	ComAe2Browser^ GetInnerBrowser();
	void* m_pInnerBrowser;
};

#endif // _COpcUaAe2ProxyBrowser_H_
