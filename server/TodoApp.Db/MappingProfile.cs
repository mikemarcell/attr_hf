using AutoMapper;
using TodoApp.Db.Model;

namespace TodoApp.Db
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<TodoItem, TodoItemDto>().ReverseMap();
    }
  }
}
