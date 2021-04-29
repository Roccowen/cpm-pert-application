using System;
using System.Collections.Generic;
using WFApp.Classes;
using System.Diagnostics;

namespace ConsoleApp
{
    class Program
    {
        static void TestGraph(List<Work> works)
        {
            
            var n = 0;
            var timer = new Stopwatch();
            var graph = new EventGraph();
            timer.Start();
            foreach (var work in works)
                graph.AddWork(work);
            var cp = graph.FindCriticalPath();

            Console.WriteLine($"время : {timer.ElapsedMilliseconds / 1000f} s");
            Console.WriteLine($"память : {Process.GetCurrentProcess().PrivateMemorySize64 / 1024} kB");
            Console.WriteLine($"КП продолжительность : {cp[cp.Count - 1].ES}");
            Console.WriteLine($"КП стоимость : {graph.GetCost()}");
            Console.Write("CP : ");
            cp.ForEach(c => Console.Write($"{c.Title} "));
            Console.Write(String.Format("\n\n  {0, -5} {1, -10} {2, -5} {3, -5} {4, -5} {5, -5} {6, -5} {7, -5}\n\n",
                                                "№", "Название", "РН", "ПН", "РО", "ПО", "Рез", "Напр"));
            foreach (var w in graph.OrderedProjectWorks)
                Console.Write(String.Format("  {0, -5} {1, -10} {2, -5} {3, -5} {4, -5} {5, -5} {6, -5} {7, -5}\n",
                                                ++n, w.Title, w.ES, w.LS, w.EE, w.LE, w.FR, w.K));
        }
        static void Main(string[] args)
        {
            var test1 = new List<Work> { 
                new Work("1-2", 5f, 3f, 23f, 26.2f, "1", "2"),
                new Work("1-3", 2f,  1f,    9.8f,  12f, "1", "3"),
                new Work("1-4", 8f,  6.5f,   22.5f, 27f, "1", "4"),
                new Work("2-5", 5f,  3.5f,   21.3f, 24f, "2", "5"),
                new Work("3-4", 9f,  6f,    39.7f, 42.3f, "3", "4"),
                new Work("3-5", 7f,  4f,    9.8f,  13.6f, "3", "5"),
                new Work("4-5", 11f, 7.5f,  46.8f, 51.7f, "4", "5"),
                new Work("4-6", 6f,  5f,    17.2f, 19f, "4", "6"),
                new Work("5-6", 7f,  5f,   34.6f, 41.1f, "5", "6")};
            TestGraph(test1);
        }
    }
}
