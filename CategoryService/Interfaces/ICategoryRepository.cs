using CategoryService.DTOs;

namespace CategoryService.Interfaces
{
    public interface ICategoryRepository
    {
        Task<ICollection<CategoryDTO>> GetCategories();
        Task<CategoryDTO?> GetCategoryById(int id);
        Task<CategoryDTO> CreateCategory(AddCategoryDTO categoryDTO);
        Task UpdateCategory(int id, AddCategoryDTO categoryDTO);
        Task DeleteCategory(int id);
    }
}
