using System.Collections.Generic;
using System.Linq;
using OrderApi.Models;
using System;

namespace OrderApi.Data
{
    public static class DbInitializer
    {
        // This method will create and seed the database.
        public static void Initialize(OrderApiContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Look for any Products
            if (context.Orders.Any())
            {
                return;   // DB has been seeded
            }

            List<Product> products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Shoe",
                    Price = 999,
                    Quantity = 3
                },
                new Product
                {
                    Id = 2,
                    Name = "T-Shirt",
                    Price = 50,
                    Quantity = 2
                }

            };

            List<Order> orders = new List<Order>
            {
                new Order {
                    Id = 1,
                    Date = DateTime.Today,
                    CustomerRegNo = 1,
                    OrderStatus = OrderStatuss.Requested,
                    ShippingCharge = 999,
                    Items = products
                },
                new Order
                {
                    Id = 2,
                    Date = DateTime.Today,
                    CustomerRegNo = 1,
                    OrderStatus = OrderStatuss.Paid,
                    ShippingCharge = 999,
                    Items = products
                },
                new Order
                {
                    Id = 3,
                    Date = DateTime.Today,
                    CustomerRegNo = 1,
                    OrderStatus = OrderStatuss.Paid,
                    ShippingCharge = 999,
                    Items = products

                }
            };

            context.Orders.AddRange(orders);
            context.SaveChanges();
        }
    }
}
