using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

class Cliente_Console_Sincrono
{
    static void Main()
    {
        TcpClient cliente = new TcpClient("127.0.0.1", 5000);
        Console.WriteLine("Conectado ao servidor!");
        NetworkStream stream = cliente.GetStream();

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

        Thread tReceber = new Thread(() =>
{
    while (true)
    {


        byte[] bufferRecebimento = new byte[1024];
        Thread.Sleep(1000);

        int bytesLidos = stream.Read(bufferRecebimento, 0, bufferRecebimento.Length);
        string mensagemRecebida = Encoding.UTF8.GetString(bufferRecebimento, 0, bytesLidos);
        Console.WriteLine("Servidor: " + mensagemRecebida);
        }
    });
        tEnviar.Start();
        tReceber.Start();

    }
}