using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.DTOs.OrderItemsModule
{
    public class OrderItemsDetailsDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }
        public string? CreatedBy { get; set; }   // UserId
        public DateTime? CreatedOn { get; set; }   // the DateTime of Creating the Record. (Nullable because i'll put a default value for him in the configurations).
        public string? ModifiedBy { get; set; }   // UserId
        public DateTime? ModifiedOn { get; set; }  // the DateTime of Modifying the Record. (Nullable because i'll put a default value for him in the configurations).
        public bool IsDeleted { get; set; }   // to make a soft delete for the record instead of hard delete.
    }
}
