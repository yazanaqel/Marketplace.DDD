namespace Domain.Entities.Images;
public class ImageInput {
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public Stream? Content { get; set; }
}
