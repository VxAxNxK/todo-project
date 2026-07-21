namespace TodoApi.Repositories;

using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

public class TodoRepository : ITodoRepository
{
    private readonly AppDbContext _db;

    public TodoRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Todo>> GetAllAsync()
        => await _db.Todos.Include(t => t.Category).ToListAsync();

    public async Task<Todo?> GetByIdAsync(int id)
        => await _db.Todos.Include(t => t.Category).FirstOrDefaultAsync(t => t.Id == id);

    public async Task AddAsync(Todo todo)
    {
        _db.Todos.Add(todo);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Todo todo)
    {
        _db.Todos.Update(todo);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var todo = await _db.Todos.FindAsync(id);
        if (todo == null) return;
        _db.Todos.Remove(todo);
        await _db.SaveChangesAsync();
    }
}