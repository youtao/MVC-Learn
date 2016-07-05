using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_Learn.Models
{
    [Table("Article")]
    public class Article : BaseModel
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string Content { get; set; }

        public string Description { get; set; }
    }
}