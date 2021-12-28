using AutoMapper;
using TodoApp.Db.Dto;
using TodoApp.Db.Model;

namespace TodoApp.Db.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TodoContext todoContext;
        private readonly IMapper mapper;

        public UserRepository(TodoContext todoContext, IMapper mapper)
        {
            this.todoContext = todoContext;
            this.mapper = mapper;
        }

        public UserDto GetById(int id)
        {
            var item = todoContext.Find<User>(id);
            return mapper.Map<UserDto>(item);
        }

        public IEnumerable<UserDto> GetAll()
        {
            var items = todoContext.TodoItems.ToArray();
            return items.Select(mapper.Map<UserDto>).ToArray();
        }

        public UserDto Create(UserDto item)
        {
            var entity = mapper.Map<TodoItem>(item);
            var res = todoContext.TodoItems.Add(entity);
            todoContext.SaveChanges();
            return mapper.Map<UserDto>(res.Entity);
        }

        public bool Update(UserDto item)
        {
            throw new NotImplementedException();
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
    }
}
