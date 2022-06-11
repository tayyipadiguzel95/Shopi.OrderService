using OrderService.Infrastructure.Interfaces;
using OrderService.Infrastructure.Public;

namespace OrderService.Infrastructure.Dtos;

/// <summary>
/// BaseDto
/// </summary>
[Serializable]
public abstract class BaseDto
{
    public Guid Id { get; set; }
}

/// <summary>
/// PagingBaseDto
/// </summary>
public abstract class PagingBaseDto : BaseDto, ISortAndPaging
{
    public string SortBy { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; } = 10;
}