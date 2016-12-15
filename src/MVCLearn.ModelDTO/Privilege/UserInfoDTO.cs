using System;
using MVCLearn.ModelDTO.JsonExtensions;
using Newtonsoft.Json;

namespace MVCLearn.ModelDTO
{
    /// <summary>
    /// 用户 Model DTO
    /// </summary>
    public class UserInfoDTO
    {
        /// <summary>
        /// 用户ID.
        /// </summary>
        /// <value>The user identifier.</value>
        public int UserID { get; set; }

        /// <summary>
        /// 用户名.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 昵称.
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 上次登录时间.
        /// </summary>
        [JsonConverter(typeof(StringDateTimeConverter))]
        public DateTime LoginTime { get; set; }
    }
}