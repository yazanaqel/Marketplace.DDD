using Domain;
using Domain.Domain.Products;

namespace Infrastructure.SqlServer.Repositories;
internal class ProductRepository(MarketplaceDbContext dbContext) : IProductRepository {
    private readonly MarketplaceDbContext _dbContext = dbContext;

    public async Task<DomainResponse<Product>> CreateProduct(Product product) {

        var response = new DomainResponse<Product>();

        try {

            await _dbContext.Products.AddAsync(product);
            response.Data = product;
        }
        catch (Exception e) {

            Console.WriteLine(e.Message);
        }

        response.Success = true;
        return response;
    }

    public Task<DomainResponse<Product>> DeleteProduct(int productId, string userId) {
        throw new NotImplementedException();
    }

    public Task<DomainResponse<IReadOnlyList<Product>>> GetAllProducts(string? sortColumn, string? sortOrder, string? searchItem, int page, int pageSize, string? userId = null) {
        throw new NotImplementedException();
    }

    public Task<DomainResponse<Product>> GetProductDetails(int productId, string? userId = null) {
        throw new NotImplementedException();
    }

    public Task<DomainResponse<Product>> UpdateProduct(Product product, string userId) {
        throw new NotImplementedException();
    }
}
