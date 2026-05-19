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

namespace ServidorIluminacao
{
    internal class Program
    {

        static Dictionary<string, string> estadoLuzes = new Dictionary<string, string>();

        static void Main(string[] args)
        {




            
            TcpListener servidor = new TcpListener(IPAddress.Any, 5005);
            servidor.Start();
            Console.WriteLine("Servidor de iluminação rodando na porta 5005");
            EstadoLuzes estado = Xml.LerXml();
            estadoLuzes["Sala"] = estado.Sala;
            estadoLuzes["Cozinha"] = estado.Cozinha;
            estadoLuzes["Quarto"] = estado.Quarto;

            while (true)
            {
               
                
            TcpClient cliente = servidor.AcceptTcpClient();
                NetworkStream stream = cliente.GetStream();
                while (true)
                {
                    

                    byte[] buffer = new byte[1024];
                    int bytesLidos = stream.Read(buffer, 0, buffer.Length);

                    if (bytesLidos == 0) break;

                    string jsonRecebido = Encoding.UTF8.GetString(buffer, 0, bytesLidos).Trim();


                    var comando = JsonSerializer.Deserialize<ComandoIluminacao>(jsonRecebido);
                    /*
                    if (comando.Acao == "status")
                    {
                        byte[] respostaStatus = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(estadoLuzes));
                        stream.Write(respostaStatus, 0, respostaStatus.Length);
                        continue;
                    }
                    */
                    if (comando.Acao == "ligar" || comando.Acao == "desligar")
                    {

                        var chave = estadoLuzes.Keys.FirstOrDefault(k => k.Equals(comando.Luz, StringComparison.OrdinalIgnoreCase));

                        if (chave != null)
                        {
                            estadoLuzes[chave] = comando.Acao == "ligar" ? "ligada" : "desligada";
                        }

                        Xml.SalvarEmXml(new EstadoLuzes
                        {

                            Sala = estadoLuzes["Sala"],
                            Cozinha = estadoLuzes["Cozinha"],
                            Quarto = estadoLuzes["Quarto"]
                        });
                    }
                    Console.WriteLine(JsonSerializer.Serialize(estadoLuzes, new JsonSerializerOptions { WriteIndented = true }));





                    byte[] resposta = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(estadoLuzes));
                    stream.Write(resposta, 0, resposta.Length);

                    




                }





            }



            
        }
    }
}

class ComandoIluminacao
{
    public string Acao { get; set; }
    public string Luz { get; set; }
}

public class EstadoLuzes
{
    public string Sala {  get; set; }
    public string Cozinha { get; set; }
    public string Quarto {  get; set; }
}

public class Xml
{
    private static string caminho = "estado.xml";
    public static void SalvarEmXml(EstadoLuzes estado)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(EstadoLuzes));

        using (FileStream fs = new FileStream(caminho, FileMode.Create))
        {
            serializer.Serialize(fs, estado);
        }

    }
    public static EstadoLuzes LerXml()
    {
        if (!File.Exists(caminho))
        {
            return new EstadoLuzes
            {
                Sala = "desligado",
                Cozinha = "desligado",
                Quarto = "desligado"
            };
        }
        XmlSerializer serializer = new XmlSerializer(typeof(EstadoLuzes));
            using (FileStream fs = new FileStream(caminho, FileMode.Open))
                
            {
                return (EstadoLuzes)serializer.Deserialize(fs);
            }
        
    }
}