namespace Domain;
public class PageInfo {

    public PageInfo(int currentPage, int pageSize, int totalCount) {
        CurrentPage = currentPage;
        PageSize = pageSize;
        TotalCount = totalCount;
    }
    public int CurrentPage { get; private set; }
    public int PageSize { get; private set; }
    public int TotalCount { get; private set; }
    public bool HasNextPage => CurrentPage * PageSize < TotalCount;
    public bool HasPreviousPage => CurrentPage > 1;
}