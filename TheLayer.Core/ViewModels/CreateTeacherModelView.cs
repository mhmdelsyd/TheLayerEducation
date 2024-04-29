using System.ComponentModel.DataAnnotations;

namespace TheLayer.Core.ViewModels
{
    public class CreateTeacherModelView
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")] public string LastName { get; set; }
        public Guid Course { get; set; }
    }
}
