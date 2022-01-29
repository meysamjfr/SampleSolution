using AutoMapper;
using NetTopologySuite.Geometries;
using Project.DTOs.Adverts;
using Project.DTOs.ApplicationUsers;
using Project.DTOs.Cities;
using Project.DTOs.ExtendedUser;
using Project.DTOs.Provinces;
using Project.Entities;

namespace Project.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles(GeometryFactory geometryFactory)
        {
            CreateMap<Advert, AdvertDTO>()
               .ForMember(x => x.Latitude, dto => dto.MapFrom(prop => prop.Location.Y))
               .ForMember(x => x.Longitude, dto => dto.MapFrom(prop => prop.Location.X))
               .ReverseMap();

            CreateMap<CreateAdvertDTO, Advert>()
                .ForMember(x => x.Location, x => x.MapFrom(dto =>
                    geometryFactory.CreatePoint(new Coordinate(dto.Longitude, dto.Latitude))))
                .ReverseMap();

            CreateMap<ApplicationUserDTO, ApplicationUser>().ReverseMap();
            CreateMap<LoginApplicationUserDTO, ApplicationUser>()
                .ForMember(x => x.Phone, x => x.MapFrom(dto => dto.Phone))
                .ForMember(x => x.Balance, options => options.Ignore())
                .ForMember(x => x.FirstName, options => options.Ignore())
                .ForMember(x => x.LastName, options => options.Ignore())
                .ForMember(x => x.Email, options => options.Ignore())
                .ForMember(x => x.LastLogin, options => options.Ignore())
                .ForMember(x => x.VerificationCode, options => options.Ignore())
                .ForMember(x => x.IsActive, options => options.Ignore())
                .ReverseMap();

            CreateMap<City, CityDTO>()
                .ForMember(x => x.Province, dto => dto.MapFrom(prop => prop.Province.Name))
                .ReverseMap();

            CreateMap<ProvinceDTO, Province>().ReverseMap();

            CreateMap<RegisterExtendedUserDTO, ExtendedUser>().ReverseMap();
        }

    }
}
