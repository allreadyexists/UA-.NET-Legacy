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

namespace Opc.Ua
{
    /// <summary>
    /// An interface to an object that describes a node local to the server.
    /// </summary>
    public interface IOperationContext
    {
        /// <summary>
        /// The identifier for the session (null if multiple sessions are associated with the operation).
        /// </summary>
        /// <value>The session identifier.</value>
        NodeId SessionId { get; }

        /// <summary>
        /// The identity of the user.
        /// </summary>
        /// <value>The user identity.</value>
        IUserIdentity UserIdentity { get; }

        /// <summary>
        /// The locales to use if available.
        /// </summary>
        /// <value>The preferred locales.</value>
        IList<string> PreferredLocales { get; }

        /// <summary>
        /// The mask to use when collecting any diagnostic information.
        /// </summary>
        /// <value>The diagnostics mask.</value>
        DiagnosticsMasks DiagnosticsMask { get; }

        /// <summary>
        /// The table of strings which is used to store diagnostic string data.
        /// </summary>
        /// <value>The string table.</value>
        StringTable StringTable { get; }

        /// <summary>
        /// When the operation times out.
        /// </summary>
        /// <value>The operation deadline.</value>
        DateTime OperationDeadline { get; }

        /// <summary>
        /// The current status of the the operation (bad if the operation has been aborted).
        /// </summary>
        /// <value>The operation status.</value>
        StatusCode OperationStatus { get; }

        /// <summary>
        /// The audit identifier associated with the operation.
        /// </summary>
        /// <value>The audit entry identifier.</value>
        string AuditEntryId { get; }
    }
}
