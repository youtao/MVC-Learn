using System;
using System.Collections.Generic;
using MVCLearn.Utilities;

namespace MVCLearn.ModelDTO.Privilege
{
    /// <summary>
    /// redis缓存授权
    /// </summary>
    public class RedisAuthorize
    {
        public RedisAuthorize(UserInfoDTO value)
        {
            this.Value = value;
            this.AuthorizeId = (value.UserID + value.UserName).MD5();
        }
        /// <summary>
        /// 授权Id
        /// </summary>
        public string AuthorizeId { get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 过期时间(默认7天)
        /// </summary>
        public TimeSpan Expiry { get; set; } = TimeSpan.FromDays(7);

        /// <summary>
        ///用户信息
        /// </summary>
        public UserInfoDTO Value { get; set; }
    }
}