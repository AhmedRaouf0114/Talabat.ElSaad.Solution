using Talabat.Core.Entities;

namespace Talabat.APIs.Dtos
{
    public class CustomerBasketDtos
    {
        public string Id { get; set; }
        public List<BasketItemDtos> Items { get; set; }
    }
}
