using System;
using Model.Migrations;
using Newtonsoft.Json;

namespace ConsoleApplication
{
    public class StringDateTimeConverter : JsonConverter
    {
        /// <summary>
        /// 时间字符串格式
        /// </summary>
        public string Format { get; set; } = "yyyy-MM-dd HH:mm:ss:fff";

        /// <summary>
        /// 默认值
        /// </summary>
        public string Default { get; set; } = string.Empty;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteValue(Default);
            }
            else
            {
                try
                {
                    var time = (DateTime)value;
                    writer.WriteValue(time.ToString(this.Format));
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}