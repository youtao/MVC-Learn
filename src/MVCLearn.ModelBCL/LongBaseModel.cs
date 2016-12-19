using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCLearn.ModelBCL
{
    /// <summary>
    /// long主键BaseModel
    /// </summary>
    public class LongBaseModel:BaseModel
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual long ID { get; set; }
    }
}