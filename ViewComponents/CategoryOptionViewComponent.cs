using EMIM.Services;
using EMIM.ViewModel;
using EMIM.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EMIM.ViewComponents
{
    public class CategoryOptionViewComponent:ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public CategoryOptionViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            var categoriesVMs = categories.Select(p => new CategoryViewModel
            {
                Name = p.Name,
            }).ToList();

            return View(categoriesVMs);
        }

    }
}
