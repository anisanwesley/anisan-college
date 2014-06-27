using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Anisoket
{
    public class SoketSupport
    {
        private As _typeConnection;

        private NetworkStream _ns;

        private StreamWriter _writer;
        private StreamReader _reader;

        private TcpListener _server;
        private TcpClient _client;
        private Socket _socket;

        private string _ip;
        private int _port;

        private bool _isStarted;
		
        /// <summary>
        /// Configure the soket as Client or Server, with Server's Ip and Port.
        /// </summary>
        /// <param name="typeConnection"></param>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public bool Configure(As typeConnection, IPAddress ip, int port)
        {
            _ip = ip.ToString();
            _port = Convert.ToInt32(port);

            _typeConnection = typeConnection;

            switch (typeConnection)
            {
                case As.Server:

                    _server = new TcpListener(ip, _port);
                    _server.Start();

                    break;
                case As.Client:

                    _client = new TcpClient();
                    break;
            }
            return _isStarted = _typeConnection != As.NotSet;
        }
        #region Overloads
        
        public bool Configure(As typeConnection, string ip, int port)
        {
            return Configure(typeConnection, IPAddress.Parse(ip), port);

        }
        public bool Configure(As typeConnection, IPAddress ip, string port)
        {
            return Configure(typeConnection, ip, Convert.ToInt32(port));

        }
        public bool Configure(As typeConnection, string ip, string port)
        {
            return Configure(typeConnection, IPAddress.Parse(ip), Convert.ToInt32(port));
        }
        #endregion

        /// <summary>
        /// Receives data via socket, case:
        /// As.Client: must be called before ReceiveData
        /// As.Server: must be called after ReceiveData
        /// </summary>
        /// <param name="data"></param>
        /// <returns>bool success</returns>
        public bool SendData(string data)
        {
            try
            {
                if(_isStarted)
                            switch (_typeConnection)
                            {
                                case As.Server:

                                    _writer.WriteLine(data);
                                    _writer.Flush();
                                    _socket.Close();

                                    break;
                                case As.Client:

                                    _client = new TcpClient(_ip, _port);
                                    _ns = _client.GetStream();

                                    _writer = new StreamWriter(_ns);
                                    _reader = new StreamReader(_ns);

                                    if (_client.Connected)

                                        _writer.WriteLine(data);
                                    _writer.Flush();

                                    break;
                            }
                return _isStarted;
            }
            catch (System.Exception)
            {
                return false;
            }
			

        }

        /// <summary>
        /// Dends data to socket, case:
        /// As.Client: must be called after SendData
        /// As.Server: must be called before SendData
        /// </summary>
        /// <returns>
        /// string data
        /// </returns>
        public string ReceiveData()
        {
		try
            {
				string data = null;

                if (_isStarted)
				switch (_typeConnection)
				{
					case As.Server:
						_socket = _server.AcceptSocket();

						_ns = new NetworkStream(_socket);

						_writer = new StreamWriter(_ns);
						_reader = new StreamReader(_ns);

						return _reader.ReadLine();

					case As.Client:

						data = _reader.ReadLine();
						_client.Close();
						break;
				}
                return data ?? _typeConnection.ToString();
			}
			catch (Exception exe)
            {
                var msg = exe.Message;
                while (exe.InnerException != null)
                {
                    exe = exe.InnerException;
                    msg += " " + exe.Message;
                }
                    return msg;
            }
		}


        public As GetTypeConnection()
        {
            return _typeConnection;
        }
        /// <summary>
        /// Return list of virtual or physical machine IPs on current gateway
        /// </summary>
        /// <returns></returns>
        public static string[] GetLocalIp()
        {
            string nome = Dns.GetHostName();

            IPAddress[] ip = Dns.GetHostAddresses(nome);

            var response = new string[ip.Length+1];
            response[0] = "127.0.0.1";
            for (int index = 0; index < ip.Length; index++)
                response[index+1] = ip[index].ToString();
            
            return response;

        }

        /// <summary>
        /// Break the cicle send/receive
        /// </summary>
        public void ForceClose()
        {
            switch (_typeConnection)
            {
                case As.Server:

                    if(_socket!=null)
                    _socket.Close();
                    break;

					case As.Client:
					
                    if (_client != null)
                    _client.Close();
                    break;
            }
        }
    }

    public enum As
    {
        NotSet, Client, Server
    }

}
