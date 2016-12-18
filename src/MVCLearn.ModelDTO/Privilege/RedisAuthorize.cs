using System;
using System.Collections.Generic;
using MVCLearn.Utilities;

namespace MVCLearn.ModelDTO.Privilege
{
    public class RedisAuthorize
    {
        public RedisAuthorize(UserInfoDTO value)
        {
            this.Value = value;
            this.AuthorizeId = (value.UserID + value.UserName).MD5();
        }
        /// <summary>
        /// 授权Id.
        /// </summary>
        /// <value>The authorize identifier.</value>
        public string AuthorizeId { get; }

        /// <summary>
        /// 创建时间.
        /// </summary>
        /// <value>The create time.</value>
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 过期时间(默认7天).
        /// </summary>
        /// <value>The expiry.</value>
        public TimeSpan Expiry { get; set; } = TimeSpan.FromDays(7);

        /// <summary>
        ///用户信息.
        /// </summary>
        /// <value>The value.</value>
        public UserInfoDTO Value { get; set; }
    }
}