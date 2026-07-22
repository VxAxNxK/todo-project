using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace TodoApi.Controller;
using TodoApi.Models;
//using TodoApi.Data;
using TodoApi.DTOs;
using TodoApi.Services;

[ApiController]
[Route("api/todos")] //[Route("api/[controller]")]會是取用Controller的名稱來當作路由的一部分，這裡是TodoController，所以路由會是api/todos
public class TodoController : ControllerBase
{
    private readonly ITodoService _service;
    public TodoController(ITodoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var todos = await _service.GetAllAsync();
        return Ok(ApiResponse<List<TodoDTO>>.SuccessResponse(todos));
    }
    
    [HttpPost]
    public async Task<ActionResult> Create(TodoDTO todoDto){
        await _service.AddAsync(todoDto);
        return Ok();
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var todo = await _service.GetByIdAsync(id);
        if (todo == null)
            return NotFound(ApiResponse<TodoDTO>.FailureResponse("找不到這筆資料"));
        return Ok(ApiResponse<TodoDTO>.SuccessResponse(todo));
    }    
    
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, TodoDTO todoDto)
    {
        await _service.UpdateAsync(id, todoDto);
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id){
        await _service.DeleteAsync(id);
        return Ok();
    }

}
