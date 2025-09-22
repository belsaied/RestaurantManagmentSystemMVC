using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Models
{
    public class RecipeLine
    {
        public int Id { get; set; }
        
        public int Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
    }
}
