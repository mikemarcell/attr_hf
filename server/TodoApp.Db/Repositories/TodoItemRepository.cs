using AutoMapper;
using TodoApp.Db.Model;

namespace TodoApp.Db.Repositories
{
  public class TodoItemRepository : ITodoItemRepository
  {
    private readonly TodoContext todoContext;
    private readonly IMapper mapper;

    public TodoItemRepository(TodoContext todoContext, IMapper mapper)
    {
      this.todoContext = todoContext;
      this.mapper = mapper;
    }

    public TodoItemDto GetById(int id)
    {
      var item = todoContext.Find<TodoItem>(id);
      return mapper.Map<TodoItemDto>(item);
    }

    public IEnumerable<TodoItemDto> GetAll()
    {
      var items = todoContext.TodoItems.ToArray();
      return items.Select(mapper.Map<TodoItemDto>).ToArray();
    }

    public TodoItemDto Create(TodoItemDto item)
    {
      var entity = mapper.Map<TodoItem>(item);
      var res = todoContext.TodoItems.Add(entity);
      todoContext.SaveChanges();
      return mapper.Map<TodoItemDto>(res.Entity);
    }

    public bool Update(TodoItemDto item)
    {
      var dbItem = todoContext.TodoItems.Find(item.Id);
      if (dbItem == null)
      {
        return false;
      }
      dbItem.Title = item.Title;
      todoContext.TodoItems.Update(dbItem);
      todoContext.SaveChanges();
      return true;
    }

    public bool Delete(int id)
    {
      var item = todoContext.TodoItems.Find(id);
      if (item == null)
      {
        return false;
      }
      todoContext.TodoItems.Remove(item);
      return true;
    }

    public IEnumerable<TodoItemDto> GetByOwnerId(int ownerId)
    {
      var items = todoContext.TodoItems.Where(t => t.OwnerId == ownerId).ToArray();
      return items.Select(mapper.Map<TodoItemDto>).ToArray();
    }
  }
}
