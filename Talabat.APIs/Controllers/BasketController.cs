using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Dtos;
using Talabat.APIs.Error;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.APIs.Controllers
{
    
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IMapper _maper;

        public BasketController(IBasketRepository basketRepo
            ,IMapper maper)
        {
            _basketRepo = basketRepo;
            _maper = maper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string Id)
        {
            var Basket = await _basketRepo.GetBasketAsync(Id);

            return Ok(Basket ?? new CustomerBasket(Id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDtos basket)
        {
            var MappingBasket = _maper.Map<CustomerBasketDtos, CustomerBasket>(basket);
            var createdOrUpdated = await _basketRepo.UpdateBasketAsync(MappingBasket);
            if (createdOrUpdated == null) { return BadRequest(new ApiResponse(400)); }

            return Ok(createdOrUpdated);
        }

        [HttpDelete] 
        public async Task DeleteBasket(string Id)
        {
            await _basketRepo.DeleteBasketAsync(Id);
        }
    }
}
