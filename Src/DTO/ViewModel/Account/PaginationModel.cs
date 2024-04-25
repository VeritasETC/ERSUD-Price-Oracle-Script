public class PaginationModel
{
    private int? pageSize = 10;
    private int? currentPage = 1;

    public int? PageSize
    {
        get { return pageSize; }
        set { pageSize = value; }
    }

    public int? CurrentPage
    {
        get { return currentPage; }
        set { currentPage = value; }
    }

    public string SortBy { get; set; }

    public int SortIndex { get; set; } = -1;

    public string Search { get; set; }
}