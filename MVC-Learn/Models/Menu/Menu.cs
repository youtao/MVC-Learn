using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Learn.Models
{
    [Table("Menu")]
    public class Menu : BaseModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; }        

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 父级菜单Id
        /// </summary>
        [ForeignKey("Parent")]
        public long? ParentId { get; set; }

        public virtual Menu Parent { get; set; }

        /// <summary>
        /// 直接子菜单
        /// </summary>
        public virtual ICollection<Menu> Children { get; set; }
    }
}