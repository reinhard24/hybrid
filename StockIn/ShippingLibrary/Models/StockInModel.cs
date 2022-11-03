using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StockInLibrary.Models
{
    public class StockInModel
    {
        [Key]
        public int stockin_id { get; set; }
        public int? product_id { get; set; }
        public int? stockin_qty { get; set; }
        public DateTime? entry_date { get; set; }
        public string? notes { get; set; }

        //[JsonIgnore]
        //public virtual products? product { get; set; }
    }
}
