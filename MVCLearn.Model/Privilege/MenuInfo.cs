using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MVCLearn.ModelBCL;

namespace MVCLearn.Model
{
    [Table("Privilege_MenuInfo")]
    public class MenuInfo : BaseModel
    {
        public MenuInfo()
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
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 父级菜单Id
        /// </summary>
        [ForeignKey("Parent")]
        public long? ParentID { get; set; }

        public virtual MenuInfo Parent { get; set; }

        /// <summary>
        /// 直接子菜单
        /// </summary>
        public virtual List<MenuInfo> Children { get; set; }
    }
}