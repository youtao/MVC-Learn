using System;
using MVCLearn.ModelDTO.JsonExtensions;
using Newtonsoft.Json;

namespace MVCLearn.ModelDTO
{
    /// <summary>
    /// 菜单 Model DTO.
    /// </summary>
    public class MenuInfoDTO
    {
        /// <summary>
        /// 父级菜单ID.
        /// </summary>
        public int? ParentID { get; set; }

        /// <summary>
        /// 菜单ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 标题.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 地址.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 图标.
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 子菜单数量.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 排序.
        /// </summary>
        public int MenuOrder { get; set; }
        /// <summary>
        /// 创建时间.
        /// </summary>
        /// <value>The create time.</value>
        [JsonConverter(typeof(StringDateTimeConverter))]
        public DateTime CreateTime { get; set; }
    }
}