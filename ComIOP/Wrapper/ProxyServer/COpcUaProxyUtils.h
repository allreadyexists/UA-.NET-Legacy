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
using namespace System::Collections::Generic;
using namespace Opc::Ua;
using namespace Opc::Ua::Com;
using namespace Opc::Ua::Com::Server;

class COpcUaProxyUtils
{
public:
	COpcUaProxyUtils(void);
	~COpcUaProxyUtils(void);
			
	/// <summary>
	/// Outputs a trace message.
	/// </summary>
	/// <param name="source">The source of the trace.</param>
	/// <param name="context">The context to print with the source.</param>
	/// <param name="args">Any additional arguments.</param>
	static void TraceState(String^ source, String^ context, ... array<Object^>^ args);

	/// <summary>
	/// Creates an application instance certificate if one does not already exist.
	/// </summary>
	/// <param name="configuration">The configuration for the application.</param>
	static void CheckApplicationInstanceCertificate(ApplicationConfiguration^ configuration);

	/// <summary>
	/// Initializes the process and loads the application configuration.
	/// </summary>
	/// <param name="configuration">The configuration for the application.</param>
	static bool Initialize(ApplicationConfiguration^% configuration);

	/// <summary>
	/// Uninitializes the process (must be called once for every call to Initialize).
	/// </summary>
	static void Uninitialize();

	/// <summary>
	/// Frees the OPCITEMPROPERTIES structure.
	/// </summary>
	/// <param name="configuration">The structure to free.</param>
	static void FreeOPCITEMPROPERTIES(OPCITEMPROPERTIES& tItem);

	// MarshalProperties
	static void MarshalProperties(
		OPCITEMPROPERTIES& tItem, 
		array<int>^ propertyIds,
		bool returnPropertyValues,
		IList<DaProperty^>^ descriptions,
		array<DaValue^>^ values);

	// GetEnumerator
	static HRESULT GetEnumerator(
		IList<String^>^ strings, 
		REFIID          riid, 
		void**          ppUnknown);

	// MarshalVARIANT
	static bool MarshalVARIANT(VARIANT& tDst, Object^ src, HRESULT& hResult);

	// GetFILETIME
	static ::FILETIME GetFILETIME(DateTime time);

	// FixupOutputVariants
	static HRESULT FixupOutputVariants(DWORD dwCount, OPCITEMPROPERTIES* pItemProperties);

	// FixupOutputVariants
	static HRESULT FixupOutputVariants(DWORD dwCount, OPCBROWSEELEMENT* ppBrowseElements);

	// FixupDecimalArray
	static void FixupDecimalArray(VARIANT& vValue);

	// FixupOutputVariant
	static void FixupOutputVariant(VARIANT& vValue);

	// FixupOutputVariants
	static void FixupOutputVariants(DWORD dwCount, OPCITEMSTATE* pItemValues);

	// FixupOutputVariants
	static void FixupOutputVariants(DWORD dwCount, VARIANT* pItemValues);

	// FixupInputVariants
	static void FixupInputVariants(DWORD dwCount, VARIANT* pValues);

	// FixupInputVariants
	static void FixupInputVariants(DWORD dwCount, OPCITEMVQT* pValues);

    // ResolveTime
    static System::DateTime ResolveTime(OPCHDA_TIME* pTime);

    // ResolveTime
    static System::DateTime ResolveTime(::FILETIME* pTime);
};

// OpcProxy_AllocArrayToReturn
#define OpcProxy_AllocArrayToReturn(xArray, xCount, xType) \
if (xCount > 0) \
{ \
	xArray = (xType*)CoTaskMemAlloc((xCount)*sizeof(xType)); \
	\
	if (xArray == NULL) \
	{ \
		throw gcnew System::OutOfMemoryException(); \
	} \
	\
	memset(xArray, 0, (xCount)*sizeof(xType)); \
}
