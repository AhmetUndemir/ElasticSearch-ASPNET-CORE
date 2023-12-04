using AutoMapper;
using ElasticSearch.WebUI.Models;
using ElasticSearch.WebUI.Repositories;
using ElasticSearch.WebUI.ViewModels;

namespace ElasticSearch.WebUI.Services
{
    public class BlogService
    {
        private readonly BlogRepository _blogRepository;
        private readonly IMapper _mapper;

        public BlogService(BlogRepository blogRepository,IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }

        public async Task<bool> SaveAsync(BlogCreateViewModel model)
        {
            var blog = new Blog
            {
                Title = model.Title,
                Content = model.Content,
                Tags = model.Tags.Split(","),
                UserId = Guid.NewGuid()
            };

            var result = await _blogRepository.SaveAsync(blog);

            return result != null;
        }
    }
}
