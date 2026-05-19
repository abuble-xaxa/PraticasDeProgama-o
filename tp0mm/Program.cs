using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Runtime;
using System.Runtime.InteropServices;


namespace tp0mm
{
    internal class Program
    {
        [DllImport("kernel32.dll")]
        static extern uint GetCurrentProcessorNumber();

        static void Main(string[] args)
        {
            double r1, r2, r3, r4;

            Console.WriteLine("Execução sequencial");

            var sw1 = Stopwatch.StartNew();

            r1 = CalcularRaizes();
            r2 = CalcularPotencias();
            r3 = CalcularSenos();
            r4 = CalcularLogaritmos();

            sw1.Stop();

            Console.WriteLine($"Tempo sequencial: {sw1.ElapsedMilliseconds} ms");


            Console.WriteLine("\nExecução paralela");

            var sw2 = Stopwatch.StartNew();

            Parallel.Invoke(
                () => r1 = CalcularRaizes(),
                () => r2 = CalcularPotencias(),
                () => r3 = CalcularSenos(),
                () => r4 = CalcularLogaritmos()
            );

            sw2.Stop();

            Console.WriteLine($"Tempo paralela: {sw2.ElapsedMilliseconds} ms");


            Console.WriteLine("\nParalela limitada a 1 Nucleo");

            Process.GetCurrentProcess().ProcessorAffinity = (IntPtr)1;

            var sw3 = Stopwatch.StartNew();

            Parallel.Invoke(
                () => r1 = CalcularRaizes(),
                () => r2 = CalcularPotencias(),
                () => r3 = CalcularSenos(),
                () => r4 = CalcularLogaritmos()
            );

            sw3.Stop();

            Console.WriteLine($"Tempo paralela (1 núcleo): {sw3.ElapsedMilliseconds} ms");

            Console.ReadLine();
        }

        static double CalcularRaizes()
        {
            Console.WriteLine($"Raizes no núcleo {GetCurrentProcessorNumber()}");

            double resultado = 0;

            for (long i = 1; i <= 80_000_000; i++)
            {
                resultado += Math.Sqrt(i);
            }

            return resultado;
        }

        static double CalcularPotencias()
        {
            Console.WriteLine($"Potencias no núcleo {GetCurrentProcessorNumber()}");

            double resultado = 0;

            for (long i = 1; i <= 40_000_000; i++)
            {
                resultado += Math.Pow(i, 0.25);
            }

            return resultado;
        }

        static double CalcularSenos()
        {
            Console.WriteLine($"Senos no núcleo {GetCurrentProcessorNumber()}");

            double resultado = 0;

            for (long i = 1; i <= 60_000_000; i++)
            {
                resultado += Math.Sin(i);
            }

            return resultado;
        }

        static double CalcularLogaritmos()
        {
            Console.WriteLine($"Logaritmos no núcleo {GetCurrentProcessorNumber()}");

            double resultado = 0;

            for (long i = 1; i <= 70_000_000; i++)
            {
                resultado += Math.Log(i);
            }

            return resultado;
        }
    }
}



/*
        [DllImport("kernel32.dll")]
        static extern uint GetCurrentProcessorNumber();

        static void Main(string[] args)
        {
            Process.GetCurrentProcess().ProcessorAffinity = (IntPtr)1;

            Console.WriteLine("Início da execução");
            Console.WriteLine(
                $"Main iniciou no nucleo{GetCurrentProcessorNumber()}\n"
                );
            Console.WriteLine("Execução sequencial");

            var sw1 = Stopwatch.StartNew();

            Tarefa1();
            Tarefa2();
            sw1.Stop();
            Console.WriteLine($"\nTempo total (sequencial): {sw1.ElapsedMilliseconds} ms");
            Console.WriteLine("Execução paralela");

            var sw2 = Stopwatch.StartNew();
            Parallel.Invoke(
                () => Tarefa1(),
                () => Tarefa2()
                ) ;

            sw2.Stop();

            Console.WriteLine($"\nTempo total (paralela): {sw2.ElapsedMilliseconds} ms");
            Console.ReadLine();

           



        }

        static void Tarefa1()
        {
            Console.WriteLine(
               $"\n[Tarefa1] iniciou no nucleo {GetCurrentProcessorNumber()}"
               );

            Thread.Sleep(2000);
            Console.WriteLine(
              $"\n[Tarefa1] terminou no nucleo {GetCurrentProcessorNumber()}"
              );


        }
        static void Tarefa2()
        {
            Console.WriteLine(
               $"\n[Tarefa2] iniciou no nucleo {GetCurrentProcessorNumber()}"
               );
            Thread.Sleep(2000);
            Console.WriteLine(
              $"\n[Tarefa2] terminou no nucleo {GetCurrentProcessorNumber()}"
              );

        }

        
*/
