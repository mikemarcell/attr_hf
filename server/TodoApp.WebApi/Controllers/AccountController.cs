using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TodoApp.Shared.Dto;
using TodoApp.Shared.Interface;

namespace TodoApp.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[AllowAnonymous]
public class AccountController : ControllerBase
{
    private readonly IAccountService accountService;
    private readonly ITokenService tokenService;
    private readonly IConfiguration configuration;

    public AccountController(IAccountService accountService, ITokenService tokenService, IConfiguration configuration)
    {
        this.accountService = accountService;
        this.tokenService = tokenService;
        this.configuration = configuration;
    }

    [HttpPost]
    [SwaggerOperation("Register a new user")]
    [Route("register")]
    public IActionResult Create([FromBody] RegistrationDto registrationDto)
    {
        var user = accountService.Register(registrationDto);
        return Ok(user);
    }

    [HttpPost]
    [SwaggerOperation("Login")]
    [SwaggerResponse(401, "Login failed.")]
    [Route("login")]
    public IActionResult Login([FromBody] LoginDto loginDto)
    {
        var userDto = accountService.Login(loginDto);

        if (userDto == null)
        {
            return StatusCode(401);
        }

        var token = tokenService.BuildToken(configuration["Jwt:Key"], configuration["Jwt:Issuer"], userDto);
        return Ok(token);
    }
}
