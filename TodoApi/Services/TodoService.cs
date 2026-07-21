namespace TodoApi.Services;

using TodoApi.DTOs;
using TodoApi.Models;
using TodoApi.Repositories;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _repo;

    public TodoService(ITodoRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<TodoDTO>> GetAllAsync()
    {
        var todos = await _repo.GetAllAsync();
        return todos.Select(t => new TodoDTO
        {
            Id = t.Id,
            Title = t.Title,
            IsCompleted = t.IsCompleted,
            CategoryId = t.CategoryId
        }).ToList();
    }

    public async Task<TodoDTO?> GetByIdAsync(int id)
    {
        var todo = await _repo.GetByIdAsync(id);
        if (todo == null) return null;
        return new TodoDTO
        {
            Id = todo.Id,
            Title = todo.Title,
            IsCompleted = todo.IsCompleted
        };
    }

    public async Task AddAsync(TodoDTO dto)
    {
        var todo = new Todo
        {
            Title = dto.Title,
            IsCompleted = dto.IsCompleted,
            CategoryId = dto.CategoryId
        };
        await _repo.AddAsync(todo);
    }

    public async Task UpdateAsync(int id, TodoDTO dto)
    {
        var todo = await _repo.GetByIdAsync(id);
        if (todo == null)
            throw new KeyNotFoundException($"找不到 Id 為 {id} 的 Todo");

        todo.Title = dto.Title;
        todo.IsCompleted = dto.IsCompleted;
        await _repo.UpdateAsync(todo);
    }

    public async Task DeleteAsync(int id)
    {
        await _repo.DeleteAsync(id);
    }
}