using TodoApp.Db.Dto;

namespace TodoApp.Db.Repositories
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository todoItemRepository;

        public TodoItemService(ITodoItemRepository todoItemRepository)
        {
            this.todoItemRepository = todoItemRepository;
        }

        public TodoItemDto Create(TodoItemDto item)
        {
            return todoItemRepository.Create(item);
        }

        public bool Delete(int id)
        {
            return todoItemRepository.Delete(id);
        }

        public IEnumerable<TodoItemDto> GetAll()
        {
            return todoItemRepository.GetAll();
        }

        public TodoItemDto GetById(int id)
        {
            return todoItemRepository.GetById(id);
        }

        public IEnumerable<TodoItemDto> GetByOwnerId(int ownerId)
        {
            return todoItemRepository.GetByOwnerId(ownerId);
        }

        public bool Update(TodoItemDto item)
        {
            return todoItemRepository.Update(item);
        }
    }
}
