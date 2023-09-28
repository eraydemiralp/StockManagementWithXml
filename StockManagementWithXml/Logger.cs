using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace StockManagementWithXml
{
    public static class Logger
    {
        public static string DefaultLogMessage
        {
            get {
                return "Exception occured at Form: {0} Method:{1} Exception Detail: {2}";
            }
        }
        public static void WriteLog(string message)
        {
            string logPathRaw = ConfigurationManager.AppSettings["logFilePath"];
            string dateStr = DateTime.Now.ToString("ddMMyyyy");
            string logPath = logPathRaw + dateStr + ".txt";
            if (!Directory.Exists(logPathRaw)) {
                Directory.CreateDirectory(logPathRaw);
            }
            using (StreamWriter writer = new StreamWriter(logPath, true)) {
                writer.WriteLine($"{DateTime.Now} : {message}");
            }
        }
    }
}
