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

#ifndef _COpcCommon_H_
#define _COpcCommon_H_

#if _MSC_VER >= 1000
#pragma once
#endif // _MSC_VER >= 1000

#include "opccomn.h"

#include "OpcDefs.h"
#include "COpcString.h"

//==============================================================================
// FUNCTION: OPCXX_MESSAGE_MODULE_NAME
// PURPOSE:  Names for modules that contain text for standard OPC messages.

#define OPC_MESSAGE_MODULE_NAME_AE    _T("opc_aeps")
#define OPC_MESSAGE_MODULE_NAME_BATCH _T("opcbc_ps")
#define OPC_MESSAGE_MODULE_NAME_DA    _T("opcproxy")
#define OPC_MESSAGE_MODULE_NAME_DX    _T("opcdxps")
#define OPC_MESSAGE_MODULE_NAME_HDA   _T("opchda_ps")
#define OPC_MESSAGE_MODULE_NAME_SEC   _T("opcsec_ps")
#define OPC_MESSAGE_MODULE_NAME_CMD   _T("opccmdps")

//==============================================================================
// CLASS:   COpcCommon
// PURPOSE: Implements the IOPCCommon interface.
// NOTES:

class OPCUTILS_API COpcCommon : public IOPCCommon
{
public:

    //==========================================================================
    // Operators

    // Constructor
    COpcCommon();

    // Destructor 
    ~COpcCommon();

	//==========================================================================
    // Public Methods
    
	// GetErrorString
    static COpcString GetErrorString(
		const COpcString& cModuleName,
        HRESULT           hResult
    );

    // GetErrorString
    static STDMETHODIMP GetErrorString( 
		LPCTSTR szModuleName,
        HRESULT dwError,
        LCID    dwLocale,
        LPWSTR* ppString
    );

	//==========================================================================
    // IOPCCommon

    // SetLocaleID
    STDMETHODIMP SetLocaleID(LCID dwLcid);

    // GetLocaleID
    STDMETHODIMP GetLocaleID(LCID *pdwLcid);

    // QueryAvailableLocaleIDs
    STDMETHODIMP QueryAvailableLocaleIDs(DWORD* pdwCount, LCID** pdwLcid);

    // GetErrorString
    STDMETHODIMP GetErrorString(HRESULT dwError, LPWSTR* ppString);

    // SetClientName
    STDMETHODIMP SetClientName(LPCWSTR szName);

protected:

    //==========================================================================
    // Protected Methods

    // GetLocaleID
    LCID GetLocaleID() const { return m_dwLcid; }

    // GetClientName
    const COpcString& GetClientName() const { return m_cClientName; }

    // GetAvailableLocaleIDs
    virtual const LCID* GetAvailableLocaleIDs() { return NULL; }
	
	// GetErrorString
    virtual STDMETHODIMP GetErrorString(HRESULT dwError, LCID dwLocale, LPWSTR* ppString) = 0;

private:

    //==========================================================================
    // Private Members

    LCID       m_dwLcid;
    COpcString m_cClientName;
};

#endif // _COpcCommon_H_
