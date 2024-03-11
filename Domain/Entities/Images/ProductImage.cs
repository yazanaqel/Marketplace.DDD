using Domain.Entities.Products;

namespace Domain.Entities.Images;
public class ProductImage {
    private ProductImage() { }
    public ProductImage(ImageId imageId, string folder, ProductId productId) {

        ImageId = imageId;
        Folder = folder;
        ProductId = productId;

    }
    public ImageId ImageId { get; private set; }
    public string Folder { get; private set; } = string.Empty;
    public ProductId ProductId { get; private set; }

}
