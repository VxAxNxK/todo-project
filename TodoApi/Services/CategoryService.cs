namespace TodoApi.Services;

using TodoApi.DTOs;
using TodoApi.Models;
using TodoApi.Repositories;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repo;

    public CategoryService(ICategoryRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<CategoryDTO>> GetAllAsync()
    {
        var categories = await _repo.GetAllAsync();
        return categories.Select(c => new CategoryDTO
        {
            Id = c.Id,
            Name = c.Name
        }).ToList();
    }

    public async Task<CategoryDTO?> GetByIdAsync(int id)
    {
        var category = await _repo.GetByIdAsync(id);
        if (category == null) return null;
        return new CategoryDTO
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    public async Task AddAsync(CategoryDTO dto)
    {
        var category = new Category
        {
            Name = dto.Name
        };
        await _repo.AddAsync(category);
    }

    public async Task UpdateAsync(int id, CategoryDTO dto)
    {
        var category = await _repo.GetByIdAsync(id);
        if (category == null) return;
        category.Name = dto.Name;
        await _repo.UpdateAsync(category);
    }

    public async Task DeleteAsync(int id)
    {
        await _repo.DeleteAsync(id);
    }
}