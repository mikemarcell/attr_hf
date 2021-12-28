using TodoApp.Db.Dto;

namespace TodoApp.Db.Repositories
{
    public interface IUserRepository
    {
        UserDto Create(UserDto item);

        bool Delete(int id);

        IEnumerable<UserDto> GetAll();

        UserDto GetById(int id);

        bool Update(UserDto item);
    }
}