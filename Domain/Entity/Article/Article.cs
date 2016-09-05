using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    /// <summary>
    /// 文章
    /// </summary>
    [Table("Article")]
    public class Article : AggregateRoot
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        [Required]
        public string Author { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        public virtual ArticleExt ArticleExt { get; set; }

    }
}