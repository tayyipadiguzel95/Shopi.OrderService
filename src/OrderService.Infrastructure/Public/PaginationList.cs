
namespace OrderService.Infrastructure.Public
{
    /// <summary>
    /// PaginationList
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaginationList<T>
    {
        public PaginationList(PaginationDto page, IList<T> data)
        {
            Page = page ?? new PaginationDto();
            Items = data ?? new List<T>();
        }
        public PaginationDto Page { get; set; }
        public IList<T> Items { get; set; }
    }

    /// <summary>
    /// PageNumber
    /// </summary>
    public class PaginationDto
    {
        public PaginationDto()
        {

        }

        public PaginationDto(long totalCount, int pageNumber, int pageSize)
        {
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
        public bool HasNext { get; set; }
        public long TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
