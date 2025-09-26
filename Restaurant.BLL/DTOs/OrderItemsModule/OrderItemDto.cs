using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.DTOs.OrderItemsModule
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        [Required(ErrorMessage = "*")]
        public decimal UnitPrice { get; set; }
    }
}
