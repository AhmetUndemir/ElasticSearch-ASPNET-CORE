using ElasticSearch.API.Models.ECommerceModel;
using ElasticSearch.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;

namespace ElasticSearch.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ECommerceController : ControllerBase
    {
        private readonly ECommerceRepository _eCommerceRepository;

        public ECommerceController(ECommerceRepository eCommerceRepository)
        {
            _eCommerceRepository = eCommerceRepository;
        }

        [HttpGet]
        public async Task<ActionResult> TermQuery(string customerFirstName)
        {
            if (string.IsNullOrWhiteSpace(customerFirstName))
            {
                return BadRequest();
            }

            var result = await _eCommerceRepository.TermQuery(customerFirstName);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> TermsQuery([FromBody] List<string> customerFirstNames)
        {
            if (customerFirstNames == null || customerFirstNames.Count == 0)
            {
                return BadRequest();
            }

            var result = await _eCommerceRepository.TermsQuery(customerFirstNames);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> PrefixQuery(string customerFullName)
        {
            var result = await _eCommerceRepository.PrefixQueryAsync(customerFullName);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> RangeQuery(double fromPrice, double toPrice)
        {
            var result = await _eCommerceRepository.RangeQueryAsync(fromPrice, toPrice);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> MatchAll()
        {
            var result = await _eCommerceRepository.MatchAllQueryAsync();

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> PaginationQuery(int page, int pageSize)
        {
            var result = await _eCommerceRepository.PaginationQueryAsync(page, pageSize);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> WildCardQuery(string customerFullName)
        {
            var result = await _eCommerceRepository.WildCardQueryAsync(customerFullName);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> FuzzyQuery(string customerName)
        {
            var result = await _eCommerceRepository.FuzzyQueryAsync(customerName);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> MathQueryFullText(string categoryName)
        {
            var result = await _eCommerceRepository.MathQueryFullTextAsync(categoryName);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> MathBoolFrefixFullText(string customerFullName)
        {
            var result = await _eCommerceRepository.MathBoolFrefixFullTextAsync(customerFullName);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> MathPhraseFullText(string customerFullName)
        {
            var result = await _eCommerceRepository.MathPhraseFullTextAsync(customerFullName);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> MathPhrasePrefixFullText(string cityName, double taxtFullTotalPrice, string categoryName, string menufacturer)
        {
            var result = await _eCommerceRepository.MathPhrasePrefixFullTextAsync(cityName, taxtFullTotalPrice, categoryName, menufacturer);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> MathPhrasePrefixFullText2(string customerFullName)
        {
            var result = await _eCommerceRepository.MathPhrasePrefixFullText2Async(customerFullName);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> MathMultiMatchFullText(string name)
        {
            var result = await _eCommerceRepository.MultiMatchFullTextAsync(name);

            return Ok(result);
        }
    }
}
