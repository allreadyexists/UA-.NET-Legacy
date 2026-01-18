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

namespace Quickstarts.ComDataAccessServer
{
    /// <summary>
    /// Manages the DA COM connections used by the UA server.
    /// </summary>
    public class ComDaClientManager : ComClientManager
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ComDaClientManager"/> class.
        /// </summary>
        public ComDaClientManager()
        {
        }
        #endregion

        #region Public Members
        /// <summary>
        /// Selects the DA COM client to use for the current context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A DA COM client instance.</returns>
        public ComDaClient SelectClient(ServerSystemContext context)
        {
            // This method can be used to select different COM server instances 
            // depending on the user credentials or locales used by the the session.
            // For now all it does is return the default instance.
            return DefaultClient;
        }
        #endregion

        #region Protected Members
        /// <summary>
        /// Gets or sets the default COM client instance.
        /// </summary>
        /// <value>The default client.</value>
        protected new ComDaClient DefaultClient
        {
            get { return base.DefaultClient as ComDaClient; }
            set { base.DefaultClient = value; }
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        protected new ComDaClientConfiguration Configuration
        {
            get { return base.Configuration as ComDaClientConfiguration; }
        }

        /// <summary>
        /// Creates a new client object.
        /// </summary>
        protected override ComClient CreateClient()
        {
            return new ComDaClient(Configuration);
        }

        /// <summary>
        /// Updates the status node.
        /// </summary>
        protected override bool UpdateStatus()
        {
            // get the status from the server.
            ComDaClient client = DefaultClient;
            OPCSERVERSTATUS? status = client.GetStatus();

            // check the client has been abandoned.
            if (!Object.ReferenceEquals(client, DefaultClient))
            {
                return false;
            }

            // update the server status.
            lock (StatusNodeLock)
            {
                if (status != null)
                {
                    StatusNode.SetStatusCode(DefaultSystemContext, StatusCodes.Good, DateTime.UtcNow);

                    StatusNode.ServerState.Value = Utils.Format("{0}", status.Value.dwServerState);
                    StatusNode.CurrentTime.Value = ComUtils.GetDateTime(status.Value.ftCurrentTime);
                    StatusNode.LastUpdateTime.Value = ComUtils.GetDateTime(status.Value.ftLastUpdateTime);
                    StatusNode.StartTime.Value = ComUtils.GetDateTime(status.Value.ftStartTime);
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
