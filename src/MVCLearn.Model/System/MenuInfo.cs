using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MVCLearn.ModelBCL;

namespace MVCLearn.Model
{
    /// <summary>
    /// 菜单Model.
    /// </summary>
    [Table("System_MenuInfo")]
    public class MenuInfo : IntBaseModel
    {
        public MenuInfo()
        {
            this.Title = "Title";
            this.Url = "javascript:void(0);";
            this.Icon = "";
            this.IsIframe = true;
            this.IsMenu = true;
            this.IsPublick = false;
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
        /// 是否是iframe(默认:true)
        /// </summary>
        public bool IsIframe { get; set; }

        /// <summary>
        /// 是否是菜单权限(否则是访问权限,默认:true)
        /// </summary>
        public bool IsMenu { get; set; }

        /// <summary>
        /// 是否公共可见(默认:false)
        /// </summary>
        public bool IsPublick { get; set; }


        /// <summary>
        /// 父级菜单Id
        /// </summary>
        [ForeignKey("Parent")]
        public int? ParentID { get; set; }
        public virtual MenuInfo Parent { get; set; }

        /// <summary>
        /// 直接子菜单
        /// </summary>
        public virtual List<MenuInfo> Children { get; set; }
    }
}