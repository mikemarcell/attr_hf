using TodoApp.Shared.Dto;

namespace TodoApp.Shared.Interface
{
    public interface IUserPictureRepository
    {
        UserPictureDto GetPictureByUserId(int id);

        void SetPicture(UserPictureDto userPicture);
    }
}