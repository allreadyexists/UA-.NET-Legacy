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
using Quickstarts.DataTypes.Types;

namespace Quickstarts.DataTypes.Instances
{
    #region BicycleType Class
    #if (!OPCUA_EXCLUDE_BicycleType)
    /// <summary>
    /// A description for the BicycleType DataType.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [DataContract(Namespace = Quickstarts.DataTypes.Instances.Namespaces.DataTypeInstances)]
    public partial class BicycleType : VehicleType
    {
    	#region Constructors
    	/// <summary>
    	/// The default constructor.
    	/// </summary>
    	public BicycleType()
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
    		m_noOfGears = (uint)0;
    	}
    	#endregion

    	#region Public Properties
    	/// <summary>
    	/// A description for the NoOfGears field.
    	/// </summary>
    	[DataMember(Name = "NoOfGears", Order = 1)]
    	public uint NoOfGears
    	{
    		get { return m_noOfGears;  }
    		set { m_noOfGears = value; }
    	}
    	#endregion

        #region IEncodeable Members
        /// <summary cref="IEncodeable.TypeId" />
        public override ExpandedNodeId TypeId
        {
            get { return DataTypeIds.BicycleType; }
        }

        /// <summary cref="IEncodeable.BinaryEncodingId" />
        public override ExpandedNodeId BinaryEncodingId
        {
            get { return ObjectIds.BicycleType_Encoding_DefaultBinary; }
        }
        
        /// <summary cref="IEncodeable.XmlEncodingId" />
        public override ExpandedNodeId XmlEncodingId
        {
            get { return ObjectIds.BicycleType_Encoding_DefaultXml; }
        }
        
        /// <summary cref="IEncodeable.Encode(IEncoder)" />
        public override void Encode(IEncoder encoder)
        {
            base.Encode(encoder);

            encoder.PushNamespace(Quickstarts.DataTypes.Instances.Namespaces.DataTypeInstances);

            encoder.WriteUInt32("NoOfGears", NoOfGears);

            encoder.PopNamespace();
        }
        
        /// <summary cref="IEncodeable.Decode(IDecoder)" />
        public override void Decode(IDecoder decoder)
        {
            base.Decode(decoder);

            decoder.PushNamespace(Quickstarts.DataTypes.Instances.Namespaces.DataTypeInstances);

            NoOfGears = decoder.ReadUInt32("NoOfGears");

            decoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.IsEqual(IEncodeable)" />
        public override bool IsEqual(IEncodeable encodeable)
        {
            if (Object.ReferenceEquals(this, encodeable))
            {
                return true;
            }
            
            BicycleType value = encodeable as BicycleType;
            
            if (value == null)
            {
                return false;
            }

            if (!base.IsEqual(encodeable)) return false;
            if (!Utils.IsEqual(m_noOfGears, value.m_noOfGears)) return false;

            return true;
        }
        
        /// <summary cref="ICloneable.Clone" />
        public override object Clone()
        {
            BicycleType clone = (BicycleType)base.Clone();

            clone.m_noOfGears = (uint)Utils.Clone(this.m_noOfGears);

            return clone;
        }
        #endregion
        
    	#region Private Fields
    	private uint m_noOfGears;
    	#endregion
    }

    #region BicycleTypeCollection Class
    /// <summary>
    /// A collection of BicycleType objects.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [CollectionDataContract(Name = "ListOfBicycleType", Namespace = Quickstarts.DataTypes.Instances.Namespaces.DataTypeInstances, ItemName = "BicycleType")]
    public partial class BicycleTypeCollection : List<BicycleType>, ICloneable
    {
    	#region Constructors
    	/// <summary>
    	/// Initializes the collection with default values.
    	/// </summary>
    	public BicycleTypeCollection() {}
    	
    	/// <summary>
    	/// Initializes the collection with an initial capacity.
    	/// </summary>
    	public BicycleTypeCollection(int capacity) : base(capacity) {}
    	
    	/// <summary>
    	/// Initializes the collection with another collection.
    	/// </summary>
    	public BicycleTypeCollection(IEnumerable<BicycleType> collection) : base(collection) {}
    	#endregion
                    
        #region Static Operators
        /// <summary>
        /// Converts an array to a collection.
        /// </summary>
        public static implicit operator BicycleTypeCollection(BicycleType[] values)
        {
            if (values != null)
            {
                return new BicycleTypeCollection(values);
            }

            return new BicycleTypeCollection();
        }
        
        /// <summary>
        /// Converts a collection to an array.
        /// </summary>
        public static explicit operator BicycleType[](BicycleTypeCollection values)
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
            BicycleTypeCollection clone = new BicycleTypeCollection(this.Count);

            for (int ii = 0; ii < this.Count; ii++)
            {
                clone.Add((BicycleType)Utils.Clone(this[ii]));
            }

            return clone;
        }
        #endregion
    }
    #endregion
    #endif
    #endregion
}
