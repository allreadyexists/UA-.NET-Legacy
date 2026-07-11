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
using System.Diagnostics;
using System.Xml;
using System.IO;
using System.Threading;
using System.Reflection;
using Opc.Ua;
using Opc.Ua.Server;
using Opc.Ua.Com;

namespace Opc.Ua.Com.Client
{
    /// <summary>
    /// An interface to an object that parses item ids.
    /// </summary>
    public interface IItemIdParser
    {
        /// <summary>
        /// Parses the specified item id.
        /// </summary>
        /// <param name="server">The COM server that provided the item id.</param>
        /// <param name="configuration">The COM wrapper configuration.</param>
        /// <param name="itemId">The item id to parse.</param>
        /// <param name="browseName">The name of the item.</param>
        /// <returns>True if the item id could be parsed.</returns>
        bool Parse(
            ComObject server,
            ComClientConfiguration configuration,
            string itemId,
            out string browseName);
    }

    /// <summary>
    /// The default item id parser that uses the settings in the configuration.
    /// </summary>
    public class ComItemIdParser : IItemIdParser
    {
        #region IItemIdParser Members
        /// <summary>
        /// Parses the specified item id.
        /// </summary>
        /// <param name="server">The COM server that provided the item id.</param>
        /// <param name="configuration">The COM wrapper configuration.</param>
        /// <param name="itemId">The item id to parse.</param>
        /// <param name="browseName">The name of the item.</param>
        /// <returns>True if the item id could be parsed.</returns>
        public bool Parse(ComObject server, ComClientConfiguration configuration, string itemId, out string browseName)
        {
            browseName = null;

            if (configuration == null || itemId == null)
            {
                return false;
            }

            if (String.IsNullOrEmpty(configuration.SeperatorChars))
            {                
                return false;
            }

            for (int ii = 0; ii < configuration.SeperatorChars.Length; ii++)
            {
                int index = itemId.LastIndexOf(configuration.SeperatorChars[ii]);

                if (index >= 0)
                {
                    browseName = itemId.Substring(index + 1);
                    return true;
                }
            }

            return false;
        }
        #endregion
    }
}
