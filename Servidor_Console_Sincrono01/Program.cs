using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

class Servidor_Console_Sincrono
{
    static void Main()
    {
        TcpListener servidor = new TcpListener(IPAddress.Any, 5000);
        servidor.Start();
        Console.WriteLine("Servidor escutando na porta 5000...");

        TcpClient cliente = servidor.AcceptTcpClient();
        Console.WriteLine("Cliente conectado!");
        
        NetworkStream stream = cliente.GetStream();

        Thread tReceber = new Thread(() =>
        {
            while (true)
            {
                // Recebe mensagem do cliente
                byte[] bufferRecebimento = new byte[1024];

                int bytesLidos = stream.Read(bufferRecebimento, 0, bufferRecebimento.Length);

                string mensagemRecebida = Encoding.UTF8.GetString(bufferRecebimento, 0, bytesLidos);

                Console.WriteLine("Cliente: " + mensagemRecebida);

            }
        });

        Thread tEnviar = new Thread(() =>
        {
            while (true)
            {

                Console.Write("Digite sua mensagem: ");
                string mensagemEnvio = Console.ReadLine();

                byte[] bufferEnvio = Encoding.UTF8.GetBytes(mensagemEnvio);

                stream.Write(bufferEnvio, 0, bufferEnvio.Length);
            }
        });

        tReceber.Start();
        tEnviar.Start();
    }
}