using Microsoft.EntityFrameworkCore;
using OrderApi2.Models;

namespace OrderApi2.Data
{
    public class OrderApiContext : DbContext
    {
        public OrderApiContext(DbContextOptions<OrderApiContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
    }
}
