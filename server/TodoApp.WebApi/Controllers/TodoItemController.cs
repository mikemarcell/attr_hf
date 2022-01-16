using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using TodoApp.Shared.Dto;
using TodoApp.Shared.Interface;

namespace TodoApp.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class TodoItemController : ControllerBase
{
    private readonly ITodoItemService todoItemService;

    public TodoItemController(ITodoItemService todoItemService)
    {
        this.todoItemService = todoItemService;
    }

    [HttpPost]
    [SwaggerOperation("Add a new todo item")]
    public IActionResult Create([FromBody] TodoItemDto todoItem)
    {
        todoItem.OwnerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        return Ok(todoItemService.Create(todoItem));
    }

    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation("Get todo item by id")]
    [SwaggerResponse(404, "Todo item not found")]
    public IActionResult GetById(int id)
    {
        var item = todoItemService.GetById(id);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }

    [HttpGet]
    [SwaggerOperation("Get all todo items")]
    [Authorize(Policy = "AdminOnly")]
    public IActionResult GetAll()
    {
        return Ok(todoItemService.GetAll());
    }

    [HttpPut]
    [SwaggerOperation("Update todo item")]
    public IActionResult Update(TodoItemDto item)
    {
        return Ok(todoItemService.Update(item));
    }

    [HttpDelete]
    [SwaggerOperation("Delete todo item")]
    public IActionResult Delete(int id)
    {
        return Ok(todoItemService.Delete(id));
    }
}
