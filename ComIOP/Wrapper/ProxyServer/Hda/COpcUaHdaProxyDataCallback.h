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

/// <summary>
/// Used to dispatch callbacks.
/// </summary>>
public ref class COpcUaHdaProxyDataCallback : IComHdaDataCallback
{
public:

	/// <summary>
	/// Creates a new callback,
	/// </summary>
	COpcUaHdaProxyDataCallback(IOPCHDA_DataCallback* ipCallback);

	/// <summary>
	/// Releases all resources used by the callback.
	/// </summary>
	~COpcUaHdaProxyDataCallback();

    /// <summary>
    /// The finializer implementation.
    /// </summary>
    !COpcUaHdaProxyDataCallback();

	virtual void OnDataChange(
		int transactionId, 
		List<HdaReadRequest^>^ results);

	virtual void OnReadComplete(
		int transactionId, 
		List<HdaReadRequest^>^ results);

	virtual void OnReadModifiedComplete(
		int transactionId, 
		List<HdaReadRequest^>^ results);

	virtual void OnReadAttributeComplete(
		int transactionId, 
		List<HdaReadRequest^>^ results);

	virtual void OnReadAnnotations(
		int transactionId, 
		List<HdaReadRequest^>^ results);

	virtual void OnInsertAnnotations(
		int transactionId, 
		List<HdaUpdateRequest^>^ results);

	virtual void OnUpdateComplete(
		int transactionId, 
		List<HdaUpdateRequest^>^ results);

	virtual void OnCancelComplete(int transactionId);

private:

	/// <summary>
	/// An unmanaged container for unmanaged data stored in the channel.
	/// </summary>
	IOPCHDA_DataCallback* m_ipCallback;

	/// <summary>
	/// A synchronization object for the object.
	/// </summary>
	Object^ m_lock;
};
