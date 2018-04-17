using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApi.Models
{
    public enum OrderStatuss
    {
        Requested, Shipped, Completed, Cancelled, Paid
    }

    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime? Date { get; set; }
        public List<Product> Items { get; set; }
        public OrderStatuss OrderStatus { get; set; }
        public decimal ShippingCharge { get; set; }
    }
}
