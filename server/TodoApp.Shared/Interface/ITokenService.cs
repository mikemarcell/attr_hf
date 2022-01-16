using TodoApp.Shared.Dto;

namespace TodoApp.Shared.Interface
{
    public interface ITokenService
    {
        string BuildToken(string key, string issuer, UserDto user);
    }
}
