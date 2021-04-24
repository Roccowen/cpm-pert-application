using System;
using System.Collections.Generic;
using WFApp.Classes;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var worksGraph = new EventGraph();
            worksGraph.AddWork(new Work(1, 3, 3, 3, 3, 3, folowingWorks: new List<uint> { 2, 3, 4 }));
            worksGraph.AddWork(new Work(2, 5, 5, 5, 5, 5, folowingWorks: new List<uint> { 2, 3, 4 }));
            worksGraph.AddWork(new Work(3, 2, 2, 2, 2, 2, folowingWorks: new List<uint> { 2, 3, 4 }));
            worksGraph.AddWork(new Work(4, 8, 8, 8, 5, 5, folowingWorks: new List<uint> { 2, 3, 4 }));
            worksGraph.AddWork(new Work(5, 5, 5, 5, 5, 5, folowingWorks: new List<uint> { 2, 3, 4 }));
            worksGraph.AddWork(new Work(6, 5, 5, 5, 5, 5, folowingWorks: new List<uint> { 2, 3, 4 }));
        }
    }
}
