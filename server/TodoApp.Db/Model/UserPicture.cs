namespace TodoApp.Db.Model
{
    public class UserPicture
    {
        public int Id { get; set; }

        public string ContentType { get; set; }

        public byte[] Data { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
