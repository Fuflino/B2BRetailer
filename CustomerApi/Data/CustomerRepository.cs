using System;
using System.Collections.Generic;
using CustomerApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CustomerApi.Data
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly CustomerApiContext db;

        public CustomerRepository(CustomerApiContext context)
        {
            db = context;
        }

        Customer IRepository<Customer>.Add(Customer entity)
        {

            var newCustomer = db.Customers.Add(entity).Entity;
            db.SaveChanges();
            return newCustomer;
        }

        void IRepository<Customer>.Edit(Customer entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        Customer IRepository<Customer>.Get(int regNo)
        {
            return db.Customers.Include("BillingAddress").Include("ShippingAddress").FirstOrDefault(o => o.RegNo == regNo);
        }

        IEnumerable<Customer> IRepository<Customer>.GetAll()
        {
            return db.Customers.ToList();
        }

        void IRepository<Customer>.Remove(int regNo)
        {
            var Customer = db.Customers.FirstOrDefault(p => p.RegNo == regNo);
            db.Customers.Remove(Customer);
            db.SaveChanges();
        }
    }
}