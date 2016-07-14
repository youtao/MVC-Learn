using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_Learn.Models
{
    /// <summary>
    /// SignalR组
    /// </summary>
    [Table("SignalR_Group")]
    public class Group : BaseModel
    {
        /// <summary>
        /// 组名
        /// </summary>
        [Required]
        public string GroupName { get; set; }

        #region 导航属性

        /// <summary>
        /// 用户
        /// </summary>
        public virtual ICollection<UserInfo> UserInfos { get; set; }

        #endregion

    }
}