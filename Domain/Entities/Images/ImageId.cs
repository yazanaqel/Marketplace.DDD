namespace Domain.Entities.Images;
public readonly record struct ImageId(Guid Value) {
    public static ImageId Empty => new(Guid.Empty);
    public static ImageId NewImageId => new(Guid.NewGuid());
};