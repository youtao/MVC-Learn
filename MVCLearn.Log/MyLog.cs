using System;
using NLog;

namespace MVCLearn.Log
{
    public static class MyLog
    {
        private static readonly Logger ErrorLogger = LogManager.GetLogger("Error_Logger");

        public static void Error(Exception exception)
        {
            LogEventInfo eventInfo = LogEventInfo.Create(LogLevel.Error, string.Empty, string.Empty);
            eventInfo.Properties["Messages"] = exception.ToString();
            ErrorLogger.Log(eventInfo);
        }
    }
}