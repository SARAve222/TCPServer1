using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TCPServertest1
{
    class Program
    {

        static void Main(string[] args)
        {

           // IPAddress AddressIP = IPAddress.Parse("127.0.0.1");
            int Port = 8888;
            TcpListener serverSocket = new TcpListener(/*AddressIP,*/Port);

            int requestCount = 0;

            TcpClient clientSocket = default(TcpClient);

            serverSocket.Start();

            Console.WriteLine(" >> Server Started");

            clientSocket = serverSocket.AcceptTcpClient();

            Console.WriteLine(" >> Accept connection from client");

            requestCount = 0;

            while ((true))
            {

                try
                {

                    requestCount = requestCount + 1;

                    NetworkStream networkStream = clientSocket.GetStream();

                    byte[] bytesFrom = new byte[10025];

                    networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);

                    string dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);

                   // dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));

                    Console.WriteLine(" >> Data from client - " + dataFromClient);

                    string serverResponse = "Last Message from client \n" + dataFromClient;

                    Byte[] sendBytes = Encoding.ASCII.GetBytes(serverResponse);

                    networkStream.Write(sendBytes, 0, sendBytes.Length);

                    networkStream.Flush();

                    Console.WriteLine(" >> " + serverResponse);

                }

                catch (Exception ex)
                {

                    Console.WriteLine(ex.ToString());

                }
                finally
                {

                }
                
   //            Console.ReadKey();
            }

            Console.ReadKey();

            clientSocket.Close();

            serverSocket.Stop();

            Console.WriteLine(" >> exit");

            Console.ReadLine();

        }



    }

}
