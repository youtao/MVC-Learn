using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MVCLearn.ModelBCL;

namespace MVCLearn.Model
{
    /// <summary>
    /// 角色Model.
    /// </summary>
    [Table("System_RoleInfo")]
    public class RoleInfo : IntBaseModel
    {
        /// <summary>
        /// 角色名.
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 访问权限(导航).
        /// </summary>
        public virtual List<AccessInfo> AccessPrivileges { get; set; }

        /// <summary>
        /// 按钮权限(导航).
        /// </summary>
        public virtual List<ButtonInfo> ButtonPrivileges { get; set; }
    }
}