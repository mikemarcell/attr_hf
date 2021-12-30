using TodoApp.Shared.Dto;

namespace TodoApp.Shared.Interface
{
    public interface IUserService
    {
        UserDto Create(UserDto item);

        bool Delete(int id);

        IEnumerable<UserDto> GetAll();

        UserDto GetById(int id);

        bool Update(UserDto item);

        UserPictureDto GetPicture(int id);

        void SetPicture(UserPictureDto userPicture);
    }
}