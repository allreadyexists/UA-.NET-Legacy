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
using System.Reflection;
using System.Xml;

namespace Opc.Ua
{
	/// <summary>
	/// A class that defines constants used by UA applications.
	/// </summary>
	public static partial class StatusCodes
	{
        #region Static Helper Functions
        /// <summary>
		/// Returns the browse name for the attribute.
		/// </summary>
        public static string GetBrowseName(uint identifier)
		{
			FieldInfo[] fields = typeof(StatusCodes).GetFields(BindingFlags.Public | BindingFlags.Static);

			foreach (FieldInfo field in fields)
			{
                if (identifier == (uint)field.GetValue(typeof(StatusCodes)))
				{
					return field.Name;
				}
			}

			return System.String.Empty;
		}

		/// <summary>
		/// Returns the browse names for all attributes.
		/// </summary>
		public static string[] GetBrowseNames()
		{
			FieldInfo[] fields = typeof(StatusCodes).GetFields(BindingFlags.Public | BindingFlags.Static);
            
            int ii = 0;

            string[] names = new string[fields.Length];
            
			foreach (FieldInfo field in fields)
			{
				names[ii++] = field.Name;
			}

			return names;
		}

		/// <summary>
		/// Returns the id for the attribute with the specified browse name.
		/// </summary>
        public static uint GetIdentifier(string browseName)
		{
			FieldInfo[] fields = typeof(StatusCodes).GetFields(BindingFlags.Public | BindingFlags.Static);

			foreach (FieldInfo field in fields)
			{
				if (field.Name == browseName)
				{
                    return (uint)field.GetValue(typeof(StatusCodes));
				}
			}

			return 0;
        }
        #endregion
    }
}
