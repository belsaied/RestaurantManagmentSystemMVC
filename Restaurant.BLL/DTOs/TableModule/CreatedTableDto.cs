using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.DTOs.TableModule
{
    public class CreatedTableDto
    {

        [Required(ErrorMessage = "*")]
        [Display(Name ="Table Number")]
        public string TableNumber { get; set; } = null!;

        public int Capacity { get; set; } = 7;
        public string Location { get; set; } = null!;
    }
}
