namespace TodoApi.Services;

using TodoApi.DTOs;

public interface ICategoryService
{
    Task<List<CategoryDTO>> GetAllAsync();
    Task<CategoryDTO?> GetByIdAsync(int id);
    Task AddAsync(CategoryDTO dto);
    Task UpdateAsync(int id, CategoryDTO dto);
    Task DeleteAsync(int id);
}