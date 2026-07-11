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

namespace Quickstarts.SimpleEvents
{
    #region CycleStepDataType Class
    #if (!OPCUA_EXCLUDE_CycleStepDataType)
    /// <summary>
    /// A description for the CycleStepDataType DataType.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [DataContract(Namespace = Quickstarts.SimpleEvents.Namespaces.SimpleEvents)]
    public partial class CycleStepDataType : IEncodeable
    {
    	#region Constructors
    	/// <summary>
    	/// The default constructor.
    	/// </summary>
    	public CycleStepDataType()
    	{
    		Initialize();
    	}
        
    	/// <summary>
    	/// Called by the .NET framework during deserialization.
    	/// </summary>
        [OnDeserializing]
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
    		m_duration = (double)0;
    	}
    	#endregion

    	#region Public Properties
    	/// <summary>
    	/// A description for the Name field.
    	/// </summary>
    	[DataMember(Name = "Name", IsRequired = false, Order = 1)]
    	public string Name
    	{
    		get { return m_name;  }
    		set { m_name = value; }
    	}

    	/// <summary>
    	/// A description for the Duration field.
    	/// </summary>
    	[DataMember(Name = "Duration", IsRequired = false, Order = 2)]
    	public double Duration
    	{
    		get { return m_duration;  }
    		set { m_duration = value; }
    	}
    	#endregion

        #region IEncodeable Members
        /// <summary cref="IEncodeable.TypeId" />
        public virtual ExpandedNodeId TypeId
        {
            get { return DataTypeIds.CycleStepDataType; }
        }

        /// <summary cref="IEncodeable.BinaryEncodingId" />
        public virtual ExpandedNodeId BinaryEncodingId
        {
            get { return ObjectIds.CycleStepDataType_Encoding_DefaultBinary; }
        }
        
        /// <summary cref="IEncodeable.XmlEncodingId" />
        public virtual ExpandedNodeId XmlEncodingId
        {
            get { return ObjectIds.CycleStepDataType_Encoding_DefaultXml; }
        }
        
        /// <summary cref="IEncodeable.Encode(IEncoder)" />
        public virtual void Encode(IEncoder encoder)
        {
            encoder.PushNamespace(Quickstarts.SimpleEvents.Namespaces.SimpleEvents);

            encoder.WriteString("Name", Name);
            encoder.WriteDouble("Duration", Duration);

            encoder.PopNamespace();
        }
        
        /// <summary cref="IEncodeable.Decode(IDecoder)" />
        public virtual void Decode(IDecoder decoder)
        {
            decoder.PushNamespace(Quickstarts.SimpleEvents.Namespaces.SimpleEvents);

            Name = decoder.ReadString("Name");
            Duration = decoder.ReadDouble("Duration");

            decoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.IsEqual(IEncodeable)" />
        public virtual bool IsEqual(IEncodeable encodeable)
        {
            if (Object.ReferenceEquals(this, encodeable))
            {
                return true;
            }
            
            CycleStepDataType value = encodeable as CycleStepDataType;
            
            if (value == null)
            {
                return false;
            }

            if (!Utils.IsEqual(m_name, value.m_name)) return false;
            if (!Utils.IsEqual(m_duration, value.m_duration)) return false;

            return true;
        }
        
        /// <summary cref="ICloneable.Clone" />
        public virtual object Clone()
        {
            CycleStepDataType clone = (CycleStepDataType)this.MemberwiseClone();

            clone.m_name = (string)Utils.Clone(this.m_name);
            clone.m_duration = (double)Utils.Clone(this.m_duration);

            return clone;
        }
        #endregion
        
    	#region Private Fields
    	private string m_name;
    	private double m_duration;
    	#endregion
    }

    #region CycleStepDataTypeCollection Class
    /// <summary>
    /// A collection of CycleStepDataType objects.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [CollectionDataContract(Name = "ListOfCycleStepDataType", Namespace = Quickstarts.SimpleEvents.Namespaces.SimpleEvents, ItemName = "CycleStepDataType")]
    public partial class CycleStepDataTypeCollection : List<CycleStepDataType>, ICloneable
    {
    	#region Constructors
    	/// <summary>
    	/// Initializes the collection with default values.
    	/// </summary>
    	public CycleStepDataTypeCollection() {}
    	
    	/// <summary>
    	/// Initializes the collection with an initial capacity.
    	/// </summary>
    	public CycleStepDataTypeCollection(int capacity) : base(capacity) {}
    	
    	/// <summary>
    	/// Initializes the collection with another collection.
    	/// </summary>
    	public CycleStepDataTypeCollection(IEnumerable<CycleStepDataType> collection) : base(collection) {}
    	#endregion
                    
        #region Static Operators
        /// <summary>
        /// Converts an array to a collection.
        /// </summary>
        public static implicit operator CycleStepDataTypeCollection(CycleStepDataType[] values)
        {
            if (values != null)
            {
                return new CycleStepDataTypeCollection(values);
            }

            return new CycleStepDataTypeCollection();
        }
        
        /// <summary>
        /// Converts a collection to an array.
        /// </summary>
        public static explicit operator CycleStepDataType[](CycleStepDataTypeCollection values)
        {
            if (values != null)
            {
                return values.ToArray();
            }

            return null;
        }
        #endregion

        #region ICloneable Methods
        /// <summary>
        /// Creates a deep copy of the collection.
        /// </summary>
        public object Clone()
        {
            CycleStepDataTypeCollection clone = new CycleStepDataTypeCollection(this.Count);

            for (int ii = 0; ii < this.Count; ii++)
            {
                clone.Add((CycleStepDataType)Utils.Clone(this[ii]));
            }

            return clone;
        }
        #endregion
    }
    #endregion
    #endif
    #endregion
}
