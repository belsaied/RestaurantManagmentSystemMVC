using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Models
{
    internal class Ingredient
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }=string.Empty;
        [Range(0, int.MaxValue)]
        public string Unit { get; set; }= string.Empty;

        public decimal UnitCost { get; set; }

        public decimal CurrentStock { get; set; }
        public decimal MinStock { get; set; }

        public bool IsActive { get; set; }
    }
}
