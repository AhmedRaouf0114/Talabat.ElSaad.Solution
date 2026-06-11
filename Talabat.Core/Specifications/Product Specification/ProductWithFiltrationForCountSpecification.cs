using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.Product_Specification
{
    public class ProductWithFiltrationForCountSpecification : BaseSpesifications<Product>
    {
        public ProductWithFiltrationForCountSpecification(ProductSepcParams SpecParams) :
             base
            (
                P =>
                ((string.IsNullOrEmpty(SpecParams.Search) || P.Name.ToLower().Contains(SpecParams.Search)) &&
                !SpecParams.BrandId.HasValue || P.BrandId == SpecParams.BrandId) && 
                (!SpecParams.CategoreId.HasValue || P.CategoryId == SpecParams.CategoreId)
            )
        {
            
        }
    }
}
