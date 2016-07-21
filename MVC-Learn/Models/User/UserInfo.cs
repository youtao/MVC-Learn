using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_Learn.Models
{
    /// <summary>
    /// 用户
    /// </summary>
    [Table("System_UserInfo")]
    public class UserInfo : BaseModel
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

        #region 导航属性

        /// <summary>
        /// SignalR连接Id
        /// </summary>
        public virtual ICollection<Connection> Connections { get; set; }

        /// <summary>
        /// SignalR组
        /// </summary>
        public virtual ICollection<Group> Groups { get; set; }

        #endregion
    }
}