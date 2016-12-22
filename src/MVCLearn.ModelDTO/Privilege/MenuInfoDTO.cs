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
        public MenuInfoDTO()
        {
            this.Children = new List<MenuInfoDTO>();
        }

        /// <summary>
        /// 菜单ID
        /// </summary>
        [JsonProperty("menuid")]
        public int MenuID { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [JsonProperty("icon")]
        public string Icon { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [JsonProperty("order")]
        public int Order { get; set; }

        /// <summary>
        /// 是否是Iframe
        /// </summary>
        [JsonProperty("isiframe")]
        public bool IsIframe { get; set; }

        /// <summary>
        /// 父级菜单ID
        /// </summary>
        [JsonProperty("parentid")]
        public int? ParentID { get; set; }

        /// <summary>
        /// 直接只菜单
        /// </summary>
        [JsonProperty("children")]
        public List<MenuInfoDTO> Children { get; set; }
    }
}