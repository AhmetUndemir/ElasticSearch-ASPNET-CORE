using ElasticSearch.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
