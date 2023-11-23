using ElasticSearch.API.DTOs;

namespace ElasticSearch.API.Models;

public class Product
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public ProductFeature? Feature { get; set; }

    public ProductDto CreateDto()
    {
        if (Feature == null)
        {
            return new ProductDto(Id, Name, Price, Stock, Created, null, null);
        }

        return new ProductDto(Id, Name, Price, Stock, Created, null, new ProductFeatureDto(Feature.Width, Feature.Heigth, Feature.Color.ToString()));
    }
}
