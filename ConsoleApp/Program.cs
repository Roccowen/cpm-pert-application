using System;
using System.Collections.Generic;
using WFApp.Classes;
using System.Diagnostics;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var timer = new Stopwatch();
            var graph = new EventGraph();
            timer.Start();
            graph.AddWork(new Work("1-2", 5f, 3f, 8f, 23f, 26.2f, "1", "2"));
            graph.AddWork(new Work("1-3", 2f, 1f, 4f, 9.8f, 12f, "1", "3"));
            graph.AddWork(new Work("1-4", 8f, 6.5f, 10f, 22.5f, 27f, "1", "4"));
            graph.AddWork(new Work("2-5", 5f, 3.5f, 8f, 21.3f, 24f, "2", "5"));
            graph.AddWork(new Work("3-4", 9f, 6f, 13f, 39.7f, 42.3f, "3", "4"));
            graph.AddWork(new Work("3-5", 7f, 4f, 10.5f, 9.8f, 13.6f, "3", "5"));
            graph.AddWork(new Work("4-5", 11f, 7.5f, 15f, 46.8f, 51.7f, "4", "5"));
            graph.AddWork(new Work("4-6", 6f, 5f, 8f, 17.2f, 19f, "4", "6"));
            graph.AddWork(new Work("5-6", 7f, 5f, 12f, 34.6f, 41.1f, "5", "6"));
            var v = graph.FindCriticalPath();
            timer.Stop();
            foreach (var e in v)
                Console.Write($"{e.Title}, ");
            Console.WriteLine(v[v.Count - 1].FollowingEvent.ES);
            Console.WriteLine(timer.ElapsedMilliseconds / 1000f);
        }
    }
}
