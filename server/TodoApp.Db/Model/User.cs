namespace TodoApp.Db.Model
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public bool IsAdmin { get; set; }

        public string PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public IList<TodoItem> TodoItems { get; set; } = new List<TodoItem>();

        public UserPicture UserPicture { get; set; }
    }
}
