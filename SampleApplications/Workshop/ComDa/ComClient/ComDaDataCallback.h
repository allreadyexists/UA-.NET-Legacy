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

#pragma once

using namespace System;
using namespace System::Runtime::InteropServices;

namespace Quickstarts { namespace ComDataAccessClient {

ref class ComDaTester;

class ComDaDataCallback : public IOPCDataCallback
{
public:

	// Initializes the object.
	ComDaDataCallback(ComDaTester^ tester);

	// Cleans up.
	~ComDaDataCallback();

	//==========================================================================
    // IUnknown

	// QueryInterface
	STDMETHODIMP QueryInterface(REFIID iid, LPVOID* ppInterface);

	// AddRef
	STDMETHODIMP_(ULONG) AddRef();

	// Release
	STDMETHODIMP_(ULONG) Release();

	//==========================================================================
    // IOPCDataCallback

    // OnDataChange
    STDMETHODIMP OnDataChange(
        DWORD       dwTransid, 
        OPCHANDLE   hGroup, 
        HRESULT     hrMasterquality,
        HRESULT     hrMastererror,
        DWORD       dwCount, 
        OPCHANDLE * phClientItems, 
        VARIANT   * pvValues, 
        WORD      * pwQualities,
		::FILETIME  * pftTimeStamps,
        HRESULT   * pErrors
    );

    // OnReadComplete
    STDMETHODIMP OnReadComplete(
        DWORD       dwTransid, 
        OPCHANDLE   hGroup, 
        HRESULT     hrMasterquality,
        HRESULT     hrMastererror,
        DWORD       dwCount, 
        OPCHANDLE * phClientItems, 
        VARIANT   * pvValues, 
        WORD      * pwQualities,
        ::FILETIME  * pftTimeStamps,
        HRESULT   * pErrors
    );

    // OnWriteComplete
    STDMETHODIMP OnWriteComplete(
        DWORD       dwTransid, 
        OPCHANDLE   hGroup, 
        HRESULT     hrMastererr, 
        DWORD       dwCount, 
        OPCHANDLE * pClienthandles, 
        HRESULT   * pErrors
    );

    // OnCancelComplete
    STDMETHODIMP OnCancelComplete(
        DWORD       dwTransid, 
        OPCHANDLE   hGroup);

private:

	ULONG m_ulRefs;
	GCHandle m_hTester;
};

}}
