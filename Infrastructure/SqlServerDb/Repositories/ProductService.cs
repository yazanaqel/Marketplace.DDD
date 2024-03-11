using Domain;
using Domain.Domain.Products;
using Domain.Entities.Images;
using Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace Infrastructure.SqlServer.Repositories;
internal class ProductService(MarketplaceDbContext dbContext) : IProductService {
    private readonly MarketplaceDbContext _dbContext = dbContext;
    private const int thumbnailWidth = 300;
    public async Task<ApplicationResponse<Product>> CreateProduct(Product product) {

        var response = new ApplicationResponse<Product>();
        try {

            await _dbContext.Products.AddAsync(product);

            if (product.Images is not null) {

                await ImagesProcess(product.Images.Select(image => new ImageInput {
                    Name = image.FileName,
                    Type = image.ContentType,
                    Content = image.OpenReadStream()
                }), product.ProductId);

            }

            //product.ProductMainImage = string.Empty;

            response.Data = product;
        }
        catch (Exception e) {

            Console.WriteLine(e.Message);
        }

        response.Success = true;
        return response;
    }

    public Task<ApplicationResponse<Product>> DeleteProduct(ProductId productId, string userId) {
        throw new NotImplementedException();
    }

    public Task<ApplicationResponse<Product>> UpdateProduct(Product product, string userId) {
        throw new NotImplementedException();
    }
    private Task<List<string>> GetProductImagesPaths(ProductId productId) {

        IQueryable<ProductImage> paths = _dbContext
            .ProductImages
            .Where(x => x.ProductId == productId);

        if (paths.Any()) {
            return paths.Select(x => x.Folder + "Thumbnail_" + x.ImageId + ".jpg").ToListAsync();
        }
        else {
            return Task.FromResult(new List<string>());
        }
    }
    private async Task ImagesProcess(IEnumerable<ImageInput> images, ProductId productId) {

        var totalImages = await _dbContext.ProductImages.CountAsync();

        var tasks = images
            .Select(image => Task.Run(async () => {
                try {
                    using var imageResult = await Image.LoadAsync(image.Content);

                    var imageId = ImageId.NewImageId;
                    var folder = $"/images/{totalImages % 1000}/";
                    var name = $"{imageId}.jpg";
                    var storagePath = Path.Combine(
                        Directory.GetCurrentDirectory(), $"wwwroot{folder}".Replace("/", "\\"));

                    if (!Directory.Exists(storagePath)) {
                        Directory.CreateDirectory(storagePath);
                    }

                    await SaveImage(imageResult, $"Thumbnail_{name}", storagePath, thumbnailWidth);

                    _dbContext.ProductImages.Add(new ProductImage(
                        imageId,
                        folder,
                        productId
                    ));

                }
                catch {


                }

            }))
            .ToList();

        await Task.WhenAll(tasks);
    }

    private async Task SaveImage(Image image, string name, string path, int resizeWidth) {
        var width = image.Width;
        var height = image.Height;

        if (width > resizeWidth) {
            height = (int)((double)resizeWidth / width * height);
            width = resizeWidth;
        }

        image.Mutate(i => i.Resize(new Size(width, height)));

        image.Metadata.ExifProfile = null;

        await image.SaveAsJpegAsync($"{path}/{name}", new JpegEncoder {
            Quality = 75
        });

    }

}
