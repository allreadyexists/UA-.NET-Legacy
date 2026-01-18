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

#ifndef _COpcThreadPool_H_
#define _COpcThreadPool_H_

#if _MSC_VER >= 1000
#pragma once
#endif // _MSC_VER >= 1000

#include "OpcDefs.h"
#include "COpcList.h"
#include "COpcCriticalSection.h"

class COpcMessage;

//==============================================================================
// INTERFACE: IOpcMessageCallback
// PURPOSE:   A interface to an object that processes messages.

interface IOpcMessageCallback : public IUnknown
{
	// ProcessMessage
	virtual void ProcessMessage(COpcMessage& cMsg) = 0;
};

//==============================================================================
// CLASS:   COpcMessage
// PURPOSE: A base class for a message.

class OPCUTILS_API COpcMessage
{
    OPC_CLASS_NEW_DELETE();

public:

    //==========================================================================
    // Public Operators

    // Constructor
    COpcMessage(UINT uType, IOpcMessageCallback* ipCallback);

    // Copy Constructor
    COpcMessage(const COpcMessage& cMessage);

	// Destructor
    virtual ~COpcMessage();

    //==========================================================================
    // Public Methods

	// Process
	virtual void Process()
	{
		if (m_ipCallback != NULL)
		{
			m_ipCallback->ProcessMessage(*this);
		}
	}

	// GetID
	UINT GetID() { return m_uID; }

	// GetType
	UINT GetType() { return m_uType; }

protected:

    //==========================================================================
    // Protected Operators

	UINT                 m_uID;
	UINT                 m_uType;
	IOpcMessageCallback* m_ipCallback;
};

//==============================================================================
// CLASS:   COpcThreadPool
// PURPOSE: Manages a pool of threads that process queued messages.

class OPCUTILS_API COpcThreadPool : public COpcSynchObject
{
    OPC_CLASS_NEW_DELETE();

public:

    //==========================================================================
    // Public Operators

    // Constructor
    COpcThreadPool();

	// Destructor
    ~COpcThreadPool();

    //==========================================================================
    // Public Methods
     
	// Start
	bool Start();

	// Stop
	void Stop();

	// Run
	void Run();

    // QueueMessage
	bool QueueMessage(COpcMessage* pMsg);

	// SetSize
	void SetSize(UINT uMinThreads, UINT uMaxThreads);

private:

    //==========================================================================
    // Private Members

	HANDLE                  m_hEvent;
	COpcList<COpcMessage*>  m_cQueue;

	UINT                    m_uTotalThreads;
	UINT                    m_uWaitingThreads;
	UINT                    m_uMinThreads;
	UINT                    m_uMaxThreads;
};

#endif // _COpcThreadPool_H_
