using ElasticSearch.API.Models;

namespace ElasticSearch.API.DTOs;

public record ProductCreateDto(string Name, decimal Price, int Stock, ProductFeatureDto? Feature)
{
    public Product CreateProduct()
    {
        var product = new Product()
        {
            Name = Name,
            Price = Price,
            Stock = Stock,
            Feature = new ProductFeature()
            {
                Width = Feature?.Width ?? 0,
                Heigth = Feature?.Heigth ?? 0,
                Color = Feature?.Color != null ? (Enum_Color)int.Parse(Feature.Color) : Enum_Color.Red
            }
        };

        return product;
    }
}

