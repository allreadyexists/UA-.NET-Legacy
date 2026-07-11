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
using System.ServiceModel.Channels;
using System.Threading;
using System.Diagnostics;
using Opc.Ua.Client;
using Opc.Ua.StackTest;
using Opc.Ua.Server;
// using Opc.Ua.NativeStack;

namespace Opc.Ua.StackTest
{

    public partial class TestClient
    {
        /*
        /// <summary>
        /// Directly tests the ANSI C and .NET serializer interoperability.
        /// </summary>
        private void ExecuteTest_SerializerDirect(TestCase testCase)
        {
            // ignore test cases other than the serializer tests.
            switch (testCase.Name)
            {
                case TestCases.SerializerDirect:
                case TestCases.SerializerDirectEx:
                {
                    break;
                }

                default:
                {
                    return;
                }
            }

            PseudoRandom random = new PseudoRandom(m_randomFilePath);
            TestCaseContext context = TestUtils.GetExecutionContext(testCase);
            NativeStackSerializerTest tester = new NativeStackSerializerTest();

            for (int ii = testCase.Start; ii < testCase.Count; ii++)
            {
                lock (m_lock)
                {
                    if (m_cancel)
                    {
                        break;
                    }

                    RaiseEvent(new TestSequenceEventArgs(testCase.TestId, testCase.Name, ii));
                }

                try
                {
                    switch (testCase.Name)
                    {
                        case TestCases.SerializerDirect:
                        {
                            ExecuteTest_SerializerDirect(tester, context, random, testCase, ii);
                            break;
                        }

                        case TestCases.SerializerDirectEx:
                        {
                            ExecuteTest_SerializerDirectEx(tester, context, random, testCase, ii);
                            break;
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new ServiceResultException(
                        StatusCodes.BadUnexpectedError,
                        Utils.Format("{0} test failed on iteration {1}.", testCase.Name, ii),
                        e);
                }
            }            
        }

        /// <summary>
        /// Directly tests the ANSI C and .NET serializer interoperability.
        /// </summary>
        private void ExecuteTest_SerializerDirect(
             NativeStackSerializerTest tester,
             TestCaseContext           context,
             PseudoRandom              random, 
             TestCase                  testCase, 
             int                       iteration)
        {
            int alwaysCheckSizes = TestUtils.GetTestParameterIntValue(TestCases.AlwaysCheckSizes, testCase.Parameter);
            
            RequestHeader requestHeader = new RequestHeader();

            requestHeader.Timestamp = DateTime.UtcNow;
            requestHeader.ReturnDiagnostics = (uint)DiagnosticsMasks.All;

            random.Start(
                (int)(testCase.Seed + iteration),
                (int)m_sequenceToExecute.RandomDataStepSize,
                context);

            TestStackRequest request = new TestStackRequest();

            request.RequestHeader = requestHeader;
            request.TestId = testCase.TestId;
            request.Iteration = iteration;

            request.Input = random.GetArrayVariant(false);

            byte[] bytes = BinaryEncoder.EncodeMessage(request, m_messageContext);

            IntPtr pRequest = tester.DecodeTestStack(bytes, false, alwaysCheckSizes != 0);

            bytes = tester.EncodeTestStack(pRequest, false, alwaysCheckSizes != 0);

            IEncodeable encodeable = BinaryDecoder.DecodeMessage(bytes, typeof(TestStackRequest), m_messageContext);

            TestStackRequest request2 = encodeable as TestStackRequest;

            if (request2 == null)
            {
                throw new ServiceResultException(
                    StatusCodes.BadInvalidState,
                    Utils.Format("'{0}' is not a TestStackRequest.", encodeable));
            }

            if (!Compare.CompareVariant(request.Input, request2.Input))
            {
                throw new ServiceResultException(
                    StatusCodes.BadInvalidState,
                    Utils.Format("'{0}' is not equal to '{1}'.", request.Input, request2.Input));
            }
        }
                
        /// <summary>
        /// Directly tests the ANSI C and .NET serializer interoperability.
        /// </summary>
        private void ExecuteTest_SerializerDirectEx(
             NativeStackSerializerTest tester,
             TestCaseContext           context,
             PseudoRandom              random, 
             TestCase                  testCase, 
             int                       iteration)
        {
            int alwaysCheckSizes = TestUtils.GetTestParameterIntValue(TestCases.AlwaysCheckSizes, testCase.Parameter);
            
            RequestHeader requestHeader = new RequestHeader();

            requestHeader.Timestamp = DateTime.UtcNow;
            requestHeader.ReturnDiagnostics = (uint)DiagnosticsMasks.All;

            random.Start(
                (int)(testCase.Seed + iteration),
                (int)m_sequenceToExecute.RandomDataStepSize,
                context);

            TestStackExRequest request = new TestStackExRequest();

            request.RequestHeader = requestHeader;
            request.TestId = testCase.TestId;
            request.Iteration = iteration;

            request.Input = random.GetCompositeTestType();

            byte[] bytes = BinaryEncoder.EncodeMessage(request, m_messageContext);

            IntPtr pRequest = tester.DecodeTestStack(bytes, true, alwaysCheckSizes != 0);

            bytes = tester.EncodeTestStack(pRequest, true, alwaysCheckSizes != 0);

            IEncodeable encodeable = BinaryDecoder.DecodeMessage(bytes, typeof(TestStackRequest), m_messageContext);

            TestStackExRequest request2 = encodeable as TestStackExRequest;

            if (request2 == null)
            {
                throw new ServiceResultException(
                    StatusCodes.BadInvalidState,
                    Utils.Format("'{0}' is not a TestStackExRequest.", encodeable));
            }

            if (!Compare.CompareCompositeTestType(request.Input, request2.Input))
            {
                throw new ServiceResultException(
                    StatusCodes.BadInvalidState,
                    Utils.Format("'{0}' is not equal to '{1}'.", request.Input, request2.Input));
            }
        }
        */
    }
}
