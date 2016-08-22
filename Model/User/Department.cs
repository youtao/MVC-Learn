using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Hierarchy;

namespace Model
{
    [Table("System_Department")]
    public class Department : BaseModel
    {
        public string Name { get; set; }
        [Required]
        public HierarchyId Path { get; set; }
    }
}