namespace OrderService.Infrastructure.Interfaces;

/// <summary>
/// IPaging
/// </summary>
public interface IPaging
{
    int PageNumber { get; set; }
    int PageSize { get; set; }
}