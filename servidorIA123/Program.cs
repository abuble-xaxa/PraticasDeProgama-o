using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class Servidor
{
    static async Task Main()
    {
        TcpListener servidor =
            new TcpListener(IPAddress.Any, 5000);

        servidor.Start();

        Console.WriteLine("Servidor iniciado.");

        TcpClient cliente =
            await servidor.AcceptTcpClientAsync();

        Console.WriteLine("Cliente conectado.");

        NetworkStream stream =
            cliente.GetStream();

        byte[] mensagemInicial =
            Encoding.UTF8.GetBytes("Olá IA");

        await stream.WriteAsync(
            mensagemInicial,
            0,
            mensagemInicial.Length
        );

        byte[] buffer = new byte[1024];

        while (true)
        {
            int bytes =
                await stream.ReadAsync(
                    buffer,
                    0,
                    buffer.Length
                );

            if (bytes == 0)
                break;

            string resposta =
                Encoding.UTF8.GetString(
                    buffer,
                    0,
                    bytes
                );

            Console.WriteLine($"IA respondeu: {resposta}");
        }
    }
}