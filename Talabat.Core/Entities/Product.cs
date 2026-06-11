using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string PictureUrl { get; set; }

        public decimal Price { get; set; }

        public int BrandId { get; set; } // Forign Key
        public virtual ProductBrand Brand { get; set; } // Navagition Property [One]

        public int CategoryId { get; set; } // Forign Key
        public virtual ProductCategory Category { get; set; } // Navagition Property [One]
    }
}
