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
using System.Runtime.Serialization;
using Opc.Ua;

namespace Quickstarts.AlarmConditionServer
{
    /// <summary>
    /// Stores the configuration the Alarm Condition server.
    /// </summary>
    [DataContract(Namespace = Namespaces.AlarmCondition)]
    public class AlarmConditionServerConfiguration
    {
        #region Constructors
        /// <summary>
        /// The default constructor.
        /// </summary>
        public AlarmConditionServerConfiguration()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes the object during deserialization.
        /// </summary>
        [OnDeserializing()]
        private void Initialize(StreamingContext context)
        {
            Initialize();
        }

        /// <summary>
        /// Sets private members to default values.
        /// </summary>
        private void Initialize()
        {
            m_areas = new AreaConfigurationCollection();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the list of top level Areas exposed by the server.
        /// </summary>
        [DataMember(Order = 1)]
        public AreaConfigurationCollection Areas
        {
            get { return m_areas; }
            set { m_areas = value; }
        }
        #endregion

        #region Private Members
        private AreaConfigurationCollection m_areas;
        #endregion
    }

    /// <summary>
    /// Stores the configuration for a Area within the Alarm Condition server.
    /// </summary>
    [DataContract(Namespace = Namespaces.AlarmCondition)]
    public class AreaConfiguration
    {
        #region Constructors
        /// <summary>
        /// The default constructor.
        /// </summary>
        public AreaConfiguration()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes the object during deserialization.
        /// </summary>
        [OnDeserializing()]
        private void Initialize(StreamingContext context)
        {
            Initialize();
        }

        /// <summary>
        /// Sets private members to default values.
        /// </summary>
        private void Initialize()
        {
            m_name = null;
            m_subAreas = null;
            m_sourcePaths = null;
        }
        #endregion
        
        #region Public Properties
        /// <summary>
        /// The browse name for the instance.
        /// </summary>
        [DataMember(Order = 1)]
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        /// <summary>
        /// Gets or set the list of sub-areas.
        /// </summary>
        [DataMember(Order = 2)]
        public AreaConfigurationCollection SubAreas
        {
            get { return m_subAreas; }
            set { m_subAreas = value; }
        }

        /// <summary>
        /// Gets or set the list of sources.
        /// </summary>
        [DataMember(Order = 3)]
        public StringCollection SourcePaths
        {
            get { return m_sourcePaths; }
            set { m_sourcePaths = value; }
        }
        #endregion

        #region Private Members
        private string m_name;
        private AreaConfigurationCollection m_subAreas;
        private StringCollection m_sourcePaths;
        #endregion
    }
    
    #region AreaConfigurationCollection Class
    /// <summary>
    /// A collection of AreaConfiguration objects.
    /// </summary>
    [CollectionDataContract(Name = "ListOfAreaConfiguration", Namespace = Namespaces.AlarmCondition, ItemName = "AreaConfiguration")]
    public partial class AreaConfigurationCollection : List<AreaConfiguration>
    {
    }
    #endregion
}
