using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Talabat.APIs.Dtos;
using Talabat.APIs.Error;
using Talabat.APIs.Helper;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.Product_Specification;

namespace Talabat.APIs.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IGenaricRepository<Product> _ProductRepo;
        private readonly IMapper _Maper;
        private readonly IGenaricRepository<ProductBrand> _productBrand;
        private readonly IGenaricRepository<ProductCategory> _productCategory;

        public ProductsController(IGenaricRepository<Product> productRepo 
                                  , IMapper maper
                                  , IGenaricRepository<ProductBrand> productBrand
                                  , IGenaricRepository<ProductCategory> productCategory)
        {
            _ProductRepo = productRepo;
            _Maper = maper;
            _productBrand = productBrand;
            _productCategory = productCategory;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDtos>>> GetAllProject([FromQuery]ProductSepcParams specParams)
        {
            var spec = new ProductWithBrandAndCategorySpecification(specParams);
            
            var products = await _ProductRepo.GetAllWithSpecAsync(spec);

            var data = _Maper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDtos>>(products);

            var count = await _ProductRepo.GetCountAsync(new ProductWithFiltrationForCountSpecification(specParams));

            return Ok(new Pagination<ProductToReturnDtos>(specParams.PageIndex , specParams.PageSize , count ,data));
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<ProductToReturnDtos>> GetProduct(int id)
        { 
            var spec = new ProductWithBrandAndCategorySpecification(id);

            var product = await _ProductRepo.GetWithSpecAsync(spec);

            if(product == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(_Maper.Map<Product, ProductToReturnDtos>(product));

        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var brands = await _productBrand.GetAllAsync();

            return Ok(brands);
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetProductCategores()
        {
            var categories = await _productCategory.GetAllAsync();

            return Ok(categories);
        }
    }
}
