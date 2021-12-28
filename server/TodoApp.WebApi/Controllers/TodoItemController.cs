using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TodoApp.Db.Model;
using TodoApp.Db.Repositories;

namespace TodoApp.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoItemController : ControllerBase
{
    private readonly ILogger<TodoItemController> logger;
    private readonly ITodoItemService todoItemService;

    public TodoItemController(ILogger<TodoItemController> logger, ITodoItemService todoItemService)
    {
        this.logger = logger;
        this.todoItemService = todoItemService;
    }

    [HttpPost]
    [SwaggerOperation("Add a new todo item")]
    public IActionResult Create([FromBody] TodoItemDto todoItem)
    {
        return Ok(todoItemService.Create(todoItem));
    }

    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation("Get todo item by id")]
    public IActionResult GetById(int id)
    {
        return Ok(todoItemService.GetById(id));
    }

    [HttpGet]
    [SwaggerOperation("Get all todo items")]
    public IActionResult GetAll(int id)
    {
        return Ok(todoItemService.GetById(id));
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
