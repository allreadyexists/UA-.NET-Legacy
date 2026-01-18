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
using System.Runtime.InteropServices;

namespace CubicOrange.Windows.Forms.ActiveDirectory
{
    internal class PInvoke
	{
		/// <summary>
		/// The GlobalLock function locks a global memory object and returns a pointer to the first byte of the object's memory block.
		/// GlobalLock function increments the lock count by one.
		/// Needed for the clipboard functions when getting the data from IDataObject
		/// </summary>
		/// <param name="hMem"></param>
		/// <returns></returns>
		[DllImport("Kernel32.dll")]
		public static extern IntPtr GlobalLock(IntPtr hMem);

		/// <summary>
		/// The GlobalUnlock function decrements the lock count associated with a memory object.
		/// </summary>
		/// <param name="hMem"></param>
		/// <returns></returns>
		[DllImport("Kernel32.dll")]
		public static extern bool GlobalUnlock(IntPtr hMem);
	}
}
