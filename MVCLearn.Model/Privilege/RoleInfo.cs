using System.ComponentModel.DataAnnotations.Schema;
using MVCLearn.ModelBCL;

namespace MVCLearn.Model
{
    /// <summary>
    /// 角色
    /// </summary>
    [Table("Privilege_RoleInfo")]
    public class RoleInfo : BaseModel
    {
        /// <summary>
        /// 角色名
        /// </summary>
        public string RoleName { get; set; }
    }
}