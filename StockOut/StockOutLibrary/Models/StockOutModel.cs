using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOutLibrary.Models
{
    public class StockOutModel
    {
        [Key]
        public int stockout_id { get; set; }
        public int? product_id { get; set; }
        public int? stockout_qty { get; set; }
        public DateTime? out_date { get; set; }
        public string? notes { get; set; }

    }
}
