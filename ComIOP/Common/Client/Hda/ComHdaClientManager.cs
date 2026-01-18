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
using System.Threading;
using Opc.Ua;
using Opc.Ua.Server;
using Opc.Ua.Com;
using OpcRcw.Da;

namespace Opc.Ua.Com.Client
{
    /// <summary>
    /// Manages the DA COM connections used by the UA server.
    /// </summary>
    public class ComHdaClientManager : ComClientManager
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ComHdaClientManager"/> class.
        /// </summary>
        public ComHdaClientManager()
        {
        }
        #endregion

        #region Public Members
        #endregion

        #region Protected Members
        /// <summary>
        /// Gets or sets the default COM client instance.
        /// </summary>
        /// <value>The default client.</value>
        protected new ComHdaClient DefaultClient
        {
            get { return base.DefaultClient as ComHdaClient; }
            set { base.DefaultClient = value; }
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        protected new ComHdaClientConfiguration Configuration
        {
            get { return base.Configuration as ComHdaClientConfiguration; }
        }

        /// <summary>
        /// Creates a new client object.
        /// </summary>
        protected override ComClient CreateClient()
        {
            return new ComHdaClient(Configuration);
        }

        /// <summary>
        /// Updates the status node.
        /// </summary>
        protected override bool UpdateStatus()
        {
            // get the status from the server.
            ComHdaClient client = DefaultClient;
            ComHdaClient.ServerStatus? status = client.GetStatus();

            // check the client has been abandoned.
            if (!Object.ReferenceEquals(client, DefaultClient))
            {
                return false;
            }

            // update the server status.
            lock (StatusNodeLock)
            {
                StatusNode.ServerUrl.Value = Configuration.ServerUrl;

                if (status != null)
                {
                    StatusNode.SetStatusCode(DefaultSystemContext, StatusCodes.Good, DateTime.UtcNow);

                    if (String.IsNullOrEmpty(status.Value.szStatusString))
                    {
                        StatusNode.ServerState.Value = Utils.Format("{0}", status.Value.wStatus);
                    }
                    else
                    {
                        StatusNode.ServerState.Value = Utils.Format("{0} '{1}'", status.Value.wStatus, status.Value.szStatusString);
                    }

                    StatusNode.CurrentTime.Value = status.Value.ftCurrentTime;
                    StatusNode.LastUpdateTime.Value = DateTime.MinValue;
                    StatusNode.StartTime.Value = status.Value.ftStartTime;
                    StatusNode.VendorInfo.Value = status.Value.szVendorInfo;
                    StatusNode.SoftwareVersion.Value = Utils.Format("{0}.{1}.{2}", status.Value.wMajorVersion, status.Value.wMinorVersion, status.Value.wBuildNumber);
                }
                else
                {
                    StatusNode.SetStatusCode(DefaultSystemContext, StatusCodes.BadOutOfService, DateTime.UtcNow);
                }

                StatusNode.ClearChangeMasks(DefaultSystemContext, true);
                return status != null;
            }
        }
        #endregion

        #region Private Fields
        #endregion
    }
}
