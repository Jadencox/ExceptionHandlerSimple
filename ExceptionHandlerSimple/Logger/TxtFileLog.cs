using System.Text;

namespace ExceptionHandlerSimple.Logger
{
    public class TxtFileLog : ILoggerHandler
    {
        private static readonly object Locko = new object();

        public static string LogFile => DateTime.Now.ToString("yyyy年MM月dd日");


        public TxtFileLog()
        {
            RenameLogFile();
        }

        public void LogError(string sLogInfo)
        {
            LogInfo(sLogInfo, 0, LogType.ERROR);
        }

        public void LogTrace(string sLogInfo)
        {
            LogInfo(sLogInfo, 0, LogType.TRACE);
        }

        public void LogInfo(string sLogInfo)
        {
            LogInfo(sLogInfo, 0, LogType.INFO);
        }

        public void LogInterface(string sLogInfo)
        {
            LogInfo(sLogInfo, 0, LogType.Interface);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sLogInfo"></param>
        /// <param name="logLevel"></param>
        /// <param name="logType"></param>
        /// <param name="loadIP">是否加载IP地址</param>
        public void LogInfo(string sLogInfo, int logLevel, LogType logType)
        {
            //if (logLevel > (int)logType || ConfigHandler.LogLevel > (int)logType)
            //    return;
            try
            {
                var filePathAndName = $"{AppDomain.CurrentDomain.BaseDirectory}AppLog\\{LogFile}.txt";

                string logLevelType;

                switch ((int)logType)
                {
                    case 0:
                        {
                            filePathAndName = $"{AppDomain.CurrentDomain.BaseDirectory}AppLog\\{LogFile}_TRACE.txt";
                            logLevelType = "Trace";
                            break;
                        }
                    case 1:
                        {
                            logLevelType = "Info";
                            break;
                        }
                    case 2:
                        {
                            logLevelType = "Debug";
                            break;
                        }
                    case 3:
                        {
                            logLevelType = "Warning";
                            break;
                        }
                    case 4:
                        {
                            logLevelType = "Error";
                            break;
                        }
                    case 5:
                        {
                            logLevelType = "Fatal";
                            break;
                        }
                    case 6:
                        {
                            filePathAndName = $"{AppDomain.CurrentDomain.BaseDirectory}AppLog\\{LogFile}_SQL.txt";
                            logLevelType = "Sql";
                            break;
                        }
                    case 7:
                        {
                            filePathAndName = $"{AppDomain.CurrentDomain.BaseDirectory}AppLog\\{LogFile}_Interface.txt";
                            logLevelType = "Interface";
                            break;
                        }
                    default:
                        {
                            logLevelType = "Unknown";
                            break;
                        }
                }
                var sb = new StringBuilder();
                sb.Append(Thread.CurrentThread.ManagedThreadId.ToString().PadLeft(3));
                sb.Append(" ");
                sb.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                sb.Append(" ");
                sb.Append(logLevelType.PadRight(8));
                sb.Append(" ");
                sb.Append(Environment.MachineName);
                sb.Append(" ");
                sb.Append(sLogInfo);
                sb.Append($"\r\n---------------------------------------------------------------------------------------------------------------------------------------------------------------");

                //write to file TecNodeService.log

                lock (Locko)
                {
                    if (!File.Exists(filePathAndName))
                    {
                        RenameLogFile();
                    }
                    using (var fs = File.Open(filePathAndName, FileMode.Append, FileAccess.Write, FileShare.Write))
                    {
                        var logFileSw = new StreamWriter(fs);
                        logFileSw.WriteLine(sb.ToString());
                        logFileSw.Close();
                        fs.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //write to file TecNodeServiceErr.txt
                var errFilePathAndName = $"{AppDomain.CurrentDomain.BaseDirectory}AppLog\\{LogFile}.txt";

                try
                {
                    var fs = File.Open(errFilePathAndName, FileMode.Append, FileAccess.Write, FileShare.Write);
                    var logFileSw = new StreamWriter(fs);
                    logFileSw.WriteLine(ex.Message);
                    logFileSw.Close();
                    fs.Close();
                }
                catch
                {
                    // ignored
                }
            }
        }

        private static void RenameLogFile()
        {
            var baseDirPath = AppDomain.CurrentDomain.BaseDirectory;
            var logPath = baseDirPath + "AppLog";
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }
            var deleteTime = DateTime.Now.AddDays(-15);
            var dir = new DirectoryInfo(logPath);
            foreach (var file in dir.GetFiles())
            {
                //默认删除半个月前的日志
                if (file.CreationTime <= deleteTime)
                {
                    file.Delete();
                }
                //默认删除文件大小大于200MB的日志
                if (file.Length > 200 * 1024 * 1024)
                {
                    file.Delete();
                }
            }
        }
    }
}
