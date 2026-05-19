using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Aula5_1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            TcpClient cliente = new TcpClient("127.0.0.1", 5000);
            Console.WriteLine("Cliente conectado ao servidor");
            NetworkStream stream = cliente.GetStream();

            while(true)
            {
                Console.WriteLine("Digite sua mensagem: ");
                string mensagemEnvio = Console.ReadLine();

                byte[] bufferDeEnvio = Encoding.UTF8.GetBytes(mensagemEnvio);

                stream.Write(bufferDeEnvio, 0, bufferDeEnvio.Length);


                //recebimento de mensagem

                byte[] buferdeRecebimento = new byte[1024];

                int bytesLidos = stream.Read(buferdeRecebimento,0, buferdeRecebimento.Length);

                string mensagemRecebida = Encoding.UTF8.GetString(buferdeRecebimento, 0, bytesLidos);

                Console.WriteLine("Servidor: "+mensagemRecebida);








            }






        }
    }
}
