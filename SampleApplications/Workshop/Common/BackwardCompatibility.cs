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
using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Server;
using Opc.Ua.Configuration;

namespace Quickstarts
{
    public partial class ConnectServerCtrl : Opc.Ua.Client.Controls.ConnectServerCtrl
    {
    }

    public partial class DiscoverServerDlg : Opc.Ua.Client.Controls.DiscoverServerDlg
    {
    }

    public partial class EditDataValueCtrl : Opc.Ua.Client.Controls.EditDataValueCtrl
    {
    }

    public partial class EditDataValueDlg : Opc.Ua.Client.Controls.EditDataValueDlg
    {
    }

    public partial class EditValueCtrl : Opc.Ua.Client.Controls.EditValueCtrl
    {
    }

    public partial class EditValueDlg : Opc.Ua.Client.Controls.EditComplexValueDlg
    {
    }

    public partial class HistoryDataListView : Opc.Ua.Client.Controls.HistoryDataListView
    {
    }

    public partial class SelectNodeDlg : Opc.Ua.Client.Controls.SelectNodeDlg
    {
    }

    public partial class BrowseNodeCtrl : Opc.Ua.Client.Controls.BrowseNodeCtrl
    {
    }
    
    public partial class ServerForm : Opc.Ua.Server.Controls.ServerForm
    {
        /// <summary>
        /// Creates an empty form.
        /// </summary>
        public ServerForm() : base()
        {
        }

        /// <summary>
        /// Creates a form which displays the status for a UA server.
        /// </summary>
        public ServerForm(StandardServer server, ApplicationConfiguration configuration) : base(server, configuration)
        {
        }

        /// <summary>
        /// Creates a form which displays the status for a UA server.
        /// </summary>
        public ServerForm(ApplicationInstance application) : base(application)
        {
        }
    }
}
