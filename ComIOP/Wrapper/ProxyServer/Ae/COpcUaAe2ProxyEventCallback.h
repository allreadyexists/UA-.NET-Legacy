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

#include "COpcUaAe2ProxySubscription.h"

using namespace System;
using namespace System::Collections::Generic;
using namespace Opc::Ua;
using namespace Opc::Ua::Com;

/// <summary>
/// Used to dispatch callbacks.
/// </summary>>
public ref class COpcUaAe2ProxyEventCallback : IComAeEventCallback
{
public:

	/// <summary>
	/// Creates a new callback,
	/// </summary>
	COpcUaAe2ProxyEventCallback(IOPCEventSink* ipCallback);

	/// <summary>
	/// Releases all resources used by the callback.
	/// </summary>
	~COpcUaAe2ProxyEventCallback();

    /// <summary>
    /// The finializer implementation.
    /// </summary>
    !COpcUaAe2ProxyEventCallback();

    /// <summary>
    /// The on event callback.
    /// </summary>
    virtual void OnEvent(
        unsigned int hClientSubscription,
        bool bRefresh,
        bool bLastRefresh,
		array<OpcRcw::Ae::ONEVENTSTRUCT>^ pEvents);

private:

	/// <summary>
	/// An unmanaged container for unmanaged data stored in the channel.
	/// </summary>
	IOPCEventSink* m_ipCallback;

	/// <summary>
	/// A synchronization object for the object.
	/// </summary>
	Object^ m_lock;
};
