using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeed
    {

        public async static Task SeedAsync(StoreContext _DbContext)
        {
            if (_DbContext.productBrands.Count()==0)
            {
                var BrandData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);


                if (Brands?.Count != 0)
                {
                    foreach (var Brand in Brands)
                    {
                         _DbContext.Set<ProductBrand>().Add(Brand);
                    }
                    await _DbContext.SaveChangesAsync();
                } 
            }
            if (_DbContext.ProductCategories.Count() == 0)
            {
                var CategoryData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
                var Categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoryData);


                if (Categories?.Count != 0)
                {
                    foreach (var category in Categories)
                    {
                        _DbContext.Set<ProductCategory>().Add(category);
                    }
                    await _DbContext.SaveChangesAsync();
                }
            }
            if (_DbContext.products.Count() == 0)
            {
                var ProductData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);


                if (Products?.Count != 0)
                {
                    foreach (var Product in Products)
                    {
                        _DbContext.Set<Product>().Add(Product);
                    }
                    await _DbContext.SaveChangesAsync();
                }
            }
            if (_DbContext.DeliveryMethods.Count() == 0)
            {
                var DeliveryData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");
                var Delivery = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryData);


                if (Delivery?.Count != 0)
                {
                    foreach (var method in Delivery)
                    {
                        _DbContext.Set<DeliveryMethod>().Add(method);
                    }
                    await _DbContext.SaveChangesAsync();
                }
            }

        }
    }
}
