namespace Domain;
public class DomainResponse<T> {
    public DomainResponse() { }

    public DomainResponse(int currentPage, int pageSize, int totalCount) {
        CurrentPage = currentPage;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    public T? Data { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;

    private int CurrentPage { get; set; }
    private int PageSize { get; set; }
    public int TotalCount { get; private set; }
    public bool HasNextPage => CurrentPage * PageSize < TotalCount;
    public bool HasPreviousPage => CurrentPage > 1;

}
