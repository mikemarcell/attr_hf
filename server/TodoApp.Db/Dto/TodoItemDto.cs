namespace TodoApp.Db.Dto
{
    public class TodoItemDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public int OwnerId { get; set; }
    }
}
