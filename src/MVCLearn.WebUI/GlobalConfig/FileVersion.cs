using System;
using System.Configuration;

namespace MVCLearn.WebUI.GlobalConfig
{
    /// <summary>
    /// 文件版本
    /// </summary>
    public static class FileVersion
    {
        public static string JS
        {
            get
            {
#if DEBUG
                return DateTime.Now.Ticks.ToString();
#else
                return ConfigurationManager.AppSettings["JSVersion"];
#endif
            }
        }

        public static string CSS
        {
            get
            {
#if DEBUG
                return DateTime.Now.Ticks.ToString();
#else
                return ConfigurationManager.AppSettings["CSSVersion"];
#endif
            }
        }
    }
}