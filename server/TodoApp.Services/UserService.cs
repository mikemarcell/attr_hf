using TodoApp.Db.Dto;
using TodoApp.Db.Model;

namespace TodoApp.Db.Repositories
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public UserDto Create(UserDto item)
        {
            return userRepository.Create(item);
        }

        public bool Delete(int id)
        {
            return userRepository.Delete(id);
        }

        public IEnumerable<UserDto> GetAll()
        {
            return userRepository.GetAll();
        }

        public UserDto GetById(int id)
        {
            return userRepository.GetById(id);
        }

        public bool Update(UserDto item)
        {
            return userRepository.Update(item);
        }
    }
}
