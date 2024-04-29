using System.ComponentModel.DataAnnotations;

namespace TheLayer.Core.ViewModels
{
    public class ResetAdminPasswordModelView
    {
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string AdminPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare(nameof(AdminPassword))]
        public string ConfirmPassword { get; set; }
    }
}
