using Elastic.Clients.Elasticsearch;
using ElasticSearch.WebUI.Models;

namespace ElasticSearch.WebUI.Repositories
{
    public class BlogRepository
    {
        private readonly ElasticsearchClient _client;
        private const string indexName = "blog";

        public BlogRepository(ElasticsearchClient client)
        {
            _client = client;
        }


        public async Task<Blog?> SaveAsync(Blog blog)
        {
            blog.CreatedDate = DateTime.Now;

            var response = await _client.IndexAsync(blog, x => x.Index(indexName));

            if (!response.IsValidResponse) return null;

            blog.Id = response.Id;

            return blog;
        }
    }
}
