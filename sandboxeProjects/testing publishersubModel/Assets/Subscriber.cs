using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

namespace com
{


    public class Subscriber
    {
        private bool _running = true;

        private Socket _clientSocket;

        public Subscriber(int port, Action<byte[]> callback)
        {
            new Thread(() => Start(port, callback)).Start();
        }

        private void Start(int port, Action<byte[]> callBack)
        {
            while (_running)
                try
                {
                    _clientSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                    _clientSocket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), port));
                    while (_running)
                    {
                        byte[] bytes=new byte[4];
                        //_clientSocket.Receive(bytes);
                        var dataSize = 23;
                        // BitConverter.ToInt32(bytes, 0);

                        if (dataSize == 0) throw new IOException();

                        var data = new byte[dataSize];
                        var recieved = 0;
                        while (recieved < dataSize && _running)
                        {
                            recieved += _clientSocket.Receive(data, recieved, dataSize - recieved, SocketFlags.None);

                        }
                        string message = "";
                        message = System.Text.Encoding.UTF8.GetString(data,0,dataSize);
                        Debug.Log(message);
                        
                        if (dataSize > 0) callBack(data);
                    }
                }
                catch (Exception)
                {
                    Thread.Sleep(1000);
                }
        }

        public void Stop()
        {
            _running = false;
            try
            {
                if (_clientSocket.Connected) _clientSocket.Shutdown(SocketShutdown.Both);
                _clientSocket.Close();
            }
            finally
            {
                _clientSocket = null;
            }
        }
    }
}