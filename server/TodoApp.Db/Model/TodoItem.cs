namespace TodoApp.Db.Model
{
    public class TodoItem
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public int OwnerId { get; set; }

        public User Owner { get; set; }
    }
}
