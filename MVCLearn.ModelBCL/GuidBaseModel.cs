using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCLearn.ModelBCL
{
    /// <summary>
    /// Guid主键BaseModel
    /// </summary>
    public class GuidBaseModel : BaseModel
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid ID { get; set; }
    }
}