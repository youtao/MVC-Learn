using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCLearn.ModelBCL
{
    /// <summary>
    /// int主键BaseModel
    /// </summary>
    public class IntBaseModel : BaseModel
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int ID { get; set; }
    }
}