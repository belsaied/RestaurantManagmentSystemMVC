using Restaurant.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.DTOs.CustomerDTOs
{
    public class CustomerDTO
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public int LoyaltyPoints { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }     
        public bool IsDeleted { get; set; }
        public int Id { get; set; }



    }
}
