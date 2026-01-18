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

namespace CubicOrange.Windows.Forms.ActiveDirectory
{
	/// <summary>
	/// Details of a directory object selected in the DirectoryObjectPickerDialog.
	/// </summary>
	public class DirectoryObject
	{
        private string adsPath;
        private string className;
		private string name;
		private string upn;

        public DirectoryObject(string name, string path, string schemaClass, string upn)
        {
            this.name = name;
            this.adsPath = path;
            this.className = schemaClass;
            this.upn = upn;
        }

        /// <summary>
        /// Gets the Active Directory path for this directory object.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The format of this string depends on the options specified in the DirectoryObjectPickerDialog
        /// from which this object was selected.
        /// </para>
        /// </remarks>
        public string Path
        {
            get
            {
                return adsPath;
            }
        }

        /// <summary>
        /// Gets the name of the schema class for this directory object (objectClass attribute).
        /// </summary>
		public string SchemaClassName
		{
			get
			{
				return className;
			}
		}

        /// <summary>
        /// Gets the directory object's relative distinguished name (RDN).
        /// </summary>
		public string Name
		{
			get
			{
				return name;
			}
		}

        /// <summary>
        /// Gets the objects user principal name (userPrincipalName attribute).
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the object does not have a userPrincipalName value, this property is an empty string. 
        /// </para>
        /// </remarks>
		public string Upn
		{
			get
			{
				return upn;
			}
		}
	}
}
