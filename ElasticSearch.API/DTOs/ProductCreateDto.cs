using ElasticSearch.API.Models;

namespace ElasticSearch.API.DTOs;

public record ProductCreateDto(string Name, decimal Price, int Stock, ProductFeatureDto? Feature)
{
    public Product CreateProduct() => new()
    {
        Name = Name,
        Price = Price,
        Stock = Stock,
        Feature = new ProductFeature() { Width = Feature?.Width ?? 0, Heigth = Feature?.Heigth ?? 0, Color = Feature?.Color ?? Enum_Color.Red }
    };
}
