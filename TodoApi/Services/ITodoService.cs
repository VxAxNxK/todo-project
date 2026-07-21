namespace TodoApi.Services;

using TodoApi.DTOs;

public interface ITodoService
{
    Task<List<TodoDTO>> GetAllAsync();
    Task<TodoDTO?> GetByIdAsync(int id);
    Task AddAsync(TodoDTO dto);
    Task UpdateAsync(int id, TodoDTO dto);
    Task DeleteAsync(int id);
}