using TodoApp.Shared.Dto;

namespace TodoApp.Shared.Interface
{
    public interface IAccountService
    {
        UserDto? Login(LoginDto loginDto);

        UserDto Register(RegistrationDto registrationDto);
    }
}
