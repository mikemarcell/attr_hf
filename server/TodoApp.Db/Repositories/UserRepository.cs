using AutoMapper;
using TodoApp.Db.Model;
using TodoApp.Shared.Dto;
using TodoApp.Shared.Interface;

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
            var items = todoContext.Users.ToArray();
            return items.Select(mapper.Map<UserDto>).ToArray();
        }

        public UserDto Create(UserDto item)
        {
            var entity = mapper.Map<User>(item);
            var res = todoContext.Users.Add(entity);
            todoContext.SaveChanges();
            return mapper.Map<UserDto>(res.Entity);
        }

        public bool Update(UserDto item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            var item = todoContext.Users.Find(id);
            if (item == null)
            {
                return false;
            }
            todoContext.Users.Remove(item);
            return true;
        }
    }
}
