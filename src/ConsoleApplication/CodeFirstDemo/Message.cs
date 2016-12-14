namespace ConsoleApplication.CodeFirstDemo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Message")]
    public partial class Message
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime PostTime { get; set; }

        public int FromUserId { get; set; }

        public int ToUserId { get; set; }

        public virtual User User { get; set; }

        public virtual User User1 { get; set; }
    }
}
