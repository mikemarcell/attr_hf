using AutoMapper;
using Microsoft.Extensions.Logging;
using TodoApp.Db.Model;
using TodoApp.Shared.Dto;
using TodoApp.Shared.Interface;

namespace TodoApp.Db.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly TodoContext todoContext;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public TodoItemRepository(TodoContext todoContext, IMapper mapper, ILogger<TodoItemRepository> logger)
        {
            this.todoContext = todoContext;
            this.mapper = mapper;
            this.logger = logger;
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
            logger.LogInformation($"Adding new todo item: {item.Title}");
            var entity = mapper.Map<TodoItem>(item);
            var res = todoContext.TodoItems.Add(entity);
            todoContext.SaveChanges();
            return mapper.Map<TodoItemDto>(res.Entity);
        }

        public bool Update(TodoItemDto item)
        {
            var dbItem = todoContext.TodoItems.Find(item.Id);
            if (dbItem == null || item.Title == dbItem.Title)
            {
                return false;
            }

            logger.LogInformation($"Updating todo item id={item.Id}, {dbItem.Title} >>> {item.Title}");

            dbItem.Title = item.Title;
            todoContext.TodoItems.Update(dbItem);
            todoContext.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            logger.LogInformation($"Deleting todo item id={id}");
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
