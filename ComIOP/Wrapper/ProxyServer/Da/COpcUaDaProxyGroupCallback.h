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

#include "COpcUaDaProxyGroup.h"

using namespace System;
using namespace System::Collections::Generic;
using namespace Opc::Ua;
using namespace Opc::Ua::Com;

/// <summary>
/// Used to dispatch callbacks.
/// </summary>>
public ref class COpcUaDaProxyGroupCallback : IComDaGroupCallback
{
public:

	/// <summary>
	/// Creates a new callback,
	/// </summary>
	COpcUaDaProxyGroupCallback(IOPCDataCallback* ipCallback);

	/// <summary>
	/// Releases all resources used by the callback.
	/// </summary>
	~COpcUaDaProxyGroupCallback();

    /// <summary>
    /// The finializer implementation.
    /// </summary>
    !COpcUaDaProxyGroupCallback();

	// ReadCompleted
	virtual void ReadCompleted(
		int groupHandle,
		bool isRefresh,
		int cancelId,
		int transactionId,
		array<int>^ clientHandles,
		array<DaValue^>^ values);

	// WriteCompleted
	virtual void WriteCompleted(
		int groupHandle,
		int transactionId,
		array<int>^ clientHandles,
		array<int>^ errors);

	// CancelSucceeded
	virtual void CancelSucceeded(
		int groupHandle,
		int transactionId);

private:

	/// <summary>
	/// An unmanaged container for unmanaged data stored in the channel.
	/// </summary>
	IOPCDataCallback* m_ipCallback;

	/// <summary>
	/// A synchronization object for the object.
	/// </summary>
	Object^ m_lock;
};
