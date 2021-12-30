using TodoApp.Shared.Dto;
using TodoApp.Shared.Interface;

namespace TodoApp.Db.Repositories
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IUserPictureRepository userPictureRepository;

        public UserService(IUserRepository userRepository, IUserPictureRepository userPictureRepository)
        {
            this.userRepository = userRepository;
            this.userPictureRepository = userPictureRepository;
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

        public UserPictureDto GetPicture(int id)
        {
            return userPictureRepository.GetPictureByUserId(id);
        }

        public void SetPicture(UserPictureDto userPicture)
        {
            userPictureRepository.SetPicture(userPicture);
        }

        public bool Update(UserDto item)
        {
            return userRepository.Update(item);
        }
    }
}
