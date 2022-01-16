using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using TodoApp.Shared.Dto;
using TodoApp.Shared.Interface;

namespace TodoApp.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserService userService;

        public AccountService(IUserService userService)
        {
            this.userService = userService;
        }
        public UserDto? Login(LoginDto loginDto)
        {
            var userDto = userService.GetByEmail(loginDto.Email);

            if (userDto == null || !ValidatePassword(userDto, loginDto.Password))
            {
                return null;
            }

            return userDto;
        }

        private bool ValidatePassword(UserDto userDto, string password)
        {
            return userDto.PasswordHash == GeneratePasswordHash(password, userDto.PasswordSalt);
        }

        public UserDto Register(RegistrationDto registrationDto)
        {
            var salt = GenerateSalt();
            var hash = GeneratePasswordHash(registrationDto.Password, salt);

            var user = new UserDto
            {
                Email = registrationDto.Email,
                IsAdmin = registrationDto.IsAdmin,
                Name = registrationDto.Name,
                PasswordSalt = salt,
                PasswordHash = hash
            };

            return userService.Create(user);
        }

        // https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-6.0
        private string GeneratePasswordHash(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
        }

        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            return salt;
        }
    }
}
