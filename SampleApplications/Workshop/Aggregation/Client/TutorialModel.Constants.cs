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
using System.Reflection;
using System.Xml;
using System.Runtime.Serialization;
using Opc.Ua;

namespace TutorialModel
{
    #region ObjectType Identifiers
    /// <summary>
    /// A class that declares constants for all ObjectTypes in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class ObjectTypes
    {
        /// <summary>
        /// The identifier for the AggregatedServerStatusType ObjectType.
        /// </summary>
        public const uint AggregatedServerStatusType = 245;
    }
    #endregion

    #region Variable Identifiers
    /// <summary>
    /// A class that declares constants for all Variables in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class Variables
    {
        /// <summary>
        /// The identifier for the AggregatedServerStatusType_EndpointUrl Variable.
        /// </summary>
        public const uint AggregatedServerStatusType_EndpointUrl = 246;

        /// <summary>
        /// The identifier for the AggregatedServerStatusType_Status Variable.
        /// </summary>
        public const uint AggregatedServerStatusType_Status = 247;

        /// <summary>
        /// The identifier for the AggregatedServerStatusType_ConnectTime Variable.
        /// </summary>
        public const uint AggregatedServerStatusType_ConnectTime = 248;
    }
    #endregion

    #region ObjectType Node Identifiers
    /// <summary>
    /// A class that declares constants for all ObjectTypes in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class ObjectTypeIds
    {
        /// <summary>
        /// The identifier for the AggregatedServerStatusType ObjectType.
        /// </summary>
        public static readonly ExpandedNodeId AggregatedServerStatusType = new ExpandedNodeId(TutorialModel.ObjectTypes.AggregatedServerStatusType, TutorialModel.Namespaces.Tutorial);
    }
    #endregion

    #region Variable Node Identifiers
    /// <summary>
    /// A class that declares constants for all Variables in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class VariableIds
    {
        /// <summary>
        /// The identifier for the AggregatedServerStatusType_EndpointUrl Variable.
        /// </summary>
        public static readonly ExpandedNodeId AggregatedServerStatusType_EndpointUrl = new ExpandedNodeId(TutorialModel.Variables.AggregatedServerStatusType_EndpointUrl, TutorialModel.Namespaces.Tutorial);

        /// <summary>
        /// The identifier for the AggregatedServerStatusType_Status Variable.
        /// </summary>
        public static readonly ExpandedNodeId AggregatedServerStatusType_Status = new ExpandedNodeId(TutorialModel.Variables.AggregatedServerStatusType_Status, TutorialModel.Namespaces.Tutorial);

        /// <summary>
        /// The identifier for the AggregatedServerStatusType_ConnectTime Variable.
        /// </summary>
        public static readonly ExpandedNodeId AggregatedServerStatusType_ConnectTime = new ExpandedNodeId(TutorialModel.Variables.AggregatedServerStatusType_ConnectTime, TutorialModel.Namespaces.Tutorial);
    }
    #endregion

    #region BrowseName Declarations
    /// <summary>
    /// Declares all of the BrowseNames used in the Model Design.
    /// </summary>
    public static partial class BrowseNames
    {
        /// <summary>
        /// The BrowseName for the AggregatedServerStatusType component.
        /// </summary>
        public const string AggregatedServerStatusType = "AggregatedServerStatusType";

        /// <summary>
        /// The BrowseName for the ConnectTime component.
        /// </summary>
        public const string ConnectTime = "ConnectTime";

        /// <summary>
        /// The BrowseName for the EndpointUrl component.
        /// </summary>
        public const string EndpointUrl = "EndpointUrl";

        /// <summary>
        /// The BrowseName for the Status component.
        /// </summary>
        public const string Status = "Status";
    }
    #endregion

    #region Namespace Declarations
    /// <summary>
    /// Defines constants for all namespaces referenced by the model design.
    /// </summary>
    public static partial class Namespaces
    {
        /// <summary>
        /// The URI for the OpcUa namespace (.NET code namespace is 'Opc.Ua').
        /// </summary>
        public const string OpcUa = "http://opcfoundation.org/UA/";

        /// <summary>
        /// The URI for the OpcUaXsd namespace (.NET code namespace is 'Opc.Ua').
        /// </summary>
        public const string OpcUaXsd = "http://opcfoundation.org/UA/2008/02/Types.xsd";

        /// <summary>
        /// The URI for the Tutorial namespace (.NET code namespace is 'TutorialModel').
        /// </summary>
        public const string Tutorial = "http://somecompany.com/TutorialModel";

        /// <summary>
        /// Returns a namespace table with all of the URIs defined.
        /// </summary>
        /// <remarks>
        /// This table is was used to create any relative paths in the model design.
        /// </remarks>
        public static NamespaceTable GetNamespaceTable()
        {
            FieldInfo[] fields = typeof(Namespaces).GetFields(BindingFlags.Public | BindingFlags.Static);

            NamespaceTable namespaceTable = new NamespaceTable();

            foreach (FieldInfo field in fields)
            {
                string namespaceUri = (string)field.GetValue(typeof(Namespaces));

                if (namespaceTable.GetIndex(namespaceUri) == -1)
                {
                    namespaceTable.Append(namespaceUri);
                }
            }

            return namespaceTable;
        }
    }
    #endregion
}
