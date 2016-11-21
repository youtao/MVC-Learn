using System;
using Newtonsoft.Json;

namespace MVCLearn.ModelDTO.JsonExtensions
{
    public class StringNumberConverter : JsonConverter
    {
        /// <summary>
        /// 默认值
        /// </summary>
        public string Default { get; set; } = string.Empty;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value?.ToString() ?? Default);
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