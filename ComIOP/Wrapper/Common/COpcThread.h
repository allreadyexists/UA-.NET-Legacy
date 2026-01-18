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

#ifndef _COpcThread_H_
#define _COpcThread_H_

#if _MSC_VER >= 1000
#pragma once
#endif // _MSC_VER >= 1000

#include "OpcDefs.h"
#include "COpcString.h"

//==============================================================================
// TYPEDEF: PfnOpcThreadControl
// PURPOSE: Pointer to a function that controls a thread.

typedef void (WINAPI *FnOpcThreadControl)(void* pData, bool bStopThread);
typedef FnOpcThreadControl PfnOpcThreadControl;

//==============================================================================
// CLASS:   COpcThread
// PURPOSE: Manages startup and shutdown of a thread.

class OPCUTILS_API COpcThread
{
    OPC_CLASS_NEW_DELETE()

public:

    //==========================================================================
    // Public Operators

    // Constructor
    COpcThread();

    // Destructor
    ~COpcThread();

    //==========================================================================
    // Public Methods

    // Start
    bool Start(
        PfnOpcThreadControl pfnStartProc, 
        void*               pData, 
        DWORD               dwTimeout = INFINITE,
		int                 iPriority = THREAD_PRIORITY_NORMAL);

    // Stop
    void Stop(DWORD dwTimeout = INFINITE);

    // WaitingForStop
    bool WaitingForStop() { return m_bWaitingForStop; }

    // Run
    DWORD Run();

    // PostMessage
    bool PostMessage(UINT uMsgID, WPARAM wParam, LPARAM lParam);

private:

    //==========================================================================
    // Private Members

    DWORD               m_dwID;
    HANDLE              m_hThread;
    HANDLE              m_hEvent;
    bool                m_bWaitingForStop;

    PfnOpcThreadControl m_pfnControl;
    void*               m_pData;
};

#endif // _COpcThread_H_
