using System.ComponentModel.DataAnnotations;

namespace Restaurant.PL.ViewModels.Auth_ViewModels
{
    public class LoginUserViewModel
    {
        [Required(ErrorMessage ="*")]
        [Display(Name ="User Name")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage ="*")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [Display(Name = "Remeber Me!")]
        public bool RememberMe { get; set; } = false;

    }
}
