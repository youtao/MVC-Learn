using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCLearn.ModelBCL
{
    /// <summary>
    /// 模型基础类
    /// </summary>
    public class BaseModel
    {
        public BaseModel()
        {
            this.CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 主键Id
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool Delete { get; set; }
    }
}