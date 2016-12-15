using System;
using NLog;

namespace MVCLearn.Log
{
    /// <summary>
    /// 日志记录.
    /// </summary>
    public static class MyLog
    {
        /// <summary>
        /// 错误记录器
        /// </summary>
        private static readonly Logger ErrorLogger = LogManager.GetLogger("Error_Logger");

        /// <summary>
        /// 记录错误日志.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public static void Error(Exception exception)
        {
            LogEventInfo eventInfo = LogEventInfo.Create(LogLevel.Error, string.Empty, string.Empty);
            eventInfo.Properties["Messages"] = exception.ToString();
            ErrorLogger.Log(eventInfo);
        }
    }
}