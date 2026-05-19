using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace servidor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TcpListener servidor = new TcpListener(IPAddress.Any, 5000);
            servidor.Start();
            Console.WriteLine("Servidor escutando na porta 5000");

            TcpClient cliente = servidor.AcceptTcpClient();
            Console.WriteLine("Cliente conectado");

            NetworkStream stream = cliente.GetStream();

            while (true)
            {
                byte[] bufferRecebimento = new byte[1023];

                int bytesLidos = stream.Read(bufferRecebimento, 0, bufferRecebimento.Length);

                string mensagemRecebida = Encoding.UTF8.GetString(bufferRecebimento, 0, bytesLidos);

                Console.WriteLine("Cliente: "+ mensagemRecebida);

                Console.WriteLine("Digite sua mensagem: ");
                string mensagemEnvio = Console.ReadLine();

                byte[] bufferEnvio = Encoding.UTF8.GetBytes(mensagemEnvio);

                stream.Write(bufferEnvio, 0, bytesLidos);
            }
        }
    }
}
