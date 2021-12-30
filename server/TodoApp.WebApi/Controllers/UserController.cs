using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TodoApp.Shared.Dto;
using TodoApp.Shared.Interface;

namespace TodoApp.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService userService;
    private readonly ITodoItemService todoItemService;

    public UserController(IUserService userService, ITodoItemService todoItemService)
    {
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
    [SwaggerResponse(404, "User not found")]
    public IActionResult GetById(int id)
    {
        var item = userService.GetById(id);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }

    [HttpGet]
    [SwaggerOperation("Get all users")]
    public IActionResult GetAll()
    {
        return Ok(userService.GetAll());
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

    [HttpGet]
    [Route("{id}/Picture")]
    [SwaggerOperation("Get a users's picture")]
    public IActionResult GetPicture(int id)
    {
        var picture = userService.GetPicture(id);

        if (picture == null)
        {
            return NotFound();
        }

        return File(picture.Data, picture.ContentType);
    }

    [HttpPost]
    [Route("{id}/Picture")]
    [SwaggerOperation("Get a users's picture")]
    public IActionResult SetPicture(int id, IFormFile picture)
    {
        using var stream = new MemoryStream();
        picture.CopyTo(stream);
        var userPicture = new UserPictureDto { UserId = id, ContentType = picture.ContentType, Data = stream.ToArray() };
        userService.SetPicture(userPicture);
        return Ok();
    }
}
