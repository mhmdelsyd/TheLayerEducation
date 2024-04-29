using System.ComponentModel.DataAnnotations;

namespace TheLayer.Core.Models.Entities
{
    public class Course
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
