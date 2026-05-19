using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net;
using System.Net.Sockets;
using System.Xml.Serialization;
using System.IO;

namespace ClienteIluminacao
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TcpClient cliente = new TcpClient("127.0.0.1", 5005);
            NetworkStream stream = cliente.GetStream();

            while(true)
            {
                Console.Write("Luz (sala, cozinha, quarto: ");
                string luz = Console.ReadLine().ToLower();

                if (luz == "sair") break;
                
                string acao = "";
                
                if (luz == "status")
                {
                    acao = "status";
                    luz = "";
                }
                
                else
                {
                    Console.WriteLine("Ação (ligar/desligar: ");
                    acao = Console.ReadLine().ToLower();
                }
                
                var comando = new ComandoIluminacao { Acao = acao , Luz=luz};

                byte[] bufferEnvio = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(comando));
                stream.Write(bufferEnvio, 0, bufferEnvio.Length);

                byte[] bufferRecebimento = new byte[1024];
                int bytesLidos = stream.Read(bufferRecebimento, 0,bufferRecebimento.Length);

                Console.WriteLine("Estado das luzes: " + Encoding.UTF8.GetString(bufferRecebimento, 0, bytesLidos));




            }
            cliente.Close();





        }
    }
}

class ComandoIluminacao
{
    public string Acao { get; set; }  
    public string Luz { get; set; }
}
