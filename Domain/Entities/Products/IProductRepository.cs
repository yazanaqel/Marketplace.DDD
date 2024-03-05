namespace Domain.Domain.Products;
public interface IProductRepository {
    Task<DomainResponse<IReadOnlyList<Product>>> GetAllProducts(string? sortColumn, string? sortOrder, string? searchItem, int page, int pageSize, string? userId = default);
    Task<DomainResponse<Product>> GetProductDetails(int productId, string? userId = default);

    //

    Task<DomainResponse<Product>> CreateProduct(Product product);
    Task<DomainResponse<Product>> UpdateProduct(Product product, string userId);
    Task<DomainResponse<Product>> DeleteProduct(int productId, string userId);
}
