using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MVCLearn.ModelBCL;

namespace MVCLearn.Model
{
    /// <summary>
    /// SignalR组Model.
    /// </summary>
    [Table("SignalR_Group")]
    public class Group : LongBaseModel
    {
        /// <summary>
        /// 组名.
        /// </summary>
        [Required]
        public string GroupName { get; set; }

        #region 导航属性

        /// <summary>
        /// 用户(导航).
        /// </summary>
        public virtual List<UserInfo> UserInfos { get; set; }

        #endregion

    }
}