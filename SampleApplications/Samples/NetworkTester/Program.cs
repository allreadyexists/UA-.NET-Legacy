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
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace Opc.Ua.NetworkTester
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
        
    /// <summary>
    /// A class which acts as proxy for a socket connection.
    /// </summary>
    public class Server
    {
        /// <summary>
        /// Initializes the server with the listener and server url.
        /// </summary>
        public Server(string listenerUrl, string serverUrl)
        {
            m_listenerUrl = new Uri(listenerUrl);
            m_serverUrl = new Uri(serverUrl);
        }

        /// <summary>
        /// Starts listening at the specified port.
        /// </summary>
        public void Start()
        {
            lock (m_lock)
            {
                m_connections = new List<Connection>();

                IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, m_listenerUrl.Port);
	
                m_listeningSocket = new Socket(endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);             
                m_listeningSocket.Bind(endpoint);
                m_listeningSocket.Listen(Int32.MaxValue);
                
                m_listeningSocket.BeginAccept(OnAccept, m_listeningSocket);
            }
        }

        /// <summary>
        /// Stops listening.
        /// </summary>
        public void Stop()
        {
            lock (m_lock)
            {
                m_listeningSocket.Close();
                m_listeningSocket = null;

                foreach (Connection connection in new List<Connection>(m_connections))
                {
                    Close(connection);
                }
            }
        }
        
        /// <summary>
        /// Handles a new connection.
        /// </summary>
        private void OnAccept(IAsyncResult result)
        {
            // find the connection.
            Socket listeningSocket = result.AsyncState as Socket;

            if (listeningSocket == null && !listeningSocket.Connected)
            {
                return;
            }

            try
            {                    
                // accept the socket.
                Socket socket = listeningSocket.EndAccept(result);

                // go back and wait for the next connection.
                listeningSocket.BeginAccept(OnAccept, listeningSocket);
    
                // connect to the server.
                Connect(socket, m_serverUrl);
            }
            catch (Exception e)
            {
                Utils.Trace(e, "Unexpected error accepting a new connection.");
            }
        }
        
        /// <summary>
        /// Stores the state of a connection.
        /// </summary>
        private class Connection
        {
            public Socket IncomingSocket;
            public Socket OutgoingSocket;
            public byte[] ClientToServerBuffer;
            public byte[] ServerToClientBuffer;
        }
        
        /// <summary>
        /// Closes the connection.
        /// </summary>
        private void Close(Connection connection)
        {            
            lock (m_lock)
            {
                m_connections.Remove(connection);
            }

            if (connection.IncomingSocket != null && connection.IncomingSocket.Connected)
            {
                connection.IncomingSocket.Close(0);
            }
            
            if (connection.OutgoingSocket != null && connection.OutgoingSocket.Connected)
            {
                connection.OutgoingSocket.Close(0);
            }
        }

        /// <summary>
        /// Connects to the server.
        /// </summary>
        private void Connect(Socket incomingSocket, Uri uri)
        { 
            Socket outgoingSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            Connection connection = new Connection();
            
            connection.IncomingSocket = incomingSocket;
            connection.OutgoingSocket = outgoingSocket;

            lock (m_lock)
            {
                m_connections.Add(connection);
            }

            outgoingSocket.BeginConnect(uri.DnsSafeHost, uri.Port, EndConnect, connection);
        }
        
        /// <summary>
        /// Called to complete an asynchronous connect operation.
        /// </summary>
        private void EndConnect(IAsyncResult result)
        {
            // find the connection.
            Connection connection = result.AsyncState as Connection;

            if (connection == null)
            {
                return;
            }

            try
            {
                // complete connection.
                connection.OutgoingSocket.EndConnect(result);   

                // start read from client.
                connection.ClientToServerBuffer = new byte[UInt16.MaxValue];
                
                connection.IncomingSocket.BeginReceive(
                    connection.ClientToServerBuffer,
                    0,
                    connection.ClientToServerBuffer.Length,
                    SocketFlags.None,
                    OnReadFromClient,
                    connection);

                // start read from server.
                connection.ServerToClientBuffer = new byte[UInt16.MaxValue];
                
                connection.OutgoingSocket.BeginReceive(
                    connection.ServerToClientBuffer,
                    0,
                    connection.ServerToClientBuffer.Length,
                    SocketFlags.None,
                    OnReadFromServer,
                    connection);
            }
            catch (Exception e)
            {
                Utils.Trace(e, "Unexpected error connecting to server.");
                Close(connection);
            }
        }
        
        /// <summary>
        /// Called to complete an asynchronous read operation.
        /// </summary>
        private void OnReadFromClient(IAsyncResult result)
        {
            // find the connection.
            Connection connection = result.AsyncState as Connection;

            if (connection == null || !connection.IncomingSocket.Connected)
            {
                return;
            }

            try
            {
                // complete read.
                int bytesReceived = connection.IncomingSocket.EndReceive(result);   

                if (bytesReceived == 0)
                {                    
                    Close(connection);
                    return;
                }

                connection.OutgoingSocket.Send(connection.ClientToServerBuffer, 0, bytesReceived, SocketFlags.None);
                      
                // start read from client.                
                connection.IncomingSocket.BeginReceive(
                    connection.ClientToServerBuffer,
                    0,
                    connection.ClientToServerBuffer.Length,
                    SocketFlags.None,
                    OnReadFromClient,
                    connection);
            }
            catch (Exception e)
            {
                Utils.Trace(e, "Unexpected error reading from client.");
                Close(connection);
            }
        }
        
        /// <summary>
        /// Called to complete an asynchronous read operation.
        /// </summary>
        private void OnReadFromServer(IAsyncResult result)
        {
            // find the connection.
            Connection connection = result.AsyncState as Connection;

            if (connection == null || !connection.IncomingSocket.Connected)
            {
                return;
            }

            try
            {
                // complete read.
                int bytesReceived = connection.OutgoingSocket.EndReceive(result);   
                
                if (bytesReceived == 0)
                {                    
                    Close(connection);
                    return;
                }
                    
                connection.IncomingSocket.Send(connection.ServerToClientBuffer, 0, bytesReceived, SocketFlags.None);           
                
                // start read from client.                
                connection.OutgoingSocket.BeginReceive(
                    connection.ServerToClientBuffer,
                    0,
                    connection.ServerToClientBuffer.Length,
                    SocketFlags.None,
                    OnReadFromServer,
                    connection);
            }
            catch (Exception e)
            {
                Utils.Trace(e, "Unexpected error reading from server.");
                Close(connection);
            }
        }

        private object m_lock = new object();
        private Uri m_listenerUrl;
        private Uri m_serverUrl;
        private Socket m_listeningSocket;
        private List<Connection> m_connections;
    }
}
