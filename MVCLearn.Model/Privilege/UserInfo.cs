using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MVCLearn.ModelBCL;

namespace MVCLearn.Model
{
    /// <summary>
    /// 用户
    /// </summary>
    [Table("Privilege_UserInfo")]
    public class UserInfo : IntBaseModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [Required]
        public string NickName { get; set; }

        /// <summary>
        /// 上次登录时间
        /// </summary>
        public DateTime LoginTime { get; set; }

        /// <summary>
        /// 上次登出时间
        /// </summary>
        public DateTime? SignoutTime { get; set; }
    }
}