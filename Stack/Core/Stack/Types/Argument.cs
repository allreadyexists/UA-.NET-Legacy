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
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;

namespace Opc.Ua
{
	/// <summary>
	/// The Argument class.
	/// </summary>
	public partial class Argument
    {
        #region Public Properties
        /// <summary>
        /// Initializes an instance of the argument.
        /// </summary>
        public Argument(string name, NodeId dataType, int valueRank, string description)
        {
            this.m_name = name;
            this.m_dataType = dataType;
            this.m_valueRank = valueRank;
            this.m_description = description;
        }
        #endregion

        #region Public Properties
        /// <summary>
		/// The value for the argument.
		/// </summary>
		public object Value
		{
			get { return m_value;  }
			set { m_value = value; }
		}
		#endregion

		#region Private Fields
		private object m_value;
		#endregion
	}
}
