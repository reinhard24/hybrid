using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogLibrary.Models
{
    public class CatalogModel
    {
        public CatalogModel()
        {
            //products = new HashSet<products>();
        }

        [Key]
        public int catalog_id { get; set; }
        public string catalog_name { get; set; } = null!;
        public int? product_qty { get; set; }
        public int? sold { get; set; }

        //[JsonIgnore]
        //public virtual ICollection<products> products { get; set; }
    }
}
