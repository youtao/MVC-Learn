using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MVCLearn.ModelBCL;

namespace MVCLearn.Model
{
    /// <summary>
    /// 用户Model.
    /// </summary>
    [Table("System_UserInfo")]
    public class UserInfo : IntBaseModel
    {
        /// <summary>
        /// 用户名.
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// 密码.
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// 昵称.
        /// </summary>
        [Required]
        public string NickName { get; set; }

        /// <summary>
        /// 上次登录时间.
        /// </summary>
        public DateTime LoginTime { get; set; }

        /// <summary>
        /// 上次登出时间.
        /// </summary>
        public DateTime? SignoutTime { get; set; }

        /// <summary>
        ///用户角色(导航).
        /// </summary>
        public virtual List<RoleInfo> RoleInfos { get; set; }
    }
}