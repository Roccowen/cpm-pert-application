using System;
using System.IO;

namespace QSBMODLibrary.Classes
{
    public static class Loger
    {
        public static void Msg(string msg)
        {
            if (!Directory.Exists("Logs\\"))
                Directory.CreateDirectory("Logs");
            var today = DateTime.Now;
            var file = String.Format("Logs\\{0,-2:D2}-{0,-2:D2}-log.txt", today.Day, today.Month);
            using (StreamWriter sw = File.AppendText(file))
            {
                today = DateTime.Now;
                var str = String.Format("{0,-2:D2}:{1,-2:D2}:{2,-2:D2}:{3,-4:D4}\t{4}",
                    today.Hour, today.Minute, today.Second, today.Millisecond, msg);
                sw.WriteLine(str);
                Console.WriteLine(str);
            }
        }
        public static void Msg(Exception ex)
        {
            if (!Directory.Exists("Logs\\"))
                Directory.CreateDirectory("Logs");
            var today = DateTime.Now;
            var file = String.Format("Logs\\{0,-2:D2}-{0,-2:D2}-log.txt", today.Day, today.Month);
            using (StreamWriter sw = File.AppendText(file))
            {
                today = DateTime.Now;
                var str = String.Format("{0,-2:D2}:{1,-2:D2}:{2,-2:D2}:{3,-4:D4}\tEXCEPTION : {4} MESSAGE: {5}",
                    today.Hour, today.Minute, today.Second, today.Millisecond, ex.GetType(), ex.Message);
                sw.WriteLine(str);
                Console.WriteLine(str);
            }
        }
    }
}
