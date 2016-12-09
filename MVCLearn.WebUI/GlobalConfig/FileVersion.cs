using System;

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
                return "201612092124";
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
                return "201612092124";
#endif
            }
        }
    }
}