using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Data.EntityFramework.Entities.Orders;

namespace OrderService.Domain.Data.EntityFramework;

/// <summary>
/// DataContext
/// </summary>
public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
     
           
    }
    public DbSet<Order> Orders { get; set; }
}