using System.Collections.Generic;
using NLog;
using NLog.Layouts;

namespace MVCLearn.WebUI.Filter
{
    public class Logging
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 记录操作记录
        /// </summary>
        public static void OperationRecord(Dictionary<string, SimpleLayout> data)
        {
            _logger.Info("");
        }

        /// <summary>
        /// 添加变量
        /// </summary>
        private static void AddVariables()
        {

        }

        /// <summary>
        /// 删除变量
        /// </summary>
        private static void RemoveVariables()
        {

        }
    }
}