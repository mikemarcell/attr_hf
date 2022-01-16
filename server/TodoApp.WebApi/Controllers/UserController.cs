using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TodoApp.Shared.Dto;
using TodoApp.Shared.Interface;
using TodoApp.WebApi.Auth;

namespace TodoApp.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService userService;
    private readonly ITodoItemService todoItemService;
    private readonly IAuthorizationService authorizationService;

    public UserController(
        IUserService userService,
        ITodoItemService todoItemService,
        IAuthorizationService authorizationService)
    {
        this.userService = userService;
        this.todoItemService = todoItemService;
        this.authorizationService = authorizationService;
    }

    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation("Get users by id")]
    [SwaggerResponse(404, "User not found")]
    [SwaggerResponse(403, "Unauthorized")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var item = userService.GetById(id);
        if (item == null)
        {
            return NotFound();
        }

        var authResult = await authorizationService.AuthorizeAsync(User, id, PolicyNames.UserPolicy);

        if (!authResult.Succeeded)
        {
            return Unauthorized();
        }

        return Ok(item);
    }

    [HttpGet]
    [Authorize(Policy = PolicyNames.AdminOnlyPolicy)]
    [SwaggerOperation("Get all users")]
    public IActionResult GetAll()
    {
        return Ok(userService.GetAll());
    }

    [HttpPut]
    [Route("{id}")]
    [SwaggerOperation("Update a user")]
    [SwaggerResponse(403, "Unauthorized")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody]UserDto item)
    {
        var authResult = await authorizationService.AuthorizeAsync(User, id, PolicyNames.UserPolicy);

        if (!authResult.Succeeded)
        {
            return Unauthorized();
        }

        return Ok(userService.Update(item));
    }

    [HttpDelete]
    [SwaggerOperation("Delete a user")]
    [SwaggerResponse(403, "Unauthorized")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var authResult = await authorizationService.AuthorizeAsync(User, id, PolicyNames.UserPolicy);

        if (!authResult.Succeeded)
        {
            return Unauthorized();
        }

        return Ok(userService.Delete(id));
    }

    [HttpGet]
    [Route("{id}/TodoItems")]
    [SwaggerOperation("Get todo items by owner")]
    [SwaggerResponse(403, "Unauthorized")]
    public async Task<IActionResult> GetTodoItemsByOwnerIdAsync(int id)
    {
        var authResult = await authorizationService.AuthorizeAsync(User, id, PolicyNames.UserPolicy);

        if (!authResult.Succeeded)
        {
            return Unauthorized();
        }

        return Ok(todoItemService.GetByOwnerId(id));
    }

    [HttpGet]
    [Route("{id}/Picture")]
    [SwaggerOperation("Get a users's picture")]
    [SwaggerResponse(404, "Not found")]
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
    [SwaggerResponse(403, "Unauthorized")]
    public async Task<IActionResult> SetPictureAsync(int id, IFormFile picture)
    {
        var authResult = await authorizationService.AuthorizeAsync(User, id, PolicyNames.UserPolicy);

        if (!authResult.Succeeded)
        {
            return Unauthorized();
        }

        using var stream = new MemoryStream();
        picture.CopyTo(stream);
        var userPicture = new UserPictureDto { UserId = id, ContentType = picture.ContentType, Data = stream.ToArray() };
        userService.SetPicture(userPicture);
        return Ok();
    }
}
