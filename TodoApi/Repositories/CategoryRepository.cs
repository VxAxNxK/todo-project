namespace TodoApi.Repositories;

using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _db;

    public CategoryRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Category>> GetAllAsync()
        => await _db.Categories.Include(c => c.Todos).ToListAsync();

    public async Task<Category?> GetByIdAsync(int id)
        => await _db.Categories.Include(c => c.Todos).FirstOrDefaultAsync(c => c.Id == id);

    public async Task AddAsync(Category category)
    {
        _db.Categories.Add(category);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category category)
    {
        _db.Categories.Update(category);
        await _db.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var category = await _db.Categories.FindAsync(id);
        if (category == null) return;
        _db.Categories.Remove(category);
        await _db.SaveChangesAsync();
    }
}