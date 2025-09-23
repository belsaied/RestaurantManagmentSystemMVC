using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Models
{
    public class Table:baseEntity
    {
        [Required(ErrorMessage = "*")]
        public string TableNumber { get; set; } = null!;

        public int Capacity { get; set; }=7;
        public string Location { get; set; } = null!;

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;
        public IEnumerable<Order>? NavOrders { get; set; }

    }
}
//Ahmed