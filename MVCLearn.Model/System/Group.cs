using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MVCLearn.ModelBCL;

namespace MVCLearn.Model
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
        public virtual List<UserInfo> UserInfos { get; set; }

        #endregion

    }
}