using System;
using Newtonsoft.Json;

namespace MVCLearn.ModelDTO.JsonExtensions
{
    public class StringDateTimeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteValue(string.Empty);
            }
            else
            {
                try
                {
                    var time = (DateTime)value;
                    writer.WriteValue(time.ToString("yyyy-MM-dd HH:mm:ss:fff"));
                }
                catch (Exception)
                {
                    writer.WriteValue(string.Empty);
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