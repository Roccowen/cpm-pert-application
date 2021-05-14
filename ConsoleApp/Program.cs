using System;
using System.Collections.Generic;
using QSBMODLibrary.Classes;
using System.Diagnostics;

namespace ConsoleApp
{
    class Program
    {
        static void TestSimplex(double[,] table, double[] result)
        {
            Console.WriteLine("\nВход:");
            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                    Console.Write(String.Format("{0, -6} ", Math.Round(table[i, j], 3)));
                Console.WriteLine();
            }

            double[,] table_result;
            Simplex S = new Simplex(table);
            table_result = S.Calculate(result);

            Console.WriteLine("\nРешенная симплекс-таблица:");
            for (int i = 0; i < table_result.GetLength(0); i++)
            {
                for (int j = 0; j < table_result.GetLength(1); j++)
                    Console.Write(String.Format("{0, -6} ", Math.Round(table_result[i, j], 3)));
                Console.WriteLine();
            }
            int n = 1;
            foreach (var x in result)
                Console.WriteLine($"X[{n++}] = {result[0]}");
        }
        static void Test(List<Work> works)
        {
            static string isKrit(float x, float y) => x == y ? "Крит." : x.ToString();
            var n = 0;
            var timer = new Stopwatch();
            var graph = new EventGraph();
            timer.Start();
            foreach (var work in works)
                graph.AddWork(work);
            var analyzer = new EventGraphAnalyzer(graph);         
            Console.WriteLine($"  КП продолжительность : {analyzer.Duration}");
            Console.WriteLine($"  КП стоимость : {analyzer.Cost}");
            Console.Write("  КП : ");
            foreach (var w in analyzer.CriticalWorks)
                Console.Write($"{w.Title} ");            
            Console.Write(String.Format("\n\n  {0, -5} {1, -10} {2, -7} {3, -7} {4, -7} {5, -7} {6, -5} {7, -5} {8, -5} {9, -5} {10, -6} {11, -13} {12, -7}\n\n",
                                                "№", "Название", "t", "c", "T_min", "C_max", "РН", "ПН", "РО", "ПО", "Резерв", "Напряженность", "tgA"));
            foreach (var w in analyzer.Works)
                Console.Write(String.Format("  {0, -5} {1, -10} {2, -7} {3, -7} {4, -7} {5, -7} {6, -5} {7, -5} {8, -5} {9, -5} {10, -6} {11, -13} {12, -7}\n",
                                               ++n, w.Title, Math.Round(w.Duration, 2), Math.Round(w.Resources, 2), Math.Round(w.DurationMin, 2), Math.Round(w.ResourcesMax, 2), w.ES, w.LS, w.EE, w.LE, isKrit(w.FR, 0), isKrit((float)Math.Round(w.K, 5), 1), Math.Round(w.tgA, 5)));
            analyzer.FullOptimize();
            Console.Write("\n\n  После оптимизации:\n\n");
            Console.WriteLine($"  КП продолжительность : {analyzer.Duration}");
            Console.WriteLine($"  КП стоимость : {analyzer.Cost}");
            foreach (var w in analyzer.Works)
                Console.Write(String.Format("  {0, -5} {1, -10} {2, -7} {3, -7} {4, -7} {5, -7} {6, -5} {7, -5} {8, -5} {9, -5} {10, -6} {11, -13} {12, -7}\n",
                                               ++n, w.Title, Math.Round(w.Duration, 2), Math.Round(w.Resources, 2), Math.Round(w.DurationMin, 2), Math.Round(w.ResourcesMax, 2), w.ES, w.LS, w.EE, w.LE, isKrit(w.FR, 0), isKrit((float)Math.Round(w.K, 5), 1), Math.Round(w.tgA, 5)));
            Console.WriteLine($"  время : {timer.ElapsedMilliseconds / 1000f} s");
            Console.WriteLine($"  память : {Process.GetCurrentProcess().PrivateMemorySize64 / 1024} kB");
        }
        static void Main(string[] args)
        {
            //var test1 = new List<Work> {
            //    new Work("1-2", 3,  5,    23,    26.2f, "1", "2"),
            //    new Work("1-3", 1,  2,    9.8f,  12,    "1", "3"),
            //    new Work("1-4", 6.5f,  8, 22.5f, 27,    "1", "4"),
            //    new Work("2-5", 3.5f,  5, 21.3f, 24,    "2", "5"),
            //    new Work("3-4", 6,  9,    39.7f, 42.3f, "3", "4"),
            //    new Work("3-5", 4,  7,    9.8f,  13.6f, "3", "5"),
            //    new Work("4-5", 7.5f, 11, 46.8f, 51.7f, "4", "5"),
            //    new Work("4-6", 5,  6,    17.2f, 19,    "4", "6"),
            //    new Work("5-6", 5,  7,    34.6f, 41.1f, "5", "6")};
            //Test(test1);
            for (int i = 0; i < 9; i++)
            {
                switch (i)
                {
                    case 0:
                        Console.WriteLine(i);
                        break;
                    case 1:
                        Console.WriteLine(i);
                        break;
                    case 2:
                        Console.WriteLine(i);
                        break;
                    case 5:
                        Console.WriteLine(i);
                        break;
                    default:
                        break;
                }
            }

            //double[,] testSimp1 = {
            //                    {25, -3,  5},
            //                    {30, -2,  5},
            //                    {10,  1,  0},
            //                    { 6,  3, -8},
            //                    { 0, -6, -5}
            //};
            //double[] testSimpRes1 = new double[2];
            //TestSimplex(testSimp1, testSimpRes1);             
        }
    }
}
