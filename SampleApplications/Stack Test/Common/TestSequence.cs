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
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Opc.Ua.StackTest
{ 
    /// <summary>
    /// A sequence of test cases to run.
    /// </summary>
    public partial class TestSequence 
    {                
        #region Static Methods
        /// <summary>
        /// Loads a test sequence from a file.
        /// </summary>
        public static TestSequence Load(string filepath)
        {
            return Load(File.OpenRead(filepath));
        }
        
        /// <summary>
        /// Loads a test sequence from a stream.
        /// </summary>
        public static TestSequence Load(Stream istrm)
        {
			XmlTextReader reader = new XmlTextReader(istrm);

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TestSequence));
                
                TestSequence sequence = serializer.Deserialize(reader) as TestSequence;
                                
                uint lastId = 1;

                foreach (TestCase testcase in sequence.TestCase)
                {
                    if (testcase.TestId == 0)
                    {
                        testcase.TestId = lastId++;
                    }
                    else
                    {
                        if (testcase.TestId < lastId)
                        {
                            testcase.TestId = lastId++;
                        }
                        else
                        {
                            lastId = testcase.TestId+1;
                        }
                    }
                }

                return sequence;
            }
            finally
            {
                reader.Close();
            }
        }
        
        /// <summary>
        /// Saves a test sequence to a file.
        /// </summary>
        public static void Save(string filepath, TestSequence sequence)
        {
            Save(File.Open(filepath, FileMode.Create), sequence);
        }
        
        /// <summary>
        /// Saves a test sequence to a stream.
        /// </summary>
        public static void Save(Stream ostrm, TestSequence sequence)
        {
			XmlTextWriter writer = new XmlTextWriter(ostrm, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TestSequence));
                serializer.Serialize(writer, sequence);
            }
            finally
            {
                writer.Close();
            }
        }
        #endregion
    }
    
    /// <summary>
    /// Bits that indicate what level of detail to include in the logs.
    /// </summary>
    [Flags]
    public enum TestLogDetailMasks : int
    {
        /// <summary>
        /// Log all errors.
        /// </summary>
        Errors = 0x01,

        /// <summary>
        /// Log an event when starting the first iteration for a test case.
        /// </summary>
        FirstStart = 0x02,

        /// <summary>
        /// Log an event when starting the all iterations for a test case.
        /// </summary>
        AllsStarts = 0x04,

        /// <summary>
        /// Log an event after completing the last iteration for a test case.
        /// </summary>
        LastEnd = 0x08,

        /// <summary>
        /// Log an event after completing each iterations for a test case.
        /// </summary>
        AllsEnds = 0x10,

        /// <summary>
        /// Log first 24 bytes of random data used to create the request/response data.
        /// </summary>
        RandomData = 0x20        
    }
}
