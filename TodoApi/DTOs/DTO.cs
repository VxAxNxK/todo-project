using System.ComponentModel.DataAnnotations;
namespace TodoApi.DTOs;

public class CategoryDTO
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
    public string Name { get; set; } = "";
    
    public List<TodoDTO> Todos { get; set; } = new();
}

public class TodoDTO
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Title is required")]
    [MaxLength(50, ErrorMessage = "Title cannot exceed 50 characters")]
    public string Title { get; set; } = "";
    
    public bool IsCompleted { get; set; }
    public int CategoryId { get; set; }
}