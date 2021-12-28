using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TodoApp.Db.Dto;
using TodoApp.Db.Repositories;

namespace TodoApp.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> logger;
    private readonly IUserService userService;
    private readonly ITodoItemService todoItemService;

    public UserController(ILogger<UserController> logger, IUserService userService, ITodoItemService todoItemService)
    {
        this.logger = logger;
        this.userService = userService;
        this.todoItemService = todoItemService;
    }

    [HttpPost]
    [SwaggerOperation("Add a new user")]
    public IActionResult Create([FromBody] UserDto todoItem)
    {
        return Ok(userService.Create(todoItem));
    }

    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation("Get users by id")]
    public IActionResult GetById(int id)
    {
        return Ok(userService.GetById(id));
    }

    [HttpGet]
    [SwaggerOperation("Get all users")]
    public IActionResult GetAll(int id)
    {
        return Ok(userService.GetById(id));
    }

    [HttpPut]
    [SwaggerOperation("Update a user")]
    public IActionResult Update(UserDto item)
    {
        return Ok(userService.Update(item));
    }

    [HttpDelete]
    [SwaggerOperation("Delete a user")]
    public IActionResult Delete(int id)
    {
        return Ok(userService.Delete(id));
    }

    [HttpGet]
    [Route("{id}/TodoItems")]
    [SwaggerOperation("Get todo items by owner")]
    public IActionResult GetTodoItemsByOwnerId(int id)
    {
        return Ok(todoItemService.GetByOwnerId(id));
    }
}
