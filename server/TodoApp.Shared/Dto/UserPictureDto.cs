namespace TodoApp.Shared.Dto
{
    public class UserPictureDto
    {
        public int Id { get; set; }

        public string ContentType { get; set; }

        public byte[] Data { get; set; }

        public int UserId { get; set; }
    }
}
