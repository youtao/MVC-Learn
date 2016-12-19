using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MVCLearn.ModelBCL;

namespace MVCLearn.Model
{
    /// <summary>
    /// 访问Model
    /// </summary>
    [Table("System_AccessInfo")]
    public class AccessInfo : IntBaseModel
    {
        public AccessInfo()
        {
            this.Title = "请设置标题";
            this.Url = "javascript:void(0);";
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 是否公共可见
        /// </summary>
        public bool IsPublick { get; set; }
    }
}