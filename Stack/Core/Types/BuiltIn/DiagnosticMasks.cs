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

using System;
using System.Collections.Generic;
using System.Text;

namespace Opc.Ua
{
	/// <summary>
	/// The DiagnosticsMasks enumeration.
	/// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1717:OnlyFlagsEnumsShouldHavePluralNames"), Flags]
	public enum DiagnosticsMasks
    {
        /// <summary>
        /// ServiceSymbolicId = 0,
        /// </summary>
        None = 0,

		/// <summary>
		/// ServiceSymbolicId = 1,
		/// </summary>
		ServiceSymbolicId = 1,

		/// <summary>
		/// ServiceLocalizedText = 2,
		/// </summary>
		ServiceLocalizedText = 2,

		/// <summary>
		/// ServiceAdditionalInfo = 4,
		/// </summary>
		ServiceAdditionalInfo = 4,

		/// <summary>
		/// ServiceInnerStatusCode = 8,
		/// </summary>
		ServiceInnerStatusCode = 8,

		/// <summary>
		/// ServiceInnerDiagnostics = 16,
		/// </summary>
		ServiceInnerDiagnostics = 16,

		/// <summary>
		/// ServiceSymbolicIdAndText = 3,
		/// </summary>
		ServiceSymbolicIdAndText = 3,

		/// <summary>
		/// ServiceNoInnerStatus = 15,
		/// </summary>
		ServiceNoInnerStatus = 15,

		/// <summary>
		/// ServiceAll = 31,
		/// </summary>
		ServiceAll = 31,

		/// <summary>
		/// OperationSymbolicId = 32,
		/// </summary>
		OperationSymbolicId = 32,

		/// <summary>
		/// OperationLocalizedText = 64,
		/// </summary>
		OperationLocalizedText = 64,

		/// <summary>
		/// OperationAdditionalInfo = 128,
		/// </summary>
		OperationAdditionalInfo = 128,

		/// <summary>
		/// OperationInnerStatusCode = 256,
		/// </summary>
		OperationInnerStatusCode = 256,

		/// <summary>
		/// OperationInnerDiagnostics = 512,
		/// </summary>
		OperationInnerDiagnostics = 512,

		/// <summary>
		/// OperationSymbolicIdAndText = 96,
		/// </summary>
		OperationSymbolicIdAndText = 96,

		/// <summary>
		/// OperationNoInnerStatus = 224,
		/// </summary>
		OperationNoInnerStatus = 224,

		/// <summary>
		/// OperationAll = 992,
		/// </summary>
		OperationAll = 992,

		/// <summary>
		/// SymbolicId = 33,
		/// </summary>
		SymbolicId = 33,

		/// <summary>
		/// LocalizedText = 66,
		/// </summary>
		LocalizedText = 66,

		/// <summary>
		/// AdditionalInfo = 132,
		/// </summary>
		AdditionalInfo = 132,

		/// <summary>
		/// InnerStatusCode = 264,
		/// </summary>
		InnerStatusCode = 264,

		/// <summary>
		/// InnerDiagnostics = 528,
		/// </summary>
		InnerDiagnostics = 528,

		/// <summary>
		/// SymbolicIdAndText = 99,
		/// </summary>
		SymbolicIdAndText = 99,

		/// <summary>
		/// NoInnerStatus = 239,
		/// </summary>
		NoInnerStatus = 239,

		/// <summary>
		/// All = 1023
		/// </summary>
		All = 1023
	}
}
