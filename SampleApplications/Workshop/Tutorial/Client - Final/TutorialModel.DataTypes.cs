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
    #region CalibrationDataType Class
    #if (!OPCUA_EXCLUDE_CalibrationDataType)
    /// <summary>
    /// A description for the CalibrationDataType DataType.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [DataContract(Namespace = TutorialModel.Namespaces.Tutorial)]
    public partial class CalibrationDataType : EncodeableObject
    {
    	#region Constructors
    	/// <summary>
    	/// The default constructor.
    	/// </summary>
    	public CalibrationDataType()
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
    		m_offset = (double)0;
    		m_period = (double)0;
    	}
    	#endregion

    	#region Public Properties
    	/// <summary>
    	/// A description for the Offset field.
    	/// </summary>
    	[DataMember(Name = "Offset", Order = 1)]
    	public double Offset
    	{
    		get { return m_offset;  }
    		set { m_offset = value; }
    	}

    	/// <summary>
    	/// A description for the Period field.
    	/// </summary>
    	[DataMember(Name = "Period", Order = 2)]
    	public double Period
    	{
    		get { return m_period;  }
    		set { m_period = value; }
    	}
    	#endregion

        #region IEncodeable Members
        /// <summary cref="IEncodeable.TypeId" />
        public override ExpandedNodeId TypeId
        {
            get { return DataTypeIds.CalibrationDataType; }
        }

        /// <summary cref="IEncodeable.BinaryEncodingId" />
        public override ExpandedNodeId BinaryEncodingId
        {
            get { return ObjectIds.CalibrationDataType_Encoding_DefaultBinary; }
        }
        
        /// <summary cref="IEncodeable.XmlEncodingId" />
        public override ExpandedNodeId XmlEncodingId
        {
            get { return ObjectIds.CalibrationDataType_Encoding_DefaultXml; }
        }
        
        /// <summary cref="IEncodeable.Encode(IEncoder)" />
        public override void Encode(IEncoder encoder)
        {
            base.Encode(encoder);

            encoder.PushNamespace(TutorialModel.Namespaces.Tutorial);

            encoder.WriteDouble("Offset", Offset);
            encoder.WriteDouble("Period", Period);

            encoder.PopNamespace();
        }
        
        /// <summary cref="IEncodeable.Decode(IDecoder)" />
        public override void Decode(IDecoder decoder)
        {
            base.Decode(decoder);

            decoder.PushNamespace(TutorialModel.Namespaces.Tutorial);

            Offset = decoder.ReadDouble("Offset");
            Period = decoder.ReadDouble("Period");

            decoder.PopNamespace();
        }

        /// <summary cref="IEncodeable.IsEqual(IEncodeable)" />
        public override bool IsEqual(IEncodeable encodeable)
        {
            if (Object.ReferenceEquals(this, encodeable))
            {
                return true;
            }
            
            CalibrationDataType value = encodeable as CalibrationDataType;
            
            if (value == null)
            {
                return false;
            }

            if (!Utils.IsEqual(m_offset, value.m_offset)) return false;
            if (!Utils.IsEqual(m_period, value.m_period)) return false;

            return true;
        }
        
        /// <summary cref="ICloneable.Clone" />
        public override object Clone()
        {
            CalibrationDataType clone = (CalibrationDataType)base.Clone();

            clone.m_offset = (double)Utils.Clone(this.m_offset);
            clone.m_period = (double)Utils.Clone(this.m_period);

            return clone;
        }
        #endregion
        
    	#region Private Fields
    	private double m_offset;
    	private double m_period;
    	#endregion
    }

    #region CalibrationDataTypeCollection Class
    /// <summary>
    /// A collection of CalibrationDataType objects.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    [CollectionDataContract(Name = "ListOfCalibrationDataType", Namespace = TutorialModel.Namespaces.Tutorial, ItemName = "CalibrationDataType")]
    public partial class CalibrationDataTypeCollection : List<CalibrationDataType>, ICloneable
    {
    	#region Constructors
    	/// <summary>
    	/// Initializes the collection with default values.
    	/// </summary>
    	public CalibrationDataTypeCollection() {}
    	
    	/// <summary>
    	/// Initializes the collection with an initial capacity.
    	/// </summary>
    	public CalibrationDataTypeCollection(int capacity) : base(capacity) {}
    	
    	/// <summary>
    	/// Initializes the collection with another collection.
    	/// </summary>
    	public CalibrationDataTypeCollection(IEnumerable<CalibrationDataType> collection) : base(collection) {}
    	#endregion
                    
        #region Static Operators
        /// <summary>
        /// Converts an array to a collection.
        /// </summary>
        public static implicit operator CalibrationDataTypeCollection(CalibrationDataType[] values)
        {
            if (values != null)
            {
                return new CalibrationDataTypeCollection(values);
            }

            return new CalibrationDataTypeCollection();
        }
        
        /// <summary>
        /// Converts a collection to an array.
        /// </summary>
        public static explicit operator CalibrationDataType[](CalibrationDataTypeCollection values)
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
            CalibrationDataTypeCollection clone = new CalibrationDataTypeCollection(this.Count);

            for (int ii = 0; ii < this.Count; ii++)
            {
                clone.Add((CalibrationDataType)Utils.Clone(this[ii]));
            }

            return clone;
        }
        #endregion
    }
    #endregion
    #endif
    #endregion
}
