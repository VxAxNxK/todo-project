using Microsoft.EntityFrameworkCore;
namespace TodoApi.Data;
using TodoApi.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Todo> Todos { get; set; }
    public DbSet<Category> Categories { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>()
            .HasOne(t => t.Category)
            .WithMany(c => c.Todos)
            .HasForeignKey(t => t.CategoryId);
    }
}