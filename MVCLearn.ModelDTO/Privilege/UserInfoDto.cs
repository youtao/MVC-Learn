using System;

namespace MVCLearn.ModelDTO
{
    /// <summary>
    /// UserInfo DTO
    /// </summary>
    public class UserInfoDto
    {
        public long UserID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 上次登录时间
        /// </summary>
        public DateTime LoginTime { get; set; }
    }
}