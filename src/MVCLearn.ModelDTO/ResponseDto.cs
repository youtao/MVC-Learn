using Newtonsoft.Json;

namespace MVCLearn.ModelDTO
{
    /// <summary>
    /// WEB 返回 DTO.
    /// </summary>
    /// <typeparam name="TData">返回的数据类型.</typeparam>
    public class ResponseDTO<TData>
    {
        /// <summary>
        /// 要返回的数据.
        /// </summary>
        /// <value>The data.</value>
        [JsonProperty("data")]
        public TData Data { get; set; }
        /// <summary>
        /// 状态.
        /// </summary>
        /// <value>The state.</value>
        [JsonProperty("state")]
        public string State { get; set; }
        /// <summary>
        /// 消息.
        /// </summary>
        /// <value>The message.</value>
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}