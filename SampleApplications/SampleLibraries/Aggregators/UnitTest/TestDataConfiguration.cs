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
using System.Reflection;

using Opc.Ua;

namespace OSIsoft.UA.HAUnitTest
{
    [DataContract(Namespace = "http://osisoft.com/UA/Server/PIUAServer/HATestConfiguration.xsd")]
    public class HistoricalConfiguration  // simple version of Opc.Ua.HistoricalConfiguration
    {
        public HistoricalConfiguration()
        {
            AggregateConfiguration = new AggregateConfiguration();
            Stepped = false;
        }

        #region Persistent properties
        [DataMember(Order = 0)]
        public AggregateConfiguration AggregateConfiguration { get; set; }

        [DataMember(Order = 1)]
        public bool Stepped { get; set; }
        #endregion
    }

    [DataContract(Namespace = "http://osisoft.com/UA/Server/PIUAServer/HATestConfiguration.xsd")]
    public class DataValueEntry
    {
        #region constructor
        public DataValueEntry()
        {
            m_DataValue = new DataValue();
            m_DataValue.ServerTimestamp = DateTime.MinValue;
            Annotations = new List<Annotation>();
        }
        #endregion

        #region Persistent properties
        [DataMember(Order = 0)]
        public DateTime SourceTimestamp
        {
            get
            {
                return m_DataValue.SourceTimestamp;
            }
            set
            {
                if (m_DataValue == null)
                    m_DataValue = new DataValue();
                m_DataValue.SourceTimestamp = value;
            }
        }
        [DataMember(Order = 1)]
        public string StatusCode
        {
            get
            {
                return m_DataValue.StatusCode.ToString();
            }
            set
            {
                if (m_DataValue == null)
                    m_DataValue = new DataValue();
                m_DataValue.StatusCode = GetStatusCode(value);
            }
        }            

        [DataMember(Order = 2)]
        public string Value
        {
            get
            {
                return m_DataValue.Value != null ? m_DataValue.Value.ToString() : null;
            }
            set
            {
                if (m_DataValue == null)
                    m_DataValue = new DataValue();
                if (value == null)
                    m_DataValue.Value = null;
                if (String.Compare(value, "true", true) == 0 || String.Compare(value, "false", true) == 0)
                    m_DataValue.Value = Convert.ToBoolean(value);
                string[] tokens = value.Split(".".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (String.Compare(tokens[0], "StatusCodes") == 0)
                {
                    switch (tokens[1])
                    {
                        case "Good":
                            m_DataValue.Value = new StatusCode(StatusCodes.Good);
                            break;
                        case "Bad":
                            m_DataValue.Value = new StatusCode(StatusCodes.Bad);
                            break;
                        case "Uncertain":
                            m_DataValue.Value = new StatusCode(StatusCodes.Uncertain);
                            break;
                    }
                }
                else
                {
                    try
                    {
                        m_DataValue.Value = Convert.ToDouble(value);
                    }
                    catch (FormatException)
                    {
                        m_DataValue.Value = value;
                    }
                }
            }
        }

        [DataMember(Order = 3, IsRequired = false)]
        public List<Annotation> Annotations { get; set; }
        #endregion

        #region private fields
        private DataValue m_DataValue;
        #endregion

        #region private methods
        private static StatusCode GetStatusCode(string statusCodeString)
        {
            string[] statusCodes = statusCodeString.Split(",".ToCharArray());
            string code;
            int aggregateBitCounts = 0;
            aggregateBitCounts = statusCodes.Length - 1;
            code = statusCodes[statusCodes.Length - 1].Trim();

            FieldInfo[] fieldInfo = typeof(StatusCodes).GetFields(BindingFlags.Public | BindingFlags.Static);
            for (int i = 0; i < fieldInfo.Length; i++)
            {
                if (String.Compare(fieldInfo[i].Name, code, true) == 0)
                {
                    uint actualCode = (uint)fieldInfo[i].GetValue(null);
                    StatusCode status = actualCode;
                    for(int ii = 0; ii < aggregateBitCounts; ii++)
                    {
                        AggregateBits bitCode = (AggregateBits)Enum.Parse(typeof(AggregateBits), statusCodes[ii]);
                        status.AggregateBits |= bitCode;
                    }
                    return status;
                }
            }
            throw new ArgumentException("Invalid StatusCodeString");
        }
        #endregion

        #region public methods
        public DataValue GetDataValue()   {   return m_DataValue; }
        public void SetDataValue(DataValue dv)   {   m_DataValue = (DataValue)dv.Clone();    }
        #endregion
    }

    [DataContract(Namespace = "http://osisoft.com/UA/Server/PIUAServer/HATestConfiguration.xsd")]
    public class TestItem
    {
        [DataMember(Order = 0)]
        public string Name { get; set; }
        [DataMember(Order = 1)]
        public List<DataValueEntry> DataValues { get; set; }

        public TestItem()
        {
            Name = String.Empty;
            DataValues = new List<DataValueEntry>();
        }
    }

    [DataContract(Namespace = "http://osisoft.com/UA/Server/PIUAServer/HATestConfiguration.xsd")]
    public class TestData : TestItem
    {
        [DataMember(Order = 2)]
        public HistoricalConfiguration Configuration { get; set; }

        public TestData(): base()
        {
            Configuration = new HistoricalConfiguration();
        }
    }

    [DataContract(Namespace = "http://osisoft.com/UA/Server/PIUAServer/HATestConfiguration.xsd")]
    public class TestResult : TestItem
    {
        protected HistoryReadDetails m_details;
    }

    [DataContract(Namespace = "http://osisoft.com/UA/Server/PIUAServer/HATestConfiguration.xsd")]
    public class AggregateTestResult: TestResult
    {
        [DataMember(Order = 2)]
        public ReadProcessedDetails Details
        {
            get
            {
                return m_details as ReadProcessedDetails;
            }
            set
            {
                m_details = value;
            }
        }

        [DataMember(Order = 3)]
        public string TestDataName { get; set; }

        public AggregateTestResult(): base()
        {
            TestDataName = String.Empty;
            Details = new ReadProcessedDetails();
        }
    }

    [DataContract(Namespace = "http://osisoft.com/UA/Server/PIUAServer/HATestConfiguration.xsd")]
    public class BoundingValueTestResult : TestResult
    {
        [DataMember(Order = 2)]
        public ReadRawModifiedDetails Details
        {
            get
            {
                return m_details as ReadRawModifiedDetails;
            }
            set
            {
                m_details = value;
            }
        }

        public BoundingValueTestResult()
        {
            Details = new ReadRawModifiedDetails();
        }
    }

    public sealed class TestDataSet: List<TestData>
    {
        public static TestDataSet LoadFromXMLFile(string filePath)
        {
            return HelperMethods.LoadXMLFile(filePath, typeof(TestDataSet)) as TestDataSet;
        }
    }

    public sealed class AggregateTestResultSet: List<AggregateTestResult>
    {
        public static AggregateTestResultSet LoadFromXMLFile(string filePath)
        {
            return HelperMethods.LoadXMLFile(filePath, typeof(AggregateTestResultSet)) as AggregateTestResultSet;
        }
    }

    public sealed class BoundingValueTestResultSet: List<BoundingValueTestResult>
    {
        public static BoundingValueTestResultSet LoadFromXMLFile(string filePath)
        {
            return HelperMethods.LoadXMLFile(filePath, typeof(BoundingValueTestResultSet)) as BoundingValueTestResultSet;
        }
    }

    [DataContract(Namespace = "http://osisoft.com/UA/Server/PIUAServer/HATestConfiguration.xsd")]
    public class TestConfiguration
    {
        [DataMember(Order = 1)]
        public string Endpoint { get; set; }
        [DataMember(Order = 2)]
        public string NodeIdString_A1 { get; set; }
        [DataMember(Order = 3)]
        public string NodeIdString_A2 { get; set; }
        [DataMember(Order = 4)]
        public string NodeIdString_A3 { get; set; }
        [DataMember(Order = 5)]
        public string NodeIdString_A4 { get; set; }
        [DataMember(Order = 6)]
        public string NodeIdString_BV { get; set; }

        public Dictionary<string, string> TestNodes 
        { 
            get 
            {
                return new Dictionary<string, string>()
                {
                    { "TestData1", NodeIdString_A1},
                    { "TestData2", NodeIdString_A2},
                    { "TestData3", NodeIdString_A3},
                    { "TestData4", NodeIdString_A4},
                    { "BV", NodeIdString_BV}
                };
            }
        }

        public static TestConfiguration LoadFromXMLFile(string filePath)
        {
            return HelperMethods.LoadXMLFile(filePath, typeof(TestConfiguration)) as TestConfiguration;
        }
    }
}
