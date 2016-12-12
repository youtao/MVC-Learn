using System.ComponentModel.DataAnnotations.Schema;
using MVCLearn.ModelBCL;

namespace MVCLearn.Model
{
    /// <summary>
    /// 权限
    /// </summary>
    [Table("Privilege_Privilege")]
    public class Privilege : IntBaseModel
    {
        /// <summary>
        /// 权限名
        /// </summary>
        public string PrivilegeName { get; set; }
    }
}