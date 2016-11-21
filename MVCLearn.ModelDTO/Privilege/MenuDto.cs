using System;
using MVCLearn.ModelDTO.JsonExtensions;
using Newtonsoft.Json;

namespace MVCLearn.ModelDTO
{
    public class MenuDto
    {
        /// <summary>
        /// 父级菜单Id
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 菜单Id
        /// </summary>
        public long Id { get; set; }

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
        /// 子菜单数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int MenuOrder { get; set; }

        [JsonConverter(typeof(StringDateTimeConverter))]
        public DateTime CreateTime { get; set; }
    }
}