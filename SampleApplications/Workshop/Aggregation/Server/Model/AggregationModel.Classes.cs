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

namespace AggregationModel
{
    #region AggregatedServerStatusState Class
    #if (!OPCUA_EXCLUDE_AggregatedServerStatusState)
    /// <summary>
    /// Stores an instance of the AggregatedServerStatusType ObjectType.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class AggregatedServerStatusState : BaseObjectState
    {
        #region Constructors
        /// <summary>
        /// Initializes the type with its default attribute values.
        /// </summary>
        public AggregatedServerStatusState(NodeState parent) : base(parent)
        {
        }
        
        /// <summary>
        /// Returns the id of the default type definition node for the instance.
        /// </summary>
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(AggregationModel.ObjectTypes.AggregatedServerStatusType, AggregationModel.Namespaces.Aggregation, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        protected override void Initialize(ISystemContext context)
        {
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <summary>
        /// Initializes the any option children defined for the instance.
        /// </summary>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString = 
           "AQAAACcAAABodHRwOi8vc29tZWNvbXBhbnkuY29tL0FnZ3JlZ2F0aW9uTW9kZWz/////JGCAAAEAAAAB" +
           "ACIAAABBZ2dyZWdhdGVkU2VydmVyU3RhdHVzVHlwZUluc3RhbmNlAQH1AAMAAAAALgAAAFJlcHJlc2Vu" +
           "dHMgdGhlIHN0YXR1cyBvZiBhbiBhZ2dyZWdhdGVkIHNlcnZlci4BAfUA/////wMAAAAVYIkKAgAAAAEA" +
           "CwAAAEVuZHBvaW50VXJsAQH2AAAuAET2AAAAAAz/////AQH/////AAAAABVgiQoCAAAAAQAGAAAAU3Rh" +
           "dHVzAQH3AAAuAET3AAAAABP/////AQH/////AAAAABVgiQoCAAAAAQALAAAAQ29ubmVjdFRpbWUBAfgA" +
           "AC4ARPgAAAAADf////8BAf////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <summary>
        /// A description for the EndpointUrl Property.
        /// </summary>
        public PropertyState<string> EndpointUrl
        {
            get
            { 
                return m_endpointUrl;  
            }
            
            set
            {
                if (!Object.ReferenceEquals(m_endpointUrl, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_endpointUrl = value;
            }
        }

        /// <summary>
        /// A description for the Status Property.
        /// </summary>
        public PropertyState<StatusCode> Status
        {
            get
            { 
                return m_status;  
            }
            
            set
            {
                if (!Object.ReferenceEquals(m_status, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_status = value;
            }
        }

        /// <summary>
        /// A description for the ConnectTime Property.
        /// </summary>
        public PropertyState<DateTime> ConnectTime
        {
            get
            { 
                return m_connectTime;  
            }
            
            set
            {
                if (!Object.ReferenceEquals(m_connectTime, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_connectTime = value;
            }
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Populates a list with the children that belong to the node.
        /// </summary>
        /// <param name="context">The context for the system being accessed.</param>
        /// <param name="children">The list of children to populate.</param>
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_endpointUrl != null)
            {
                children.Add(m_endpointUrl);
            }

            if (m_status != null)
            {
                children.Add(m_status);
            }

            if (m_connectTime != null)
            {
                children.Add(m_connectTime);
            }

            base.GetChildren(context, children);
        }

        /// <summary>
        /// Finds the child with the specified browse name.
        /// </summary>
        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case AggregationModel.BrowseNames.EndpointUrl:
                {
                    if (createOrReplace)
                    {
                        if (EndpointUrl == null)
                        {
                            if (replacement == null)
                            {
                                EndpointUrl = new PropertyState<string>(this);
                            }
                            else
                            {
                                EndpointUrl = (PropertyState<string>)replacement;
                            }
                        }
                    }

                    instance = EndpointUrl;
                    break;
                }

                case AggregationModel.BrowseNames.Status:
                {
                    if (createOrReplace)
                    {
                        if (Status == null)
                        {
                            if (replacement == null)
                            {
                                Status = new PropertyState<StatusCode>(this);
                            }
                            else
                            {
                                Status = (PropertyState<StatusCode>)replacement;
                            }
                        }
                    }

                    instance = Status;
                    break;
                }

                case AggregationModel.BrowseNames.ConnectTime:
                {
                    if (createOrReplace)
                    {
                        if (ConnectTime == null)
                        {
                            if (replacement == null)
                            {
                                ConnectTime = new PropertyState<DateTime>(this);
                            }
                            else
                            {
                                ConnectTime = (PropertyState<DateTime>)replacement;
                            }
                        }
                    }

                    instance = ConnectTime;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private PropertyState<string> m_endpointUrl;
        private PropertyState<StatusCode> m_status;
        private PropertyState<DateTime> m_connectTime;
        #endregion
    }
    #endif
    #endregion
}
