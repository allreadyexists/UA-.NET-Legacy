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

namespace Quickstarts {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://opcfoundation.org/UA/HA/TestData")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://opcfoundation.org/UA/HA/TestData", IsNullable=false)]
    public partial class TestData {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("DataSet", IsNullable=false)]
        public RawDataSetType[] RawDataSets;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("DataSet", IsNullable=false)]
        public ProcessedDataSetType[] ProcessedDataSets;
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://opcfoundation.org/UA/HA/TestData")]
    public partial class RawDataSetType {
        
        /// <remarks/>
        public string Name;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Value", IsNullable=false)]
        public ValueType[] Values;
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://opcfoundation.org/UA/HA/TestData")]
    public partial class ValueType {
        
        /// <remarks/>
        public string Timestamp;
        
        /// <remarks/>
        public string Value;
        
        /// <remarks/>
        public string Quality;
        
        /// <remarks/>
        public string Comment;
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    // [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://opcfoundation.org/UA/HA/TestData")]
    public partial class ProcessedDataSetType {
        
        /// <remarks/>
        public string DataSetName;
        
        /// <remarks/>
        public string AggregateName;
        
        /// <remarks/>
        public uint ProcessingInterval;
        
        /// <remarks/>
        public bool Stepped;
        
        /// <remarks/>
        public bool TreatUncertainAsBad;
        
        /// <remarks/>
        public ushort PercentBad;
        
        /// <remarks/>
        public ushort PercentGood;
        
        /// <remarks/>
        public bool UseSlopedExtrapolation;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Value", IsNullable=false)]
        public ValueType[] Values;
    }
}
