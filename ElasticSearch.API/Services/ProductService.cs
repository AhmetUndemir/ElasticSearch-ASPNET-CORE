using ElasticSearch.API.DTOs;
using ElasticSearch.API.Models;
using ElasticSearch.API.Repositories;
using System.Collections.Immutable;
using System.Net;

namespace ElasticSearch.API.Services;

public class ProductService
{
    private readonly ProductRepository _productRepository;

    public ProductService(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ResponseDto<ProductDto>> SaveAsync(ProductCreateDto request)
    {
        var responseProduct = await _productRepository.SaveAsync(request.CreateProduct());

        if (responseProduct is null) return ResponseDto<ProductDto>.Fail("Kayıt esnasında hata meydana geldi.", HttpStatusCode.BadRequest);

        return ResponseDto<ProductDto>.Success(responseProduct.CreateDto(), HttpStatusCode.Created);
    }

    public async Task<ResponseDto<List<ProductDto>>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();

        var productListDto = products.Select(a => new ProductDto(a.Id, a.Name, a.Price, a.Stock, a.Created, a.Updated,
            new ProductFeatureDto(a.Feature?.Width, a.Feature?.Heigth, a.Feature?.Color.ToString()))).ToList();

        return ResponseDto<List<ProductDto>>.Success(productListDto, HttpStatusCode.OK);
    }

    public async Task<ResponseDto<ProductDto>> GetByIdAsync(string id)
    {
        var hasProduct = await _productRepository.GetByIdAsync(id);

        if (hasProduct is null) return ResponseDto<ProductDto>.Fail("Kayıt bulunamadı.", HttpStatusCode.NotFound);

        return ResponseDto<ProductDto>.Success(hasProduct.CreateDto(), HttpStatusCode.OK);
    }
}
