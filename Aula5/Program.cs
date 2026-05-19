using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace Aula5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TcpListener servidor = new TcpListener(IPAddress.Any, 5000);
            servidor.Start();
            Console.WriteLine("Servidor rodando na porta 5000");

            TcpClient cliente = servidor.AcceptTcpClient();

            Console.WriteLine("Cliente conectado");

            NetworkStream stream = cliente.GetStream();
            
            while (true)
            {
                byte[] bufferRecebimento = new byte[1024];
                int bytesLidos = stream.Read(bufferRecebimento, 0, bufferRecebimento.Length);
                string mensagemRecebida = Encoding.UTF8.GetString(bufferRecebimento, 0 , bytesLidos);
                byte[] bufferDeEnvio;

                Console.WriteLine("Cliente: "+ mensagemRecebida);
                //envia msg pro cliente

                Console.Write("Digite sua mensagem: ");

                string rServidor;


                if (mensagemRecebida.Equals("@data", StringComparison.OrdinalIgnoreCase))
                {
                    mensagemRecebida = "Data Atual: " + DateTime.Now.ToString("dd-mm-yyyy");
                }
                else if (mensagemRecebida.Equals("@hora", StringComparison.OrdinalIgnoreCase))
                {
                    mensagemRecebida = "Hora Atual: " + DateTime.Now.ToString("HH:mm:ss");
                }



                bufferDeEnvio = Encoding.UTF8.GetBytes(mensagemRecebida);
                stream.Write(bufferDeEnvio, 0, bufferDeEnvio.Length);
                rServidor = Console.ReadLine();

                bufferDeEnvio = Encoding.UTF8.GetBytes(rServidor);
                stream.Write(bufferDeEnvio, 0, bufferDeEnvio.Length);







            }
            



        }
    }
}
