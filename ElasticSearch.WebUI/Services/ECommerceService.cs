using AutoMapper;
using ElasticSearch.WebUI.Repositories;
using ElasticSearch.WebUI.ViewModels;

namespace ElasticSearch.WebUI.Services;

public class ECommerceService
{
    private readonly ECommerceRepository _repository;

    public ECommerceService(ECommerceRepository repository)
    {
        _repository = repository;
    }

    public async Task<(List<ECommerceViewModel> list, long totalCount, long pageLinkCount)> SearchAsync(ECommerceSearchFormViewModel searchViewModel, int page, int pageSize)
    {
        var (eCommerceList, totalCount) = await _repository.SearchAsync(searchViewModel, page, pageSize);

        var pageLinkCountCalculate = totalCount % pageSize;
        long pageLinkCount = 0;

        if (pageLinkCountCalculate == 0)
            pageLinkCount = totalCount / pageSize;
        else
            pageLinkCount = (totalCount / pageSize) + 1;


        var eCommerceListViewModel = eCommerceList.Select(x => new ECommerceViewModel()
        {
            Category = string.Join(",", x.Category),
            CustomerFirstName = x.CustomerFirstName,
            CustomerFullName = x.CustomerFullName,
            CustomerLastName = x.CustomerLastName,
            Gender = x.Gender.ToLower(),
            Id = x.Id,
            OrderDate = x.OrderDate.ToShortDateString(),
            OrderId = x.OrderId,
            TaxfulTotalPrice = x.TaxfulTotalPrice
        }).ToList();

        return (eCommerceListViewModel, totalCount, pageLinkCount);
    }
}
