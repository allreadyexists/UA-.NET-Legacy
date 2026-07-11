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
using System.Threading;

namespace Opc.Ua
{    
    /// <summary>
    /// Stores context information for the current secure channel.
    /// </summary>
    public class SecureChannelContext
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance with the specified property values.
        /// </summary>
        /// <param name="secureChannelId">The secure channel identifier.</param>
        /// <param name="endpointDescription">The endpoint description.</param>
        /// <param name="messageEncoding">The message encoding.</param>
        public SecureChannelContext(
            string              secureChannelId,
            EndpointDescription endpointDescription,
            RequestEncoding     messageEncoding)
        {        
            m_secureChannelId     = secureChannelId;
            m_endpointDescription = endpointDescription;
            m_messageEncoding     = messageEncoding;
        }

        /// <summary>
        /// Initializes a new instance with the context for the current thread.
        /// </summary>
        protected SecureChannelContext()
        {        
            SecureChannelContext context = SecureChannelContext.Current;

            if (context != null)
            {
                m_secureChannelId     = context.SecureChannelId;
                m_endpointDescription = context.EndpointDescription;
                m_messageEncoding     = context.MessageEncoding;
            }
        }
        #endregion
                
        #region Public Properties
        /// <summary>
        /// TThe unique identifier for the secure channel.
        /// </summary>
        /// <value>The secure channel identifier.</value>
        public string SecureChannelId
        {
            get { return m_secureChannelId; }
        }

        /// <summary>
        /// The description of the endpoint used with the channel.
        /// </summary>
        /// <value>The endpoint description.</value>
        public EndpointDescription EndpointDescription
        {
            get { return m_endpointDescription; }
        }

        /// <summary>
        /// The encoding used with the channel.
        /// </summary>
        /// <value>The message encoding.</value>
        public RequestEncoding MessageEncoding
        {
            get { return m_messageEncoding; }
        }     
        #endregion   

		#region Static Members
        /// <summary>
        /// The active secure channel for the thread.
        /// </summary>
        /// <value>The current secure channel context.</value>
        public static SecureChannelContext Current        
        {
            get
            {
                return Thread.GetData(s_Dataslot) as SecureChannelContext;
            }

            set
            {
                Thread.SetData(s_Dataslot, value);
            }
        }
        #endregion

        #region Private Fields
        private string m_secureChannelId;
        private EndpointDescription m_endpointDescription;
        private RequestEncoding m_messageEncoding;
        private static LocalDataStoreSlot s_Dataslot = Thread.AllocateDataSlot();
        #endregion
    }

    /// <summary>
    /// The message encoding used with a request.
    /// </summary>
    public enum RequestEncoding
    {
        /// <summary>
        /// The request used the UA binary encoding.
        /// </summary>
        Binary,

        /// <summary>
        /// The request used the UA XML encoding.
        /// </summary>
        Xml
    }
}
