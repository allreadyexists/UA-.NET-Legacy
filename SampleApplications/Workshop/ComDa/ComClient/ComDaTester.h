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

#include "ComDaDataCallback.h"

using namespace System;
using namespace System::Collections::Generic;
using namespace System::Threading;
using namespace Opc::Ua;

namespace Quickstarts { namespace ComDataAccessClient {

ref class MainForm;

ref class ComDaTester
{
public:
		// Initializes the object.
		ComDaTester(void);
	
		// gets or sets the update rate.
        property int UpdateRate
        {
            int get() { return m_updateRate; }
            void set(int value) { m_updateRate = value; }
        }
	
		// gets or sets the item count.
        property int ItemCount
        {
            int get() { return m_itemCount; }
            void set(int value) { m_itemCount = value; }
        }

		// returns the number of callbacks that have arrived.
        property int CallbackCount
        {
            int get() { return m_callbackCount; }
        }

		// returns the total number of item updates that have arrived.
        property int TotalItemUpdateCount
        {
            int get() { return m_totalItemUpdateCount; }
        }

		// returns the time of the first callback.
        property DateTime FirstCallbackTime
        {
            DateTime get() { return m_firstCallbackTime; }
        }

		// returns the time of the last callback.
        property DateTime LastCallbackTime
        {
            DateTime get() { return m_lastCallbackTime; }
        }

        // Gets the statistic collected since the last update.
        void GetStatistics(
            int% callbackCount,
            int% totalItemUpdateCount,
            DateTime% firstCallbackTime,
            DateTime% lastCallbackTime,
            int% minItemUpdateCount,
            int% maxItemUpdateCount);

		// starts the connectivity against the server an reports the results to the form.
        void StartConnectivityTest(MainForm^ form, String^ progId);

		// starts the performance test against the server an reports the results to the form.
        void StartPerformanceTest(MainForm^ form, String^ progId);

        // stops the test.
        void StopTest()
        {
            try
            {
                Monitor::Enter(m_lock);

                if (m_event != nullptr)
                {
                    m_event->Set();
                }
            }
            finally
            {
                Monitor::Exit(m_lock);
            }
        }

		// reports a message for the test.
		void ReportMessage(String^ message, ... array<Object^>^ args);

		// returns the messages that have been reported since the last time GetMessages() was called.
		array<String^>^ GetMessages();

		// handles a data change notification from the server.
		void OnDataChange( 
			DWORD dwCount, 
			OPCHANDLE* phClientItems, 
			VARIANT* pvValues, 
			HRESULT* pErrors);

private:
	
        Object^ m_lock;
        List<String^>^ m_messages;
        int m_updateRate;
        int m_itemCount;
        int m_callbackCount;
		int m_totalItemUpdateCount;
        DateTime m_firstCallbackTime;
        DateTime m_lastCallbackTime;
        array<int>^ m_itemUpdateCounts;
		ComDaDataCallback* m_ipCallback;
        ManualResetEvent^ m_event;
};

}}
