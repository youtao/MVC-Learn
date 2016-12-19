using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCLearn.Model
{
    /// <summary>
    /// 菜单 Model.
    /// </summary>
    [Table("System_MenuInfo")]
    public class MenuInfo : AccessInfo
    {
        /// <summary>
        /// 图标.
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 排序.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 是否是iframe.
        /// </summary>
        public bool IsIframe { get; set; }

        /// <summary>
        /// 父级菜单ID(外键).
        /// </summary>
        [ForeignKey("Parent")]
        public int? ParentID { get; set; }

        /// <summary>
        /// 父级菜单(导航).
        /// </summary>
        /// <value>The parent.</value>
        public virtual MenuInfo Parent { get; set; }

        /// <summary>
        /// 直接子菜单(导航).
        /// </summary>
        public virtual List<MenuInfo> Children { get; set; }

    }
}