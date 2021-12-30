using AutoMapper;
using TodoApp.Db.Dto;
using TodoApp.Db.Model;

namespace TodoApp.Db
{
    public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<TodoItem, TodoItemDto>().ReverseMap();
      CreateMap<User, UserDto>().ReverseMap();
      CreateMap<UserPicture, UserPictureDto>().ReverseMap();
    }
  }
}
