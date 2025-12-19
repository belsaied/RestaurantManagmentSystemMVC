using System.ComponentModel.DataAnnotations;

namespace Restaurant.PL.ViewModels.Auth_ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Full Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Address")]
        public string? Address { get; set; }
    }
}
