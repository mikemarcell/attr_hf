using TodoApp.Db.Dto;

namespace TodoApp.Db.Repositories
{
    public interface IUserPictureRepository
    {
        UserPictureDto GetPictureByUserId(int id);
        void SetPicture(UserPictureDto userPicture);
    }
}