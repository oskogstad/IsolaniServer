using AutoMapper;

namespace Isolani.Models
{
    public class SimpleModelMappings : Profile
    {
        public SimpleModelMappings()
        {
            CreateMap<User, Player>();
            CreateMap<NewUserRequest, User>();
        }
    }
}