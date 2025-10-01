using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.BLL.DTOs.TableModule
{
    public class TableDto
    {
        public int Id { get; set; }
        public string TableNumber { get; set; } = null!;

        public int Capacity { get; set; } = 7;
        public string Location { get; set; } = null!;
    }
}
