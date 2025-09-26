using Restaurant.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.DTOs.OrderDTOs
{
    public class UpdateOrderDTO
    {
        public string PaymentStatus { get; set; } = null!;
        public string OrderType { get; set; } = null!;


    }
}
