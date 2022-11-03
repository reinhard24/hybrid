using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAdjustmentLibrary.Models
{
    public class StockAdjustmentModel
    {
        [Key]
        public int stockadjusment_id { get; set; }
        public DateTime? adjusment_date { get; set; }
        
    }
}
