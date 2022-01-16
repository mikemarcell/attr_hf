using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using TodoApp.Shared.Dto;
using TodoApp.Shared.Interface;
using TodoApp.WebApi.Auth;

namespace TodoApp.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class TodoItemController : ControllerBase
{
    private readonly ITodoItemService todoItemService;
    private readonly IAuthorizationService authorizationService;

    public TodoItemController(ITodoItemService todoItemService, IAuthorizationService authorizationService)
    {
        this.todoItemService = todoItemService;
        this.authorizationService = authorizationService;
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
    [SwaggerResponse(403, "Unauthorized")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var item = todoItemService.GetById(id);
        
        if (item == null)
        {
            return NotFound();
        }

        var authResult = await authorizationService.AuthorizeAsync(User, id, PolicyNames.OwnerPolicy);
        
        if (!authResult.Succeeded)
        {
            return Unauthorized();
        }

        return Ok(item);
    }

    [HttpGet]
    [Authorize(Policy = PolicyNames.AdminOnlyPolicy)]
    [SwaggerOperation("Get all todo items")]
    [SwaggerResponse(403, "Unauthorized")]
    public IActionResult GetAll()
    {
        return Ok(todoItemService.GetAll());
    }

    [HttpPut]
    [Route("{id}")]
    [SwaggerOperation("Update todo item")]
    [SwaggerResponse(403, "Unauthorized")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] TodoItemDto item)
    {
        var authResult = await authorizationService.AuthorizeAsync(User, id, PolicyNames.OwnerPolicy);

        if (!authResult.Succeeded)
        {
            return Unauthorized();
        }

        return Ok(todoItemService.Update(item));
    }

    [HttpDelete]
    [Route("{id}")]
    [SwaggerOperation("Delete todo item")]
    [SwaggerResponse(403, "Unauthorized")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var authResult = await authorizationService.AuthorizeAsync(User, id, PolicyNames.OwnerPolicy);

        if (!authResult.Succeeded)
        {
            return Unauthorized();
        }

        return Ok(todoItemService.Delete(id));
    }
}
