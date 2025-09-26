using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.DTOs.TableModule
{
    public class TabelDetailsDto
    {
        public int Id { get; set; }
        public string TableNumber { get; set; } = null!;

        public int Capacity { get; set; } = 7;
        public string Location { get; set; } = null!;

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;
        public string? CreatedBy { get; set; }   // UserId
        public DateTime? CreatedOn { get; set; } //Date Of Creation
    }
}
