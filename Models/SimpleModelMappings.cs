using AutoMapper;

namespace Isolani.Models
{
    public class SimpleModelMappings : Profile
    {
        public SimpleModelMappings()
        {
            CreateMap<NewUserPlayerRequest, Player>();
            CreateMap<NewUserPlayerRequest, User>();
        }
    }
}