using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IGenaricRepository<Product> _productRepo;
        private readonly IGenaricRepository<DeliveryMethod> _deliveryMethodRepo;
        private readonly IGenaricRepository<Order> _orderRepo;

        public OrderService(
            IBasketRepository basketRepo,
            IGenaricRepository<Product> productRepo,
            IGenaricRepository<DeliveryMethod> deliveryMethodRepo,
            IGenaricRepository<Order> orderRepo)
        {
            _basketRepo = basketRepo;
            _productRepo = productRepo;
            _deliveryMethodRepo = deliveryMethodRepo;
            _orderRepo = orderRepo;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address address)
        {
            // 1- Get Basket from BasketRepo
            var Basket = await _basketRepo.GetBasketAsync(basketId);

            // 2- Get selected Item at Basket from ProductRepo
            var OrderItem = new List<OrderItem>();

            if (Basket?.Items?.Count > 0)
            {
                foreach (var item in Basket.Items)
                {
                    var product = await _productRepo.GetAsync(item.Id);

                    var ProductItemOrdered = new ProductItemOrdered(item.Id, product.Name, product.PictureUrl);

                    OrderItem.Add(new OrderItem(ProductItemOrdered, product.Price, item.Quantity));
                }
            }
            // 3- Calc sub total
            var SubTotal =  OrderItem.Sum(O => O.Quantity * O.Price);

            //4- Get DeliveryMethods
            var DeliveryMethod = await _deliveryMethodRepo.GetAsync(deliveryMethodId);

            // 5- Create Order [Add to DB]
            var Order = new Order(buyerEmail ,address ,DeliveryMethod , OrderItem , SubTotal);

            await _orderRepo.Add(Order);

            // 6- Save Changes


            return Order;
        }

        public Task<Order> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
