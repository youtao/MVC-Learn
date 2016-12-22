using MVCLearn.ModelEnum;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MVCLearn.ModelDTO
{
    /// <summary>
    /// 按钮DTO
    /// </summary>
    public class ButtonInfoDTO
    {
        /// <summary>
        /// 按钮名称
        /// </summary>
        [JsonProperty("name")]
        public string ButtonName { get; set; }

        /// <summary>
        /// 按钮类型
        /// </summary>
        [JsonProperty("type")]
        public ButtonType ButtonType { get; set; }
    }
}