using TodoApp.Shared.Dto;

namespace TodoApp.Shared.Interface
{
    public interface ITodoItemRepository
    {
        TodoItemDto Create(TodoItemDto item);

        bool Delete(int id);

        IEnumerable<TodoItemDto> GetAll();

        TodoItemDto GetById(int id);

        bool Update(TodoItemDto item);

        IEnumerable<TodoItemDto> GetByOwnerId(int ownerId);
    }
}