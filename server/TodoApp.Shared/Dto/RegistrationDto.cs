namespace TodoApp.Shared.Dto
{
    public class RegistrationDto
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }
    }
}
