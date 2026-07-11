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

namespace Quickstarts { namespace ComDataAccessClient {

class ComDaClientUtils
{
public:
			
	// Creates an instance of a COM server.
	static IUnknown* CreateInstance(REFCLSID tClsid, REFIID tInterface);

	// Returns the ProgIDs of COM proxies for UA servers on the local machine. 
	static List<String^>^ EnumerateComServerOnLocalhost();

	// Returns the UA servers on the specified host.
	static List<ApplicationDescription^>^ EnumerateUaServersOnHost(String^ hostname);

	// Creates a COM server for the specified url  and returns the ProgID.
	static String^ CreateComServerForApplication(String^ discoveryUrl, bool useSecurity);
};

}}
