using Elastic.Clients.Elasticsearch;
using ElasticSearch.API.DTOs;
using ElasticSearch.API.Models;
using ElasticSearch.API.Repositories;
using System.Collections.Immutable;
using System.Net;

namespace ElasticSearch.API.Services;

public class ProductService
{
    private readonly ProductRepository _productRepository;
    private readonly ILogger<ProductService> _logger;

    public ProductService(ProductRepository productRepository, ILogger<ProductService> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
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

    public async Task<ResponseDto<bool>> UpdateAsync(ProductUpdateDto request)
    {
        var isSuccess = await _productRepository.UpdateAsync(request);

        if (!isSuccess)
        {
            return ResponseDto<bool>.Fail("Güncelleme esanasında bir hata meydana geldi.", HttpStatusCode.InternalServerError);
        }

        return ResponseDto<bool>.Success(true, HttpStatusCode.NoContent);
    }

    public async Task<ResponseDto<bool>> DeleteAsync(string id)
    {
        var deleteResponse = await _productRepository.DeleteAsync(id);

        if (!deleteResponse.IsSuccess() && deleteResponse.Result == Result.NotFound)
        {
            return ResponseDto<bool>.Fail("Silmeye çalıştığınız ürün bulunamamıştır.", HttpStatusCode.NotFound);
        }

        if (!deleteResponse.IsSuccess())
        {
            deleteResponse.TryGetOriginalException(out Exception? exception);
            _logger.LogError(exception, deleteResponse.ElasticsearchServerError.Error.ToString());
            return ResponseDto<bool>.Fail("Silme esnasında bir hata meydana geldi.", HttpStatusCode.InternalServerError);
        }

        return ResponseDto<bool>.Success(true, HttpStatusCode.NoContent);
    }
}
