using ACUS.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ACUS.Logging
{
    class Logger
    {
        private static string acusDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ACUS");
        public static void Log(string error)
        {
            if (!Directory.Exists(acusDir))
                Directory.CreateDirectory(acusDir);

            var filePath = Path.Combine(acusDir, "Error.txt");
            //filePath += "/Error.txt";
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            
            File.AppendAllText(filePath, error);
        }

        public static void LogCSV(List<string> data)
        {
            if (!Directory.Exists(acusDir))
                Directory.CreateDirectory(acusDir);

            var resDir = Path.Combine(acusDir, "Results");
            if (!Directory.Exists(resDir))
                Directory.CreateDirectory(resDir);

            var filePath = Path.Combine(resDir, "Measurements_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv");
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            File.WriteAllLines(filePath, data);
            ACUSConstants.ResultFilePath = filePath;
        }
    }
}
