using OrderService.Infrastructure.Public;

namespace OrderService.Infrastructure.Interfaces;

public interface ISort
{
    
    string SortBy { get; set; }
}