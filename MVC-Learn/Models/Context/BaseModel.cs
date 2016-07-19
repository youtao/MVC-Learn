using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_Learn.Models
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
        public long Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}