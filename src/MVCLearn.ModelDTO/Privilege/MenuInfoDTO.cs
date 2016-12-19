using System;
using System.Collections.Generic;
using MVCLearn.ModelDTO.JsonExtensions;
using Newtonsoft.Json;

namespace MVCLearn.ModelDTO
{
    /// <summary>
    /// 菜单DTO
    /// </summary>
    public class MenuInfoDTO
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public int MenuID { get; set; }

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
        /// 是否是Iframe
        /// </summary>
        public bool IsIframe { get; set; }

        /// <summary>
        /// 父级菜单ID
        /// </summary>
        public int? ParentID { get; set; }

        /// <summary>
        /// 直接只菜单
        /// </summary>
        public List<MenuInfoDTO> Children { get; set; }
    }
}