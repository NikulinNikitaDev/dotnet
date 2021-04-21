using AutoMapper;
using StudentsApp.Core.Models;
using StudentsApp.API.Resources;

namespace StudentsApp.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Mark, MarkResource>().ReverseMap();
            CreateMap<Student, StudentResource>().ReverseMap();
            
            CreateMap<SaveMarkResource, Mark>();
            CreateMap<SaveStudentResource, Student>();
        }
    }
}