using System.Text.Json.Serialization;

namespace ElasticSearch.WebUI.ViewModels
{
    public class BlogViewModel
    {
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string? Tags { get; set; }
        public string UserId { get; set; } = null!;
        public string CreatedDate { get; set; } = null!;

    }
}
