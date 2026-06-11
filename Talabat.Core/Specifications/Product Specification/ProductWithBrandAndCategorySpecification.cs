using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications.Product_Specification;

namespace Talabat.Core.Specifications
{
    public class ProductWithBrandAndCategorySpecification : BaseSpesifications<Product>
    {
        public ProductWithBrandAndCategorySpecification(ProductSepcParams SpecParams) : 
            base
            (
                P =>
                ((string.IsNullOrEmpty(SpecParams.Search) || P.Name.ToLower().Contains(SpecParams.Search) ) &&
                !SpecParams.BrandId.HasValue || P.BrandId == SpecParams.BrandId) &&
                (!SpecParams.CategoreId.HasValue || P.CategoryId == SpecParams.CategoreId)
            ) 
        {
            Includes.Add(P => P.Category);
            Includes.Add(P => P.Brand);

            if (!string.IsNullOrEmpty(SpecParams.Sort))
            {
                switch (SpecParams.Sort) 
                {
                    case "priceAsc":
                        AddOrderByAsc(P => P.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(P => P.Price);
                        break;
                    default:
                        AddOrderByAsc(P => P.Name);
                        break;
                }
            }
            else
            {
                AddOrderByAsc(P => P.Name);
            }

            ApplayPagination((SpecParams.PageIndex-1)*SpecParams.PageSize , SpecParams.PageSize);
        }

        public ProductWithBrandAndCategorySpecification(int id ) : base(P => P.Id==id)
        {
            Includes.Add(P => P.Category);
            Includes.Add(P => P.Brand);
        }

        


    }
}
