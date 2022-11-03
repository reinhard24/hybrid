using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HybridKitchen.Models
{
    public partial class catalogs
    {
        public catalogs()
        {
            //products = new HashSet<products>();
        }

        public int catalog_id { get; set; }
        public string catalog_name { get; set; } = null!;
        public int? product_qty { get; set; }
        public int? sold { get; set; }
        
        //[JsonIgnore]
        //public virtual ICollection<products> products { get; set; }
    }
}
