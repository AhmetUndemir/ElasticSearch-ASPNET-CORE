using System.ComponentModel.DataAnnotations;

namespace ElasticSearch.WebUI.ViewModels
{
    public class BlogCreateViewModel
    {
        [Display(Name = "Başlık")]
        [Required]
        public string Title { get; set; } = null!;
        [Display(Name = "İçerik")]
        [Required]
        public string Content { get; set; } = null!;
        [Display(Name = "Etiketler")]
        public string Tags { get; set; } = null!;
    }
}
