using System.ComponentModel.DataAnnotations;

namespace Restaurant.PL.ViewModels.Auth_ViewModels
{
    public class RegisterUserViewModel
    {
        [RegularExpression(@"^[a-zA-Z0-9_.-]{3,20}$",
        ErrorMessage = "Username must be 3-20 characters and can only contain letters, numbers, underscores, dots, and dashes.")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = null!;
        public string Address { get; set; } = null!;

    }
}
