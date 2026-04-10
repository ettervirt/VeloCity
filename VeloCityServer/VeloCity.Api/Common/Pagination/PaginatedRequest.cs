namespace VeloCity.Api.Common.Pagination;

public class PaginatedRequest
{
    private int _pageNumber = 1;
    private int _pageSize = 20;

    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = value < 1 ? 1 : value;
    }
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value < 1 ? 1 : (value > 100 ? 100 : value);
    }
    public string? SortBy { get; set; }
    public string SortDirection { get; set; } = "asc";
    public bool IsDescending => SortDirection.ToLower() == "desc";
}
