using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using ElasticSearch.WebUI.Models;
using ElasticSearch.WebUI.ViewModels;

namespace ElasticSearch.WebUI.Repositories;

public class ECommerceRepository
{
    private readonly ElasticsearchClient _client;
    private const string indexName = "kibana_sample_data_ecommerce";

    public ECommerceRepository(ElasticsearchClient client)
    {
        _client = client;
    }

    public async Task<(List<ECommerce> list, long count)> SearchAsync(ECommerceSearchFormViewModel searchViewModel, int page, int pageSize)
    {
        List<Action<QueryDescriptor<ECommerce>>> listQuery = new List<Action<QueryDescriptor<ECommerce>>>();

        if (searchViewModel is null)
        {
            listQuery.Add((q) => q.MatchAll());

            return await CalculateResultSet(page, pageSize, listQuery);
        }

        if (!string.IsNullOrEmpty(searchViewModel.Category))
        {
            listQuery.Add((q) => q
            .Match(m => m
            .Field(f => f.Category)
            .Query(searchViewModel.Category)));
        }

        if (!string.IsNullOrEmpty(searchViewModel.CustomerFullName))
        {
            listQuery.Add((q) => q
            .Match(m => m
            .Field(f => f.CustomerFullName)
            .Query(searchViewModel.CustomerFullName)));
        }

        if (searchViewModel.OrderDateStart.HasValue)
        {
            listQuery.Add((q) => q
            .Range(r => r
            .DateRange(d => d
            .Field(f => f.OrderDate)
            .Gte(searchViewModel.OrderDateStart.Value))));
        }

        if (searchViewModel.OrderDateEnd.HasValue)
        {
            listQuery.Add((q) => q
            .Range(r => r
            .DateRange(d => d
            .Field(f => f.OrderDate)
            .Lte(searchViewModel.OrderDateEnd.Value))));
        }

        if (!string.IsNullOrEmpty(searchViewModel.Gender))
        {
            listQuery.Add((q) => q.Term(t => t.Field(f => f.Gender).Value(searchViewModel.Gender).CaseInsensitive()));
        }

        if (!listQuery.Any())
        {
            listQuery.Add((q) => q.MatchAll());
        }

        return await CalculateResultSet(page, pageSize, listQuery);
    }

    public async Task<(List<ECommerce>, long count)> CalculateResultSet(int page, int pageSize, List<Action<QueryDescriptor<ECommerce>>> listQuery)
    {
        var pageFrom = (page - 1) * pageSize;

        var result = await _client.SearchAsync<ECommerce>(s => s
                      .Index(indexName)
                      .Size(pageSize)
                      .From(pageFrom)
                      .Query(q => q
                      .Bool(b => b
                      .Must(listQuery.ToArray()))));

        foreach (var hit in result.Hits) hit.Source.Id = hit.Id;

        return (result.Documents.ToList(), result.Total);

    }
}
