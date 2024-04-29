using System.ComponentModel.DataAnnotations;

namespace TheLayer.Core.ViewModels
{
    public class ResetTeacherPasswordModelView
    {
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string TeacherEmail { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string TeacherPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare(nameof(TeacherPassword))]
        public string ConfirmPassword { get; set; }

        public List<string>? Messages { get; set; } = new List<string>();
    }
}
