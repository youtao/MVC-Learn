using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebUI.Models
{
    [Table("System_Menu")]
    public class Menu : BaseModel
    {
        public Menu()
        {
            this.Title = "Title";
            this.Url = "javascript:void(0);";
            this.Icon = "";
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