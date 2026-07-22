using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace TodoApi.Controller;
using TodoApi.Models;
using TodoApi.Services;
using TodoApi.DTOs;

[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _service;
    public CategoryController(ICategoryService service)
    {
        _service = service;
    }
    
    // 查詢全部（含底下的 Todo）
    [HttpGet]
    public async Task<ActionResult> GetAll(){
        await _service.GetAllAsync();
        return Ok(ApiResponse<List<CategoryDTO>>.SuccessResponse(await _service.GetAllAsync()));
    }
    
    // 新增
    [HttpPost]
    public async Task<ActionResult> Create(CategoryDTO categoryDto)
    {
        await _service.AddAsync(categoryDto);
        return Ok();
    }

    // 刪除
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id){
        await _service.DeleteAsync(id);
        return Ok();
    }
}