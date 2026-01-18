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
using System.Runtime.InteropServices;
using System.Globalization;

namespace Opc.Ua.Com.Server.Ae
{
    /// <summary>
    /// Primary purpose is to maintain list of server instances and as a respository for
    /// data common to all server instances.
    /// </summary>
    public class Global
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Global()
        {
            m_StartTime = DateTime.Now;
            m_EvServerSet = new List<ComAeProxy>();
            m_lcid = ComUtils.LOCALE_SYSTEM_DEFAULT;
            m_StdAttrNames = new string[] { "AckComment", "Areas" };
            m_StdAttrIds = new int[] { 0, 1 };
        }

        /// <summary>
        /// Called during incoming event processing and as a result of a client-initiated Refresh.
        /// Forwards to each server instance for further processing.
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="Condition"></param>
        /// <param name="cond"></param>
        public void NotifyClients(string Source, string Condition, OPCCondition cond)
        {
            OnEventClass OEClass = new OnEventClass(Source, Condition, cond);

            foreach (ComAeProxy s in m_EvServerSet)
            {
                s.ProcessNewEvent(OEClass);
            }

        }

        /// <summary>
        /// Called from server object constructor to insert server reference into the global list
        /// </summary>
        /// <param name="s"></param>
        public void ServerListInsert(ComAeProxy s)
        {
            m_EvServerSet.Add(s);
        }

        /// <summary>
        /// Called from server object finalizer to remove server reference from the global list
        /// </summary>
        /// <param name="s"></param>
        public void ServerListRemove(ComAeProxy s)
        {
            m_EvServerSet.Remove(s);
        }

        /// <summary>
        /// Start time for this COM proxy
        /// </summary>
        public DateTime StartTime
        {
            get { return m_StartTime; }
        }

        /// <summary>
        /// Locale ID
        /// </summary>
        public int LCID
        {
            get { return m_lcid; }
        }

        /// <summary>
        /// Static accessor
        /// </summary>
        public static Global TheGlobal
        {
            get { return theGlobal; }
        }

        /// <summary>
        /// Standard attribute names
        /// </summary>
        public string[] StdAttrNames
        {
            get { return m_StdAttrNames; }
        }

        /// <summary>
        /// Standard attribute IDs
        /// </summary>
        public int[] StdAttrIds
        {
            get { return m_StdAttrIds; }
        }

        private readonly string[] m_StdAttrNames;
        private readonly int[] m_StdAttrIds;
        private static Global theGlobal = new Global();
        private List<ComAeProxy> m_EvServerSet;
        private DateTime m_StartTime;
        private int m_lcid;

    }

}
