using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Model;
using Nelibur.ObjectMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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
            //ConfigurationItemFactory.Default.Targets.RegisterDefinition("hello-world", typeof(HelloWorldLayoutRenderer));
            //LogManager.Configuration.Variables["name"] = "youtao";
            ////for (int i = 0; i < 100; i++)
            ////{
            ////    _logger.Trace("Sample trace message");
            ////    _logger.Debug("Sample debug message");
            ////    _logger.Info("Sample informational message");
            ////    _logger.Warn("Sample warning message");
            ////    _logger.Error("Sample error message");
            ////    _logger.Fatal("Sample fatal error message");
            ////}
            //Logger.Trace("Sample trace message");

            Database.SetInitializer<LearnDbContext>(null);
            using (LearnDbContext db = new LearnDbContext())
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                Mapper.Initialize(e =>
                e.CreateMap<Menu, MenuDto>()
                .ForMember(dto => dto.MenuId, conf => conf.MapFrom(src => src.Id))
                );

                var list = db.Menu.Take(10).ProjectTo<MenuDto>().AsNoTracking().ToList();

                stopwatch.Stop();
                var json = JsonConvert.SerializeObject(list);

                JsonConvert.SerializeObject(list);
                File.WriteAllText("json.json", json);
                Console.WriteLine(stopwatch.ElapsedMilliseconds);
            }
            Console.WriteLine("ok");
            Console.ReadKey();
        }
    }

    public class MenuDto
    {
        [JsonConverter(typeof(StringNumberConverter))]
        public long MenuId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }

        [JsonConverter(typeof(StringDateTimeConverter))]
        public DateTime CreateTime { get; set; }

        [JsonIgnore]
        public long? ParentId { get; set; }

        [JsonProperty("ParentId")]
        public string ParentIdToJson
        {
            get
            {
                var result = string.Empty;
                if (this.ParentId!=null)
                {
                    result = ParentId.ToString();
                }
                return result;
            }
        }
    }
}
