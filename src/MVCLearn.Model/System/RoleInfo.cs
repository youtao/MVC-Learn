using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MVCLearn.ModelBCL;

namespace MVCLearn.Model
{
    /// <summary>
    /// 角色
    /// </summary>
    [Table("System_RoleInfo")]
    public class RoleInfo : IntBaseModel
    {
        /// <summary>
        /// 角色名
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 菜单权限
        /// </summary>
        public virtual List<MenuInfo> MenuPrivileges { get; set; }

        /// <summary>
        /// 按钮权限
        /// </summary>
        public virtual List<ButtonInfo> ButtonPrivileges { get; set; }
    }
}