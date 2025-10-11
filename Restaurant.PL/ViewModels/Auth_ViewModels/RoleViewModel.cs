using System.ComponentModel.DataAnnotations;

namespace Restaurant.PL.ViewModels.Auth_ViewModels
{
    public class RoleViewModel
    {
        [Display(Name = "Role Name")]
        public string RoleName { get; set; } = null!;
    }
}
