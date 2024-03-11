namespace Domain;
public class ApplicationResponse<T> {

    public T? Data { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool Success { get; set; }
    public PageInfo? PageInfo { get; set; }
}
