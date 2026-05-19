using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Atividade2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Endereco> enderecos = new List<Endereco>
            {
                new Endereco { Tipo = "Residencial", Rua = "Rua A", Numero = 123 },
                new Endereco { Tipo = "Comercial", Rua = "Rua B", Numero = 456 }
            };

            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(enderecos, options);
            Console.WriteLine("Jason  Serializado: \n" + json);

            List<Endereco> enderecosDesserializados = JsonSerializer.Deserialize<List<Endereco>>(json);
            
            Console.WriteLine("\nEndereço Desserializados: ");

            foreach(var endereco in enderecosDesserializados)
            {
                Console.WriteLine($"Tipo: {endereco.Tipo}, Rua: {endereco.Rua}, Numero: {endereco.Numero}");
            }




            Console.ReadKey();
        }
    }
}


class Endereco
{
    public string Tipo { get; set; }
    public string Rua { get; set;}
    public int Numero { get; set; }
}

