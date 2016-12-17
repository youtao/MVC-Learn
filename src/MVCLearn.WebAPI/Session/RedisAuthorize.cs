using System;
using MVCLearn.ModelDTO;
using MVCLearn.Utilities;

namespace MVCLearn.WebAPI.Session
{
    public class RedisAuthorize
    {
        public RedisAuthorize(UserInfoDTO value)
        {
            this.Value = value;
            this.AuthorizeId = (value.UserID + value.UserName).MD5();
        }

        public string AuthorizeId { get; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 过期时间(默认7天).
        /// </summary>
        /// <value>The expiry.</value>
        public TimeSpan Expiry { get; set; } = TimeSpan.FromDays(7);

        public UserInfoDTO Value { get; set; }
    }
}