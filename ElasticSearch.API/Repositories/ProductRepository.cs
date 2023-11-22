using Elastic.Clients.Elasticsearch;
using ElasticSearch.API.Models;
using Nest;

namespace ElasticSearch.API.Repositories;

public class ProductRepository
{
    private readonly ElasticsearchClient _client;
    private const string indexName = "products11";

    public ProductRepository(ElasticsearchClient client)
    {
        _client = client;
    }

    public async Task<Product?> SaveAsync(Product newProduct)
    {
        newProduct.Created = DateTime.Now;

        var response = await _client.IndexAsync(newProduct, x => x.Index(indexName));

        if (!response.IsSuccess()) return null;

        newProduct.Id = response.Id;

        return newProduct;

    }
}
