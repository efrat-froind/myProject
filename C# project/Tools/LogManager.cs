

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Tools {
    public static class LogManager
    {
        private static string logDirPath = "Log";
        private static string logSubDir = @$"{logDirPath}\{DateTime.Now.Year}";

        public static string GetMonthFileName()
        {
            string dirPath = @$"{logDirPath}\{DateTime.Now.Year}\{DateTime.Now.Month}";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            return dirPath;
        }

        public static string GetTodayFileName()
        {
            string filePath = @$"{GetMonthFileName()}\{DateTime.Now.Day}.txt";
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            return filePath;
        }

        public static void WriteToLog(string Project, string funcName, string message)
        {
            using (StreamWriter sw = new StreamWriter(GetTodayFileName(), true))
            {
                sw.WriteLine($"{DateTime.Now}\t{Project}\t{funcName}\t{message}");
            }
        }

        public static void Remove()
        {
            DateTime cutoffDate = DateTime.Now.AddMonths(-2);
            string[] subDirectorys = Directory.GetDirectories(logSubDir);
            foreach (string subDirectory in subDirectorys)
            {
                DateTime creationTime = Directory.GetCreationTime(subDirectory);
                if (creationTime.Month< cutoffDate.Month)
                {
                    Directory.Delete(subDirectory, true);
                }
            }
        }


    }
}

