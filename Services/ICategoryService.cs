using EMIM.Models;

namespace EMIM.Services
{
    public interface ICategoryService

    {
        Task<List<Category>> GetAllCategoriesAsync();
    }
}
