using AutoMapper;
using TodoApp.Db.Model;
using TodoApp.Shared.Dto;
using TodoApp.Shared.Interface;

namespace TodoApp.Db.Repositories
{
    public class UserPictureRepository : IUserPictureRepository
    {
        private readonly TodoContext todoContext;
        private readonly IMapper mapper;

        public UserPictureRepository(TodoContext todoContext, IMapper mapper)
        {
            this.todoContext = todoContext;
            this.mapper = mapper;
        }

        public UserPictureDto GetPictureByUserId(int id)
        {
            var pic = todoContext.UserPictures.FirstOrDefault(p => p.UserId == id);
            return mapper.Map<UserPictureDto>(pic);
        }

        public void SetPicture(UserPictureDto userPicture)
        {
            var pic = todoContext.UserPictures.FirstOrDefault(u => u.UserId == userPicture.UserId);

            if (pic == null)
            {
                pic = new UserPicture { UserId = userPicture.UserId };
            }

            pic.ContentType = userPicture.ContentType;
            pic.Data = userPicture.Data;
            todoContext.UserPictures.Update(pic);
            todoContext.SaveChanges();
        }
    }
}
