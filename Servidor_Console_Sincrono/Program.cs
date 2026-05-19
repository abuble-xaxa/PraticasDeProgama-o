using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;

class ServidorIA
{
    static NetworkStream stream;

    static HttpClient client = new HttpClient();

    static void Main()
    {
        TcpListener servidor =
            new TcpListener(IPAddress.Any, 5000);

        servidor.Start();

        Console.WriteLine("Servidor IA iniciado...");

        TcpClient cliente =
            servidor.AcceptTcpClient();

        stream = cliente.GetStream();

        Thread thread =
            new Thread(ReceberMensagens);

        thread.Start();

        while (true)
        {
            Thread.Sleep(1000);
        }
    }

    static void ReceberMensagens()
    {
        byte[] buffer = new byte[1024];

        while (true)
        {
            int bytes = stream.Read(buffer, 0, buffer.Length);

            if (bytes == 0)
                break;

            string pergunta = Encoding.UTF8.GetString(buffer, 0, bytes);

            Console.WriteLine("Cliente: " + pergunta);

            string resposta =
                PerguntarIA(pergunta);

            Console.WriteLine("IA: " + resposta);

            byte[] envio =  Encoding.UTF8.GetBytes(resposta);

            stream.Write(envio, 0, envio.Length);
        }
    }

    static string PerguntarIA(string pergunta)
    {
        client.DefaultRequestHeaders.Clear();

        client.DefaultRequestHeaders.Add(
            "Authorization",
            "Bearer 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"
        );

        var dados = new
        {
            model = "openai/gpt-4o-mini",

            messages = new[]
            {
                new
                {
                    role = "user",
                    content = pergunta
                }
            }
        };

        string json =
            JsonSerializer.Serialize(dados);

        StringContent content =
            new StringContent(
                json,
                Encoding.UTF8,
                "application/json"
            );

        HttpResponseMessage resposta =
            client.PostAsync(
                "https://openrouter.ai/api/v1/chat/completions",
                content
            ).Result;

        string retorno =
            resposta.Content.ReadAsStringAsync().Result;

        JsonDocument doc =
            JsonDocument.Parse(retorno);

        return doc.RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString();
    }
}