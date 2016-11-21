using Newtonsoft.Json;

namespace MVCLearn.ModelDTO
{
    public class ResponseDto<TData>
    {
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public TData Data { get; set; }
    }
}