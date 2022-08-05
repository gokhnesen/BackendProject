using AutoMapper;
using BackendProject.Configurations.Mapper.AfterMaps;
using BackendProject.Data.Requests;
using BackendProject.Data.Response;

namespace BackendProject.Profiles
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Person, Entity.DomainModels.Person>().ReverseMap();
            CreateMap<Gender, Entity.DomainModels.Gender>().ReverseMap();
            CreateMap<KPI, Entity.DomainModels.KPI>().ReverseMap();
            CreateMap<Team, Entity.DomainModels.Team>().ReverseMap();
            CreateMap<User, Entity.DomainModels.User >().ReverseMap();

            CreateMap<UpdatePersonRequest, Entity.DomainModels.Person>().AfterMap<UpdatePersonRequestAfterMap>();
            CreateMap<UpdateKPIRequest, Entity.DomainModels.KPI >().ReverseMap();
            CreateMap<AddPersonRequest, Entity.DomainModels.Person >().AfterMap<AddPersonRequestAfterMap>();
            CreateMap<AddTeamRequest, Entity.DomainModels.Team >().ReverseMap();
            CreateMap<UserForLoginRequest, Entity.DomainModels.User >().ReverseMap();
           







        }
    }
}
