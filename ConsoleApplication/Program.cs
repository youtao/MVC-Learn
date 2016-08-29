using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using NLog;
using NLog.Config;
using NLog.Layouts;

namespace ConsoleApplication
{
    class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {            
            ConfigurationItemFactory.Default.Targets.RegisterDefinition("hello-world", typeof(HelloWorldLayoutRenderer));
            LogManager.Configuration.Variables["name"] = "youtao";
            //for (int i = 0; i < 100; i++)
            //{                
            //    _logger.Trace("Sample trace message");
            //    _logger.Debug("Sample debug message");
            //    _logger.Info("Sample informational message");
            //    _logger.Warn("Sample warning message");
            //    _logger.Error("Sample error message");
            //    _logger.Fatal("Sample fatal error message");
            //}
            Logger.Trace("Sample trace message");           
        }
    }
}
