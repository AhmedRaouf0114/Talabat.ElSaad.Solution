using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications.Product_Specification
{
    public class ProductSepcParams
    {
        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value?.ToLower(); }
        }

        private int pageSize = 5;
        
        public int PageSize {
             get { return pageSize; }
             set { pageSize = value > 10 ? 10 : value; }
        }
        public int PageIndex { get; set; } = 1;
        public int? BrandId { get; set; }
        public int? CategoreId { get; set; }
        public string? Sort { get; set; }


    }
}
