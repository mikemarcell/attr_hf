using TodoApp.Db.Dto;
using TodoApp.Db.Model;

namespace TodoApp.Db.Repositories
{
    public interface IUserService
    {
        UserDto Create(UserDto item);

        bool Delete(int id);

        IEnumerable<UserDto> GetAll();

        UserDto GetById(int id);

        bool Update(UserDto item);
    }
}