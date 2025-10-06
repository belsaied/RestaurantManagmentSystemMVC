using Restaurant.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Restaurant.BLL.DTOs.CustomerDTOs
{
    public class CreateCustomerDTO
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be 2-50 characters")]
        [RegularExpression(@"^[\p{L}\p{M}'\-\s]{2,50}$", ErrorMessage = "First name contains invalid characters")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be 2-50 characters")]
        [RegularExpression(@"^[\p{L}\p{M}'\-\s]{2,50}$", ErrorMessage = "Last name contains invalid characters")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [RegularExpression(@"^\+?[0-9\s\-]{7,15}$", ErrorMessage = "Phone must be 7-15 digits, optional +, spaces or dashes")]
        [StringLength(20, ErrorMessage = "Phone must be 20 characters or less")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(254, ErrorMessage = "Email must be 254 characters or less")]
        public string Email { get; set; } = null!;
        public IFormFile? Image { get; set; }
    }
}
