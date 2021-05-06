using System;
using System.IO;
using System.Text;

namespace QSBMODLibrary.Classes
{
    public static class EventGraphReader
    {
        public static EventGraph ReadFromCSV(string path)
        {
            string dir;
            if (path.LastIndexOf('\\') == -1)
                dir = path.Substring(0, path.LastIndexOf('/'));
            else
                dir = path.Substring(0, path.LastIndexOf('\\'));
            if (!Directory.Exists(dir))
                throw new DirectoryNotFoundException();
            var eventGraph = new EventGraph();
            const Int32 BufferSize = 128;
            using (var fileStream = File.OpenRead(path))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                streamReader.ReadLine();
                String line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var rawWork = line.Split(';');
                    try
                    {
                        eventGraph.AddWork(new Work(rawWork[0], float.Parse(rawWork[1]), float.Parse(rawWork[2]), float.Parse(rawWork[3]), 
                                        float.Parse(rawWork[4]), float.Parse(rawWork[5]), float.Parse(rawWork[6]), rawWork[7], rawWork[8]));
                    }
                    catch (Exception)
                    {

                    }                   
                }
            }
            return eventGraph;
        }
        public static void SaveToCSV(EventGraph graph, string path)
        {
            string dir;
            if (path.LastIndexOf('\\') == -1)
                dir = path.Substring(0, path.LastIndexOf('/'));
            else
                dir = path.Substring(0, path.LastIndexOf('\\'));  
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            if (!File.Exists(path))
                File.CreateText(path);
            using StreamWriter sw = new StreamWriter(path);
            sw.WriteLine("Title;t;T_min;T_max;c;C_min;C_max;FirstEventTitle;SecondEventTitle;");
            foreach (var work in graph.WorksByTitle.Values)
                sw.WriteLine($"{work.Title};{work.Duration};{work.DurationMin};{work.DurationMax};" +
                    $"{work.Resources};{work.ResourcesMin};{work.ResourcesMax};{work.FirstEventTitle};{work.SecondEventTitle};");
        }
    }
}
