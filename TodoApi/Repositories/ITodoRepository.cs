namespace TodoApi.Repositories;

using TodoApi.Models;

public interface ITodoRepository
{
    Task<List<Todo>> GetAllAsync();
    Task<Todo?> GetByIdAsync(int id);
    Task AddAsync(Todo todo);
    Task UpdateAsync(Todo todo);
    Task DeleteAsync(int id);
}