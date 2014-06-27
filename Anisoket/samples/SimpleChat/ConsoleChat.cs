using System;
using System.Threading;
using Anisoket;


namespace Anichat
{
    //Este Programa cria um chat P2P em Console Application
    class Program
    {
        private static string _nick;
        private static string _ipLocal;
        private static string _ipDestiny;

        //portas devem ser diferentes (por enquanto)
        private static string _portLocal;
        private static string _portDestiny;

        private static bool _hasColor;
        
        private static Thread _server;
        private static Thread _client;

        static readonly SoketSupport _soketClient = new SoketSupport();
        static readonly SoketSupport _soketServer = new SoketSupport();

        static void Main()
        {


            Console.WriteLine("Digite seu nick:");
            _nick = Console.ReadLine();

            Console.WriteLine("Deseja cores? [s/n]");
            var readLine = Console.ReadLine();
            if (readLine != null) _hasColor = readLine.ToLower()=="s";

            Console.WriteLine("Pegando IPs disponiveis... ");
            var ips = _soketClient.GetLocalIp();
            Console.WriteLine("Digite qual localhost deseja utilizar:");
            for (int index = 0; index < ips.Length; index++)
            {
                var ip = ips[index];
                Console.WriteLine("[{0}] - {1}",index,ip);
            }



            _ipLocal = ips[Convert.ToInt16(Console.ReadKey().KeyChar.ToString())];

            Console.WriteLine("\nEscolhido: "+_ipLocal);
            Console.WriteLine("Informe o IP da outra ponta:");
            _ipDestiny = Console.ReadLine();

            Console.Write("Informe a Porta Local...");
            _portLocal = Console.ReadLine();
            Console.WriteLine(_portLocal);

            Console.Write("Informe a Porta da outra ponta:");
            _portDestiny = Console.ReadLine();
            Console.WriteLine(_portDestiny);
            
            Console.WriteLine("Iniciando Threads..");
            _server = new Thread(ServerStart);
            _client = new Thread(ClientStart);

            Console.WriteLine("Startando Threads..");
            _server.Start();
            _client.Start();

            Console.WriteLine("Pronto para digitar.");

            //Threads assumem

        }

        public static void ServerStart()
        {
            //server recebe requisição e responde
            _soketServer.Configure(As.Server,_ipLocal,_portLocal);
            
            for(;;)
            {
                try
                {
                    //server aguardando requisição
                    var texto =_soketServer.ReceiveData();
                    if(_hasColor)
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(texto);
                    if (_hasColor)
                    Console.ForegroundColor = ConsoleColor.Yellow;

                    //não precisa retornar, apenas fecha coneção
                    //caso contrario var response = _soketServer.SendData("Resposta");
                    _soketServer.ForceClose();
                }
                catch (Exception exe)
                {
                    Console.WriteLine("EXCEPTION: "+exe.Message);
                }
            }

        }
        private static void ClientStart()
        {
            //cliente envia requisição e aguarda resposta
            _soketClient.Configure(As.Client, _ipDestiny,_portDestiny);

            for (;;)
            {
                Thread.Sleep(2000);
                try
                {
                    var texto = Console.ReadLine();

                    _soketClient.SendData(_nick + " : " + texto);
                    
                    //não espera receber, apenas fecha coneção
                    //caso contrario var response = _soketClient.ReceiveData();
                    _soketClient.ForceClose();
                }
                catch (Exception exe)
                {
                    Console.WriteLine("EXCEPTION: " + exe.Message);
                }

               
            }
        }
    }
}
