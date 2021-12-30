using AutoMapper;
using TodoApp.Db.Model;
using TodoApp.Shared.Dto;

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
