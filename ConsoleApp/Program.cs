using System;
using System.Collections.Generic;
using QSBMODLibrary.Classes;
using System.Diagnostics;

namespace ConsoleApp
{
    class Program
    {
        static void GetSymplexRes(double[,] table, double[] result)
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
            Func<float, float, string> isKrit = (x, y) => x == y ? "Крит." : x.ToString(); 
            var n = 0;
            var timer = new Stopwatch();
            var graph = new EventGraph();
            timer.Start();
            foreach (var work in works)
                graph.AddWork(work);
            var cp = graph.FindCriticalPath();            
            Console.WriteLine($"  время : {timer.ElapsedMilliseconds / 1000f} s");
            Console.WriteLine($"  память : {Process.GetCurrentProcess().PrivateMemorySize64 / 1024} kB");
            Console.WriteLine($"  КП продолжительность : {cp[cp.Count - 1].ES}");
            Console.WriteLine($"  КП стоимость : {graph.Cost}");
            Console.Write("  КП : ");
            cp.ForEach(c => Console.Write($"{c.Title} "));
            Console.Write(String.Format("\n\n  {0, -5} {1, -10} {2, -5} {3, -5} {4, -5} {5, -5} {6, -6} {7, -13} {8, -7}\n\n",
                                                "№", "Название", "РН", "ПН", "РО", "ПО", "Резерв", "Напряженность", "tgA"));
            foreach (var w in graph.OrderedProjectWorks)
                Console.Write(String.Format("  {0, -5} {1, -10} {2, -5} {3, -5} {4, -5} {5, -5} {6, -6} {7, -13} {8, -7}\n",
                                                ++n, w.Title, w.ES, w.LS, w.EE, w.LE, isKrit(w.FR, 0), isKrit((float)Math.Round(w.K, 5), 1), Math.Round(w.tgA, 5)));
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
            //double[,] testSimp1 = {
            //                    {25, -3,  5},
            //                    {30, -2,  5},
            //                    {10,  1,  0},
            //                    { 6,  3, -8},
            //                    { 0, -6, -5}
            //};
            //double[] testSimpRes1 = new double[2];
            //GetSymplexRes(testSimp1, testSimpRes1);
            //double[,] testSimp2 = {
            //                    {0, -1,  0, 0},
            //                    {3.2, 1, 0, 0},
            //                    {2, 0.625, 0, 0},
            //                    {0, -0.625, 0, 0},
            //                    {0, 0, -1, 0},
            //                    {2.2, 0, 1, 0},
            //                    {1, 0, 0.454, 0},
            //                    {0, 0, -0.454, 0},
            //                    {0, 0, 0, -1},
            //                    {4.5, 0, 0, 1 },
            //                    {1.5, 0, 0, 0.333 },
            //                    {0, 0, 0, -0.333 },
            //                    {0, -0.625, 0.454, 0 },
            //                    {0, 0.625, -0.454, 0 },
            //                    {0, 0, -0.454, 0.333 },
            //                    {0, 0, 0.454, -0.333 },
            //                    {0, -0.625, -0.454, -0.333 }
            //};
            //double[] testSimpRes2 = new double[3];
            //GetSymplexRes(testSimp2, testSimpRes2);
            //for (int i = 0; i < testSimp2.GetLength(0); i++)
            //    Console.WriteLine($"{testSimp2[i, 0]} >= {testSimp2[i, 1] * 1.598 + testSimp2[i, 2] * 1.598 + testSimp2[i, 3] * 1.598} " +
            //        $"res : {testSimp2[i, 0] >= (testSimp2[i, 1] * 1.598 + testSimp2[i, 2] * 1.598 + testSimp2[i, 3] * 1.598)}");
            //Console.WriteLine();
            //Console.WriteLine();
            //for (int i = 0; i < testSimp1.GetLength(0); i++)
            //    Console.WriteLine($"{testSimp1[i, 0]} >= {testSimp1[i, 1] * 10 + testSimp1[i, 2] * 10} " +
            //        $"res : {testSimp1[i, 0] >= (testSimp1[i, 1] * 10 + testSimp1[i, 2] * 10)}");          
        }
    }
}
