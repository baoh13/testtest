using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.interfaces
{
    public interface IAppDbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
