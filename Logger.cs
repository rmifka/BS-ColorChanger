using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace FirstWpfAPPColorPicker
{
    public static class Logger
    {
        public static void WriteLog(string message)
        {
            string logPath = "Logs/log.log";
            if(!File.Exists(logPath))
            {
                Directory.CreateDirectory("Data");
                File.Create(logPath);
            }
            File.AppendAllText(logPath,$"{message} {Environment.NewLine}");
            
        }
    }
}
