using System.Net.Sockets;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using OpenAI.Chat;
using OpenAI;

class ClienteIA
{
    static async Task Main()
    {
        TcpClient cliente = new TcpClient();

        await cliente.ConnectAsync("127.0.0.1", 5000);

        Console.WriteLine("Conectado ao servidor.");

        NetworkStream stream = cliente.GetStream();

        byte[] buffer = new byte[1024];

        while (true)
        {
            int bytesLidos = await stream.ReadAsync(buffer, 0, buffer.Length);

            if (bytesLidos == 0)
                break;

            string mensagemRecebida =
                Encoding.UTF8.GetString(buffer, 0, bytesLidos);

            Console.WriteLine($"Servidor: {mensagemRecebida}");

            string respostaIA = await GerarRespostaIA(mensagemRecebida);

            Console.WriteLine($"IA: {respostaIA}");

            byte[] dados =
                Encoding.UTF8.GetBytes(respostaIA);

            await stream.WriteAsync(dados, 0, dados.Length);
        }

        cliente.Close();
    }

    static async Task<string> GerarRespostaIA(string mensagem)
    {
        HttpClient http = new HttpClient();

        string apiKey = "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";

        http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", apiKey);

        string json =
            "{\"model\":\"gpt-4o-mini\",\"messages\":[{\"role\":\"user\",\"content\":\""
            + mensagem +
            "\"}]}";

        HttpContent content =
            new StringContent(
                json,
                Encoding.UTF8,
                "application/json"
            );

        HttpResponseMessage response =
            await http.PostAsync(
                "https://openrouter.ai/api/v1/chat/completions",
                content
            );

        string resultado =
            await response.Content.ReadAsStringAsync();

        return resultado;
    }

}