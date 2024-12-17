using CategoryService.Data;
using CategoryService.DTOs;
using CategoryService.Interfaces;
using CategoryService.Models;
using Microsoft.EntityFrameworkCore;

namespace CategoryService.Services
{
    public class CategoryRepositoryService : ICategoryRepository
    {
        private readonly CategoryContext _context;

        public CategoryRepositoryService(CategoryContext context)
        {
            _context = context;
        }
        public async Task<ICollection<CategoryDTO>> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();

            var categoriesDto = categories.Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
            }).ToList();

            return categoriesDto;            
        }

        public async Task<CategoryDTO?> GetCategoryById(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null) return null;

            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
            };
        }

        public async Task<CategoryDTO> CreateCategory(AddCategoryDTO categoryDTO)
        {
            var category = new Category
            {
                Name = categoryDTO.Name,
            };

            var categoryAdded = await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return new CategoryDTO
            {
                Id = categoryAdded.Entity.Id,
                Name = categoryAdded.Entity.Name
            };
        }

        public async Task UpdateCategory(int id, AddCategoryDTO categoryDTO)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id) ?? throw new KeyNotFoundException("Categoria no encontrada");

            category.Name = categoryDTO.Name;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id) ?? throw new KeyNotFoundException("Categoria no encontrada");

            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();
        }
    }
}
