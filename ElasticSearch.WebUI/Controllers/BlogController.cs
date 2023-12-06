using ElasticSearch.WebUI.Models;
using ElasticSearch.WebUI.Services;
using ElasticSearch.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearch.WebUI.Controllers
{
    public class BlogController : Controller
    {

        private readonly BlogService _blogService;

        public BlogController(BlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IActionResult> Search()
        {
            return View(await _blogService.SearchAsync(string.Empty));
        }

        [HttpPost]
        public async Task<IActionResult> Search(string searchText)
        {
            ViewBag.SearchText = searchText;
            return View(await _blogService.SearchAsync(searchText));
        }

        public IActionResult Save()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(BlogCreateViewModel model)
        {
            var result = await _blogService.SaveAsync(model);

            if (!result)
            {
                TempData["result"] = "Kayıt işlemi başarısız oldu.";
                return RedirectToAction(nameof(Save));
            }

            TempData["result"] = "Kayıt işlemi başarılı oldu.";
            return RedirectToAction(nameof(Save));
        }

    }
}
