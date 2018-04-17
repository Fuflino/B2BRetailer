using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using CustomerApi.Models;

namespace CustomerApi.Data
{
    public static class DbInit
    {
        public static void Initialize(CustomerApiContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Look for any Products
            if (context.Customers.Any())
            {
                return;   // DB has been seeded
            }

            var Address1 = new Address()
            {
                Id = 1,
                City = "Esbjerg",
                Country = "Danmark",
                StreetName = "Fynsgade 33",
                ZipCode = 6700
            };
            
            var Address2 = new Address()
            {
                Id = 2,
                City = "Esbjerg",
                Country = "Danmark",
                StreetName = "Tarp byvej 50",
                ZipCode = 6715
            };

            List<Customer> customers = new List<Customer>
            {
                new Customer
                {
                    RegNo = 1,
                    Email = "First@customer.dk",
                    Phone = "12345678",
                    CompanyName = "Some Company A/S",
                    BillingAddress = Address1,
                    ShippingAddress = Address1

                },

                new Customer
                {
                RegNo = 2,
                Email = "Second@customer.dk",
                Phone = "87654321",
                CompanyName = "Some Other Company A/S",
                ShippingAddress = Address2,
                BillingAddress = Address2
                }
            };

            context.Customers.AddRange(customers);
            context.SaveChanges();
        }
    }
}