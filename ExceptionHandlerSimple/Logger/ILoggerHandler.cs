namespace ExceptionHandlerSimple.Logger
{
    public interface ILoggerHandler
    {
        void LogError(string sLogInfo);
        void LogTrace(string sLogInfo);
        void LogInfo(string sLogInfo);
        void LogInfo(string sLogInfo, int logLevel, LogType logType);
    }
}
