using Domain.Entities.Products;

namespace Domain.Domain.Products;
public interface IProductService {
    Task<ApplicationResponse<Product>> CreateProduct(Product product);
    Task<ApplicationResponse<Product>> UpdateProduct(Product product, string userId);
    Task<ApplicationResponse<Product>> DeleteProduct(ProductId productId, string userId);

}
