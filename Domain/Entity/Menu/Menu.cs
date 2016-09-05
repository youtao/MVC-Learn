using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    [Table("System_Menu")]
    public class Menu : AggregateRoot
    {
        public Menu()
        {
            this.Title = "标题";
            this.Url = "javascript:void(0);";
            this.Icon = "fa fa-user";
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
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 父级菜单Id
        /// </summary>
        [ForeignKey("Parent")]
        public long? ParentId { get; set; }

        public virtual Menu Parent { get; set; }

        /// <summary>
        /// 直接子菜单
        /// </summary>
        public virtual List<Menu> Children { get; set; }
    }
}