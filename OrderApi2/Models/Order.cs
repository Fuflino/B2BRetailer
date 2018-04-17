using System;
using System.Collections.Generic;
using OrderApi2.Models;

namespace OrderApi2.Models
{
    public enum OrderStatuss
    {
        Requested, Shipped, Completed, Cancelled, Paid
    }

    public class Order
    {
        public int Id { get; set; }
        public int CustomerRegNo { get; set; }
        public DateTime? Date { get; set; }
        public List<Product> Items { get; set; }
        public OrderStatuss OrderStatus { get; set; }
        public decimal ShippingCharge { get; set; }
    }

    
}
