using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingLibrary.Models
{
    public class ShippingModel
    {
        public ShippingModel()
        {
            //orders = new HashSet<orders>();
        }


        [Key]
        public int shipping_id { get; set; }
        public string name { get; set; } = null!;

        //[JsonIgnore]
        //public virtual ICollection<orders> orders { get; set; }
    }
}
