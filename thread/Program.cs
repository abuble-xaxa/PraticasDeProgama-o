using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace thread
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Processo principal iniciado");

            Thread t = new Thread(ExecutaTarefaLonga);
            t.Start();

            for (int i=1; i<=3; i++)
            {
                Thread.Sleep(500);
                Console.WriteLine($"Processo principal - etapa{i}");
            }

            t.Join();

            Console.WriteLine("Processo principal concluido");
            Console.ReadKey();

            
        }
        public static void ExecutaTarefaLonga()
        {
            Console.WriteLine("Tarefa longa iniciada");

            for (int a = 1; a <= 3; a++)
            {
                Thread.Sleep(1000);
                Console.WriteLine($"Processo longo - Etapa{a}");
            }
            Console.WriteLine("Processo longo concluido");
        }
    }

    
}
