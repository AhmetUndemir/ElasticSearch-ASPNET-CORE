using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
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

        public async Task<List<Blog>> SearchAsync(string searchText)
        {
            List<Action<QueryDescriptor<Blog>>> ListQuery = new List<Action<QueryDescriptor<Blog>>>();

            Action<QueryDescriptor<Blog>> matchAll = (q) => q.MatchAll();

            Action<QueryDescriptor<Blog>> matchContent = (q) => q.Match(m => m.Field(f => f.Content).Query(searchText));

            Action<QueryDescriptor<Blog>> titleMatchBoolPrefix = (q) => q.MatchBoolPrefix(m => m.Field(f => f.Title).Query(searchText));


            if (string.IsNullOrEmpty(searchText))
            {
                ListQuery.Add(matchAll);
            }
            else
            {
                ListQuery.Add(matchContent);
                ListQuery.Add(titleMatchBoolPrefix);
            }


            var result = await _client.SearchAsync<Blog>(s => s
                                  .Index(indexName)
                                  .Size(1000)
                                  .Query(q => q
                                  .Bool(b => b
                                  .Should(ListQuery.ToArray()))));


            foreach (var hit in result.Hits) hit.Source.Id = hit.Id;

            return result.Documents.ToList();

        }

    }
}
